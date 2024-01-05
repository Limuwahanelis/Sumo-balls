using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalGameModeManager : GameModeManager
{
    
    private NormalModeSettings _normalModeSettings;
    private float _currentTime;
    private int _powerUpSpawns = 1;
    private int _spawnedEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {

        _normalModeSettings = GlobalSettings.SelectedGameModeSettings as NormalModeSettings;
#if UNITY_EDITOR
        if (_debug) _normalModeSettings = _debugSettings as NormalModeSettings;
#endif
        PrepareStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnedEnemies >= _normalModeSettings.NumberOfEnemiesToDefeat) return;
        _currentTime +=Time.deltaTime;
        if (_currentTime >= _normalModeSettings.PowerUpSpawnRateInSeconds * _powerUpSpawns)
        {
            _powerUpSpawns++;
            _powerUpSpawner.SpawnPowerUp();
        }
    }

    private void PrepareStage()
    {
        for (int i = 0; i < _normalModeSettings.SimultaneouslNumberOfEnemies; i++)
        {
            _spawnedEnemies++;
            _enemySpawner.SpawnEnemy().OnDeath.AddListener(OnEnemyDeath);
        }
    }
    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath.RemoveListener(OnEnemyDeath);
        if (_spawnedEnemies < _normalModeSettings.NumberOfEnemiesToDefeat)
        {
            _enemySpawner.SpawnEnemy().OnDeath.AddListener(OnEnemyDeath);
            _spawnedEnemies++;
        }
        else
        {
            _stageClearPause.SetPause(true);
        }
    }

    public override void RestartStage()
    {
        _currentTime = 0;
        _powerUpSpawns = 1;
        _spawnedEnemies = 0;
        OnResetStage?.Invoke();
        PrepareStage();
    }
}
