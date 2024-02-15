using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBallWithPowerUp : TutorialTask
{
    [SerializeField] CombatTutorialPowerUpSpawner _powerUpSpawner;
    [SerializeField] TimeCounter _timeCounter;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] float _powerUpSpawnTime;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_timeCounter.CurrentTime>=_powerUpSpawnTime)
        {
            _powerUpSpawner.SpawnPowerUp().OnPickedUp.AddListener(StartTimer);
            _timeCounter.ResetTimer();
            _timeCounter.SetCountTime(false);
        }
    }

    public override void StartTask()
    {
        base.StartTask();
        _timeCounter.SetCountTime(true);
        enemy = _enemySpawner.SpawnEnemy();
        enemy.OnDeath.AddListener(CompleteTask);
    }
    private void StartTimer()
    {
        _timeCounter.SetCountTime(true);
    }
    private void CompleteTask(Enemy en)
    {
        enemy.OnDeath.RemoveListener(CompleteTask);
        CompleteTask();
    }
    public override void CompleteTask()
    {
        base.CompleteTask();
        GameDataManager.UpdateCombatTutorial(true);
        enabled = false;
    }
}
