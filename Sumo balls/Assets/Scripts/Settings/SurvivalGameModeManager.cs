using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalGameModeManager : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] PowerUpSpawner _powerUpSpawner;
    private SurvivalModeSettings _survivalModeSettings;
    private float _currentTime;
    private int _powerUpSpawns = 1;
    private int _spawnedWaves = 1;
    private int _killedEnemies = 0;
    private bool _isCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        _survivalModeSettings = GlobalSettings.SelectedGameModeSettings as SurvivalModeSettings;
        for (int i = 0; i < _survivalModeSettings.StartingNumberOfEnemies; i++)
        {
            _enemySpawner.SpawnEnemy().OnDeath += OnEnemyDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCompleted) return;
        if (_currentTime >= _survivalModeSettings.TimeToSurvive)
        {
            Debug.Log("Completed");
            _isCompleted = true;
            Debug.Log($"killed {_killedEnemies}");
            return;
        }
        if(_currentTime>=_survivalModeSettings.SecondsToSpawnEnemies*_spawnedWaves)
        {
            _spawnedWaves++;
            Debug.Log("spawn wave");
            for(int i=0;i<_survivalModeSettings.NumbeOfEnemiesToSpawn;i++)
            {
                _enemySpawner.SpawnEnemy().OnDeath += OnEnemyDeath;
            }
        }
        _currentTime += Time.deltaTime;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= OnEnemyDeath;
        Debug.Log("killed");
        _killedEnemies++;
    }
}
