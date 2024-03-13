using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using static FallingBallsSpawner;
using Random = UnityEngine.Random;

public class HoleInArenaSpawner : MonoBehaviour
{
    [SerializeField] HexagonSpawn _hexagonSpawn;
    [SerializeField] HoleInArenaPool _pool;
    [SerializeField,HideInInspector] GameObject _debugSphere;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] TimeCounter _timeCounter;
    [SerializeField] bool _debug;
    [SerializeField,HideInInspector] HoleInArenaSettings _settings;
    //[SerializeField] float _arenaHeight;
    //[SerializeField] float _firstHoleDelay;
    //[SerializeField] float _minHoleMaxRadius;
    //[SerializeField] float _maxHoleMaxRadius;
    //[SerializeField] float _timeToReachMaxzRadius;
    //[SerializeField] float _timeToStayAtMaxRadius;
    //[SerializeField] float _timeToBeginGrowth;
    //[SerializeField] float _spawnDelay;
    //[SerializeField] int _numberOfConcurrentHoles;


    private List<HoleInArenaManager> _allHolesInArena = new List<HoleInArenaManager>();
    private List<HoleInArenaManager> _holesInArena = new List<HoleInArenaManager>();
    private List<Spawncandidate> _candidatesToSpawn = new List<Spawncandidate>();

    private bool _isSpawningHoles;
    [SerializeField,HideInInspector] private bool _isSetUp;
    // Start is called before the first frame update
    void Start()
    {
        float holeRadius = Random.Range(1, 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isSpawningHoles) return;
        if (_timeCounter.CurrentTime >_settings.SpawnDelay)// _spawnDelay)
        {
            if (_holesInArena.Count < _settings.NumberOfConcurrentHoles)
            {
                SpawnHole();
            }
           
        }
    }
    private void SpawnHole()
    {
        HoleInArenaManager hole = _pool.GetHole();

        float maxHoleRadius = Random.Range(_settings.MinMaxHoleRadius, _settings.MaxHoleMaxRadius);
        Vector3 holeSpawnPos = SelectSpawnPos(maxHoleRadius);
        holeSpawnPos.y = _settings.ArenaHeight;
        hole.transform.position = holeSpawnPos;
        hole.SetUpHole(maxHoleRadius, _settings.TimeToReachMaxRadius, _settings.TimeToStayAtMaxRadius, _settings.TimeToBeginGrowth);
        hole.gameObject.SetActive(true);
        hole.OnHoleCycleCompleted.AddListener(OnHoleDisappear);
        _holesInArena.Add(hole);
        _enemySpawner.UpdateAvoidTransforms(hole.transform, maxHoleRadius, false);
        hole.gameObject.SetActive(true);
        if (!_allHolesInArena.Contains(hole)) _allHolesInArena.Add(hole);
        _timeCounter.ResetTimer();
    }
    public void SetSpawning(bool value)
    {
        if (!_isSetUp)
        {
            enabled = false;
            return;
        }
        _timeCounter.SetCountTime(value);
       _isSpawningHoles = value;
    }
    public void SetSpawnParameters(HoleInArenaSettings settings)
    {
        //_arenaHeight = settings.ArenaHeight;
        //_minHoleMaxRadius = settings.MinMaxHoleRadius;
        //_maxHoleMaxRadius = settings.MaxHoleMaxRadius;
        //_timeToReachMaxzRadius = settings.TimeToReachMaxRadius;
        //_numberOfConcurrentHoles = settings.NumberOfConcurrentHoles;
        //_timeToStayAtMaxRadius = settings.TimeToStayAtMaxRadius;
        //_timeToBeginGrowth = settings.TimeToBeginGrowth;
        //_spawnDelay = settings.SpawnDelay;
        _settings = settings;
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
    public void ClearSpawn()
    {
        for (int i = _holesInArena.Count - 1; i >= 0; i--)
        {
            OnHoleDisappear(_holesInArena[i]);
        }
        _timeCounter.ResetTimer();
    }
    private void OnHoleDisappear(HoleInArenaManager hole)
    {
        hole.OnHoleCycleCompleted.RemoveListener(OnHoleDisappear);
        hole.ReturnToPool();
        _enemySpawner.UpdateAvoidTransforms(hole.transform, hole.MaxRadius, true,true);
        _holesInArena.Remove(hole);
    }
    private void TestSpawn()
    {
        float maxHoleRadius = Random.Range(_settings.MinMaxHoleRadius, _settings.MaxHoleMaxRadius);
        Debug.Log(string.Format("radius: {0}",maxHoleRadius));
        _timeCounter.ResetTimer();
        _debugSphere.transform.localScale = new Vector3(maxHoleRadius*2, maxHoleRadius*2, maxHoleRadius*2);   
        for (int i= 0; i < 60; i++)
            Instantiate(_debugSphere, _hexagonSpawn.GetAvilablePosition(maxHoleRadius, maxHoleRadius), _debugSphere.transform.rotation);
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(HoleInArenaSpawner))]
    public class HoleInArenaSpawnerEditor: Editor
    {
        SerializedProperty holeSettings;
        SerializedProperty debug;
        SerializedProperty isSetUp;
        SerializedProperty debugSphere;
        private void OnEnable()
        {
            holeSettings = serializedObject.FindProperty("_settings");
            debug = serializedObject.FindProperty("_debug");
            isSetUp = serializedObject.FindProperty("_isSetUp");
            debugSphere = serializedObject.FindProperty("_debugSphere");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if(debug.boolValue)
            {
                isSetUp.boolValue = true;
                EditorGUILayout.PropertyField(holeSettings);
                EditorGUILayout.PropertyField(debugSphere);
                if (GUILayout.Button("Test Spawn"))
                {
                    (target as HoleInArenaSpawner).TestSpawn();
                }
            }
            else
            {
                isSetUp.boolValue = false;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
