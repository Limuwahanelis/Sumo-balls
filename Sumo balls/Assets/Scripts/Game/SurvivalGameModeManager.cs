using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        _taskDescription.SetValue(ConvertTime(_survivalModeSettings.TimeToSurvive - _currentTime));
        if (_currentTime >= _survivalModeSettings.TimeToSurvive)
        {
            _isCompleted = true;
            _taskDescription.SetValue(ConvertTime(0));
            _stageClearPause.SetPause(true);
            return;
        }
        if (_currentTime >= _survivalModeSettings.SecondsToSpawnEnemies * _spawnedWaves)
        {
            _spawnedWaves++;
            for (int i = 0; i < _survivalModeSettings.NumbeOfEnemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
        }
        if (_currentTime >= _survivalModeSettings.TimeToSpawnPowerUp * _powerUpSpawns)
        {
            _powerUpSpawns++;
            _powerUpSpawner.SpawnPowerUp();
        }
        _currentTime += Time.deltaTime;
    }
    private string ConvertTime(float timeInSeconds)
    {
        int miliSeconds =(int) ((timeInSeconds - math.floor(timeInSeconds))*1000);
        int seconds = (int)math.floor(timeInSeconds);
        return string.Format("{0:00}:{1:000}", seconds, miliSeconds);
    }
    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath.RemoveListener(OnEnemyDeath);
        Debug.Log("killed");
        _killedEnemies++;
    }
    private void SpawnEnemy()
    {
        Enemy en = _enemySpawner.SpawnEnemy();
        en.OnDeath.AddListener(OnEnemyDeath);
        if (_survivalModeSettings.AreEnemiesRandomized)
        {
            en.RandomizeAngularDrag(0.5f, 3.5f);
            en.RandomizePushForce(200, 600);
        }
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
            SpawnEnemy();
        }
    }
}
