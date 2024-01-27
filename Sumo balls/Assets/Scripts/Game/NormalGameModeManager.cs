using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalGameModeManager : GameModeManager
{
    [SerializeField] WallsManager _wallsManager;
    private NormalModeSettings _normalModeSettings;
    private float _currentTime;
    private int _powerUpSpawns = 1;
    private int _spawnedEnemies = 0;
    private int _killedEnemies = 0;
    private int _score = 2;
    private void Awake()
    {
        
#if UNITY_EDITOR
        if (debug) _normalModeSettings = _debugSettings as NormalModeSettings;
        else _normalModeSettings = GlobalSettings.SelectedStage.GameModeSettings as NormalModeSettings;
        if (_normalModeSettings.IsInCage)
        {
            _wallsManager.SetUp(_normalModeSettings);
            OnResetStage.AddListener(_wallsManager.RestoreWalls);
        }
        return;
#endif
        _normalModeSettings = GlobalSettings.SelectedStage.GameModeSettings as NormalModeSettings;
        if (_normalModeSettings.IsInCage)
        {
            _wallsManager.SetUp(_normalModeSettings);
            OnResetStage.AddListener(_wallsManager.RestoreWalls);
        }
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
        if(!_normalModeSettings.IsInCage && _score >= 0&& _currentTime >= _normalModeSettings.TimeRequiredForStar[_score] )
        {
            _score--;
            _stageCompleteScore.ReduceScore();
        }
        _currentTime +=Time.deltaTime;
        

    }

    public override void PrepareStage()
    {
        _stageCompleteScore.SetScore(3);
        _stageCompleteScore.SetDescription(_normalModeSettings.GetStarsDescription());
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
            OnStageCompleted?.Invoke();
        }
        _taskDescription.SetValue((_normalModeSettings.NumberOfEnemiesToDefeat - _killedEnemies).ToString());
        _enemySpawner.ReturnEnemyToPool(enemy as NormalEnemy);
    }

    public override void RestartStage()
    {
        _currentTime = 0;
        _powerUpSpawns = 1;
        _spawnedEnemies = 0;
        _killedEnemies = 0;
        _score = 2;
        _stageCompleteScore.SetScore(3);
        OnResetStage?.Invoke();
    }

    private void SpawnEnemy()
    {
        NormalEnemy en = _enemySpawner.SpawnEnemy();
        en.OnDeath.AddListener(OnEnemyDeath);
        if(_normalModeSettings.AreEnemiesRandomized)
        {
            en.RandomizeAngularDrag(0.5f, 3.5f);
            en.RandomizePushForce(200, 600);
        }
    }
    private void OnDestroy()
    {
        OnResetStage.RemoveListener(_wallsManager.RestoreWalls);
    }
}
