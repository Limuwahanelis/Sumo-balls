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
    private bool _countTime = false;
    private void Awake()
    {

        if(GlobalSettings.SelectedStage==null)
        {
#if UNITY_EDITOR
            if (debug) _survivalModeSettings = _debugSettings as SurvivalModeSettings;
#else
            Debug.LogError("Stage was not set in Global settings but was loaded");
#endif
        }
        else _survivalModeSettings = GlobalSettings.SelectedStage.GameModeSettings as SurvivalModeSettings;
        _stageCompleteScore.SetScore(0);
        _stageCompleteScore.SetDescription(_survivalModeSettings.GetStarsDescription());
        
    }
    private void Start()
    {
        RestartStage();
    }
    // Update is called once per frame
    void Update()
    {
        if (_isCompleted) return;
        if (_countTime)
        {
            _currentTime += Time.deltaTime;
            _taskDescription.SetValue(ConvertTime(_survivalModeSettings.TimeToSurvive - _currentTime));
        }
        if (_currentTime >= _survivalModeSettings.TimeToSurvive)
        {
            _isCompleted = true;
            _countTime = false;
            _taskDescription.SetValue(ConvertTime(0));
            OnStageCompleted?.Invoke();
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
        _killedEnemies++;
        if(_killedEnemies==_survivalModeSettings.EnemiesToDefeatForStar[_stageCompleteScore.ScoreAsReversedIndex])
        {
            _stageCompleteScore.IncreaseScore();
        }
    }
    private void SpawnEnemy()
    {
        NormalEnemy en = _enemySpawner.SpawnEnemy();
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
        _stageCompleteScore.SetScore(0);
        _enemySpawner.ReturnAllEnemiesToPool();
        _powerUpSpawner.ReturnAllPowerUpsToPool();
        OnResetStage?.Invoke();
        PrepareStage();
    }


    public override void PrepareStage()
    {
        _taskDescription.SetValue(ConvertTime(_survivalModeSettings.TimeToSurvive));
        for (int i = 0; i < _survivalModeSettings.StartingNumberOfEnemies; i++)
        {
            SpawnEnemy();
        }
        _enemySpawner.SetAllEnemyScript(false);
        SetCountTime(false);
    }
    public void SetCountTime(bool value)
    {
        _countTime = value;
    }

    public override void StartStage()
    {
        SetCountTime(true);
        _enemySpawner.SetAllEnemyScript(true);
        OnStageStarted?.Invoke();
    }

    public override void FailStage()
    {
        OnStageFailed?.Invoke();
    }
}
