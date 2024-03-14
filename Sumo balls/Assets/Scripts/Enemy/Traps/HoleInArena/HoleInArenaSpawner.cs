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


    private List<HoleInArenaManager> _allHolesInArena = new List<HoleInArenaManager>();
    private List<HoleInArenaManager> _holesInArena = new List<HoleInArenaManager>();
    private List<Spawncandidate> _candidatesToSpawn = new List<Spawncandidate>();

    private bool _isSpawningHoles;
    [SerializeField, HideInInspector] private float _testRadius;
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

        float maxHoleRadius = Random.Range(_settings.MinMaxHoleRadius, _settings.MaxHoleMaxRadius);
        Vector3 holeSpawnPos = SelectSpawnPos(maxHoleRadius,out bool canSpawn);
        if(!canSpawn) // if hole can't be safetly spawned, abort.
        {
            _timeCounter.ResetTimer();
            return;
        }
        HoleInArenaManager hole = _pool.GetHole();
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
        _settings = settings;
        _isSetUp = true;
    }
    private Vector3 SelectSpawnPos(float holeRadius, out bool canSpawn)
    {
        _candidatesToSpawn.Clear();
        Vector3 enemySpawnPos = _hexagonSpawn.GetAvilablePositionForSphereInside(holeRadius);
        bool isSelectingSpawnpos = true;
        int selectiontries = 1;
        canSpawn = true;
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
                enemySpawnPos = _hexagonSpawn.GetAvilablePositionForSphereInside(holeRadius);

                if (selectiontries == 10) isSelectingSpawnpos = false;
            }
            else isSelectingSpawnpos = false;
        }
        if (canSpawn) return _candidatesToSpawn[_candidatesToSpawn.Count - 1].position;
        else return Vector3.zero;
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
    private void TestSingleSpawn()
    {
        //2.183955
        float maxHoleRadius = _testRadius; //Random.Range(_settings.MinMaxHoleRadius, _settings.MaxHoleMaxRadius);
        _debugSphere.transform.localScale = new Vector3(maxHoleRadius*2, maxHoleRadius*2, maxHoleRadius*2);
        for (int i = 0; i < 60; i++)
        {
            GameObject go = Instantiate(_debugSphere, _hexagonSpawn.GetAvilablePositionForSphereInside(maxHoleRadius), _debugSphere.transform.rotation);
            go.transform.position = new Vector3(go.transform.position.x, _settings.ArenaHeight, go.transform.position.z);
        }
    }
    private void TestSettingseSpawn()
    {
        float maxHoleRadius = Random.Range(_settings.MinMaxHoleRadius,_settings.MaxHoleMaxRadius); //Random.Range(_settings.MinMaxHoleRadius, _settings.MaxHoleMaxRadius);
        _debugSphere.transform.localScale = new Vector3(maxHoleRadius * 2, maxHoleRadius * 2, maxHoleRadius * 2);
        for (int i = 0; i < 60; i++)
        {
            GameObject go = Instantiate(_debugSphere, _hexagonSpawn.GetAvilablePositionForSphereInside(maxHoleRadius), _debugSphere.transform.rotation);
            go.transform.position = new Vector3(go.transform.position.x, _settings.ArenaHeight, go.transform.position.z);
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(HoleInArenaSpawner))]
    public class HoleInArenaSpawnerEditor: Editor
    {
        SerializedProperty settings;
        SerializedProperty testRadius;
        SerializedProperty debug;
        SerializedProperty isSetUp;
        SerializedProperty debugSphere;
        private void OnEnable()
        {
            testRadius = serializedObject.FindProperty("_testRadius");
            settings = serializedObject.FindProperty("_settings");
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
                EditorGUILayout.PropertyField(testRadius);
                EditorGUILayout.PropertyField(settings);
                EditorGUILayout.PropertyField(debugSphere);
                if (GUILayout.Button("Test single Spawn"))
                {
                    (target as HoleInArenaSpawner).TestSingleSpawn();
                }
                if (GUILayout.Button("Test settings spawn"))
                {
                    (target as HoleInArenaSpawner).TestSettingseSpawn();
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
