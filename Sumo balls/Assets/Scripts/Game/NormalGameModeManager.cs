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
    private int _killedEnemies = 0;
    private void Awake()
    {
        _normalModeSettings = GlobalSettings.SelectedGameModeSettings as NormalModeSettings;
#if UNITY_EDITOR
        if (debug) _normalModeSettings = _debugSettings as NormalModeSettings;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime >= _normalModeSettings.PowerUpSpawnRateInSeconds * _powerUpSpawns)
        {
            _powerUpSpawns++;
            _powerUpSpawner.SpawnPowerUp();
        }
        if (_killedEnemies >= _normalModeSettings.NumberOfEnemiesToDefeat) return;
        _currentTime +=Time.deltaTime;
        

    }

    public override void PrepareStage()
    {
        _taskDescription.SetValue((_normalModeSettings.NumberOfEnemiesToDefeat).ToString());
        for (int i = 0; i < _normalModeSettings.SimultaneouslNumberOfEnemies; i++)
        {
            _spawnedEnemies++;
            SpawnEnemy();
        }
    }
    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath.RemoveListener(OnEnemyDeath);
        _killedEnemies++;
        if (_spawnedEnemies < _normalModeSettings.NumberOfEnemiesToDefeat)
        {
            SpawnEnemy();
            _spawnedEnemies++;
        }
        else if(_killedEnemies== _normalModeSettings.NumberOfEnemiesToDefeat)
        {
            _stageClearPause.SetPause(true);
        }
        _taskDescription.SetValue((_normalModeSettings.NumberOfEnemiesToDefeat - _killedEnemies).ToString());
    }

    public override void RestartStage()
    {
        _currentTime = 0;
        _powerUpSpawns = 1;
        _spawnedEnemies = 0;
        _killedEnemies = 0;
        OnResetStage?.Invoke();
        //PrepareStage();
    }

    private void SpawnEnemy()
    {
        Enemy en = _enemySpawner.SpawnEnemy();
        en.OnDeath.AddListener(OnEnemyDeath);
        if(_normalModeSettings.AreEnemiesRandomized)
        {
            en.RandomizeAngularDrag(0.5f, 3.5f);
            en.RandomizePushForce(200, 600);
        }
    }
}
