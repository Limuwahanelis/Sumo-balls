using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalGameModeManager : GameModeManager
{
    [SerializeField] WallsManager _wallsManager;
    [SerializeField] TimeCounter _timeCounter;
    [SerializeField] InStageDescription _timeDisplay;
    [SerializeField] Player _player;
    private NormalModeSettings _normalModeSettings;
    private int _powerUpSpawns = 1;
    private int _spawnedEnemies = 0;
    private int _killedEnemies = 0;
    private int _score = 2;
    private void Awake()
    {
        _restartStage.OnTriggered.AddListener(RestartStage);
        if (GlobalSettings.SelectedStage == null)
        {
#if UNITY_EDITOR
            if (debug) _normalModeSettings = _debugSettings as NormalModeSettings;
#else
            Debug.LogError("Stage was not set in Global settings but was loaded");
#endif
        }
        else _normalModeSettings = GlobalSettings.SelectedStage.GameModeSettings as NormalModeSettings;
        if (_normalModeSettings.IsInCage)
        {
            _wallsManager.SetUp(_normalModeSettings);
            OnResetStage.AddListener(_wallsManager.RestoreWalls);
        }
        _stageCompleteScore.SetDescription(_normalModeSettings.GetStarsDescription());
        

    }
    private void Start()
    {
        RestartStage();
    }

    // Update is called once per frame 
    void Update()
    {
        if (_timeCounter.CurrentTime >= _normalModeSettings.PowerUpSpawnRateInSeconds * _powerUpSpawns)
        {
            _powerUpSpawns++;
            _powerUpSpawner.SpawnPowerUp();
        }
        if (_killedEnemies >= _normalModeSettings.NumberOfEnemiesToDefeat) return;
        if(!_normalModeSettings.IsInCage && _score >= 0&& _timeCounter.CurrentTime >= _normalModeSettings.TimeRequiredForStar[_score] )
        {
            _score--;
            _stageCompleteScore.ReduceScore();
        }
        _timeDisplay.SetValue(_timeCounter.FormattedTime);
        

    }
    public override void RestartStage()
    {
        PrepareStage();
        OnResetStage?.Invoke();
    }
    public override void PrepareStage()
    {
        _timeCounter.SetCountTime(false);
        _timeDisplay.SetValue("0");
        _taskDescription.SetValue((_normalModeSettings.NumberOfEnemiesToDefeat).ToString());
        _powerUpSpawns = 1;
        _spawnedEnemies = 0;
        _killedEnemies = 0;
        _timeCounter.ResetTimer();
        _score = 2;
        _stageCompleteScore.SetScore(3);
        _enemySpawner.ReturnAllEnemiesToPool();
        _powerUpSpawner.ReturnAllPowerUpsToPool();
        _player.ResetPlayer();
        for (int i = 0; i < _normalModeSettings.SimultaneouslNumberOfEnemies; i++)
        {
            _spawnedEnemies++;
            SpawnEnemy();
        }
        _enemySpawner.SetAllEnemyScript(false);
    }

    public override void StartStage()
    {

        _enemySpawner.SetAllEnemyScript(true);
        _timeCounter.SetCountTime(true);
        OnStageStarted?.Invoke();
    }
    public override void FailStage()
    {
        _timeCounter.SetCountTime(false);
        GlobalSettings.SetPause(true);
        OnStageFailed?.Invoke();
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
    }



    private void SpawnEnemy()
    {
        NormalEnemy en = _enemySpawner.SpawnEnemy();
        en.OnDeath.AddListener(OnEnemyDeath);
        EnemyBelts.Belt belt =(EnemyBelts.Belt)Random.Range((int)EnemyBelts.Belt.WHITE, (int)EnemyBelts.Belt.BLACK + 1) ;
        switch (belt)
        {
            case EnemyBelts.Belt.WHITE:
                {
                    en.RandomizeAngularDrag(0.5f, 1.5f);
                    en.RandomizePushForce(200, 600);
                    break;
                }
            case EnemyBelts.Belt.YELLOW:
                {
                    en.RandomizeAngularDrag(1.5f, 2.5f);
                    en.RandomizePushForce(300, 450);
                    break;
                }
            case EnemyBelts.Belt.BLACK:
                {
                    en.RandomizeAngularDrag(2.5f, 3.5f);
                    en.RandomizePushForce(450, 600);
                    break;
                }
                
        }
        en.SetBelt(belt);
        //if (_normalModeSettings.AreEnemiesRandomized)
        //{
        //    en.RandomizeAngularDrag(0.5f, 3.5f);
        //    en.RandomizePushForce(200, 600);
        //}
    }
    private void OnDestroy()
    {
        OnResetStage.RemoveListener(_wallsManager.RestoreWalls);
        _restartStage.OnTriggered.RemoveListener(RestartStage);
    }


}
