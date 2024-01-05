using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalGameModeManager : GameModeManager
{

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
#if UNITY_EDITOR
        if (_debug) _survivalModeSettings = _debugSettings as SurvivalModeSettings;
#endif
        PrepareStage();

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
        if (_currentTime >= _survivalModeSettings.SecondsToSpawnEnemies * _spawnedWaves)
        {
            _spawnedWaves++;
            Debug.Log("spawn wave");
            for (int i = 0; i < _survivalModeSettings.NumbeOfEnemiesToSpawn; i++)
            {
                _enemySpawner.SpawnEnemy().OnDeath.AddListener(OnEnemyDeath);
            }
        }
        if (_currentTime >= _survivalModeSettings.TimeToSpawnPowerUp * _powerUpSpawns)
        {
            _powerUpSpawns++;
            Debug.Log("spawn power up");
            _powerUpSpawner.SpawnPowerUp();
        }
        _currentTime += Time.deltaTime;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath.RemoveListener(OnEnemyDeath);
        Debug.Log("killed");
        _killedEnemies++;
    }

    public override void RestartStage()
    {
        _currentTime = 0;
        _powerUpSpawns = 1;
        _spawnedWaves = 1;
        _killedEnemies = 0;
        _isCompleted = false;
        OnResetStage?.Invoke();
        PrepareStage();
    }


    public override void PrepareStage()
    {
        for (int i = 0; i < _survivalModeSettings.StartingNumberOfEnemies; i++)
        {
            _enemySpawner.SpawnEnemy().OnDeath.AddListener(OnEnemyDeath);
        }
    }
}
