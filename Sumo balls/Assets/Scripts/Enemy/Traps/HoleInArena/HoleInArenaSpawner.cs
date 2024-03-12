using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using static FallingBallsSpawner;
using Random = UnityEngine.Random;

public class HoleInArenaSpawner : MonoBehaviour
{
    [SerializeField] HexagonSpawn _hexagonSpawn;
    [SerializeField] HoleInArenaPool _pool;
    [SerializeField] GameObject debugCube;


    [SerializeField] float _arenaHeight;
    [SerializeField] float _minHoleMaxRadius;
    [SerializeField] float _maxHoleMaxRadius;
    [SerializeField] float _timeToReachMaxzRadius;
    [SerializeField] float _timeToStayAtMaxRadius;
    [SerializeField] float _timeToBeginGrowth;
    [SerializeField] int _numberOfConcurrentHoles;


    private List<HoleInArena> _allHolesInArena = new List<HoleInArena>();
    private List<HoleInArena> _holesInArena = new List<HoleInArena>();
    private List<Spawncandidate> _candidatesToSpawn = new List<Spawncandidate>();

    private bool _isSpawningHoles;
    private bool _isSetUp;
    // Start is called before the first frame update
    void Start()
    {
        float holeRadius = Random.Range(1, 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isSpawningHoles) return;

        if (_holesInArena.Count < _numberOfConcurrentHoles)
        {
            HoleInArena hole = _pool.GetHole();

            float maxHoleRadius = Random.Range(_minHoleMaxRadius, _maxHoleMaxRadius);
            Vector3 holeSpawnPos = SelectSpawnPos(maxHoleRadius);
            holeSpawnPos.y = _arenaHeight;
            hole.SetUp(maxHoleRadius, _timeToReachMaxzRadius, _timeToStayAtMaxRadius, _timeToBeginGrowth);
            hole.gameObject.SetActive(true);
            hole.transform.position=holeSpawnPos;
            hole.OnHoleCycleCompleted.AddListener(OnHoleDisappear);
            _holesInArena.Add(hole);
            //_enemySpawner.UpdateAvoidTransforms(ball.MainBody.transform, ballScale / 2, false);
            if (!_allHolesInArena.Contains(hole)) _allHolesInArena.Add(hole);
        }
    }
    public void SetSpawning(bool value)
    {
        if (!_isSetUp)
        {
            enabled = false;
            return;
        }
        _isSpawningHoles = value;
    }
    public void SetSpawnParameters(HoleInArenaSettings settings)
    {
        _arenaHeight = settings.ArenaHeight;
        _minHoleMaxRadius = settings.MinMaxHoleRadius;
        _maxHoleMaxRadius = settings.MaxHoleMaxRadius;
        _timeToReachMaxzRadius = settings.TimeToReachMaxRadius;
        _numberOfConcurrentHoles = settings.NumberOfConcurrentHoles;
        _timeToStayAtMaxRadius = settings.TimeToStayAtMaxRadius;
        _timeToBeginGrowth = settings.TimeToBeginGrowth;
        _isSetUp = true;
    }
    private Vector3 SelectSpawnPos(float holeRadius)
    {
        _candidatesToSpawn.Clear();
        Vector3 enemySpawnPos = _hexagonSpawn.GetAvilablePosition(holeRadius * 2 * math.sqrt(2), holeRadius * 2 * math.sqrt(2));
        bool isSelectingSpawnpos = true;
        int selectiontries = 1;
        bool canSpawn = true;
        Spawncandidate winner;
        while (isSelectingSpawnpos)
        {
            canSpawn = true;
            Spawncandidate candidate = new Spawncandidate()
            {
                position = enemySpawnPos,
                distances = new List<float>()
            };
            _candidatesToSpawn.Add(candidate);
            for (int i = 0; i < _holesInArena.Count; i++)
            {
                Vector3 posToCheck = new Vector3(_holesInArena[i].transform.position.x, 0, _holesInArena[i].transform.position.z);
                float distanceBetweenSpawnPoints = Vector3.Distance(enemySpawnPos, posToCheck);
                if (distanceBetweenSpawnPoints < _holesInArena[i].MaxRadius + holeRadius)
                {
                    canSpawn = false;
                }
                candidate.distances.Add(distanceBetweenSpawnPoints);
            }
            if (!canSpawn)
            {
                selectiontries++;
                enemySpawnPos = _hexagonSpawn.GetAvilablePosition(holeRadius * 2 * math.sqrt(2), holeRadius * 2 * math.sqrt(2));

                if (selectiontries == 10) isSelectingSpawnpos = false;
            }
            else isSelectingSpawnpos = false;
        }
        if (canSpawn) return _candidatesToSpawn[_candidatesToSpawn.Count - 1].position;
        else
        {
            winner = _candidatesToSpawn[0];
            winner.distances.Sort();
            for (int i = 1; i < _candidatesToSpawn.Count; i++)
            {
                _candidatesToSpawn[i].distances.Sort();
                if (_candidatesToSpawn[i].distances[0] > winner.distances[0])
                {
                    winner = _candidatesToSpawn[i];
                }
            }
        }
        return winner.position;
    }
    private void OnHoleDisappear(HoleInArena hole)
    {
        hole.OnHoleCycleCompleted.RemoveListener(OnHoleDisappear);
        hole.ReturnToPool();
        //_enemySpawner.UpdateAvoidTransforms(ball.MainBody.transform, ball.MainBody.transform.localScale.x / 2f, true);
        _holesInArena.Remove(hole);
    }
    public void TestSpawn()
    {
        // multiply by 2*sqrt(2) to ensure that whole hole fit in the arena
        for(int i= 0; i < 60; i++)
            Instantiate(debugCube, _hexagonSpawn.GetAvilablePosition(3f,3f), debugCube.transform.rotation);
    }
    [CustomEditor(typeof(HoleInArenaSpawner))]
    public class dd:Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Test Spawn"))
            {
                (target as HoleInArenaSpawner).TestSpawn();
            }
        }
    }
}
