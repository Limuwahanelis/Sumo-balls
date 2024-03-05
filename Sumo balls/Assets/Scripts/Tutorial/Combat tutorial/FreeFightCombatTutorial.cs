using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFightCombatTutorial : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] BoolReference _skipToFreeFight;
    [SerializeField] GameObject _firstTutorialPopUp;
    [SerializeField] PlayerInputHandler _playerInputHandler;
    [SerializeField] CombatTutorialPowerUpSpawner _powerUpSpawner;
    [SerializeField] TimeCounter _timeCounter;
    [SerializeField] float _powerUpSpawnTime;
    [SerializeField] CursorLockControl _lockControl;
    private void Start()
    {
        if (_skipToFreeFight.value)
        {
            _playerInputHandler.ResetActionMap();
            _firstTutorialPopUp.SetActive(false);
            StartFreeFight();
        }
        else
        {
            _lockControl.SetForcedCursorVisibilty(true);
            _playerInputHandler.DisablePlayerNonPauseActions();
        }

    }
    private void Update()
    {
        if (_timeCounter.CurrentTime >= _powerUpSpawnTime)
        {
            _powerUpSpawner.SpawnPowerUp().OnPickedUp.AddListener(StartTimer);
            _timeCounter.ResetTimer();
            _timeCounter.SetCountTime(false);
        }
    }
    private void StartTimer()
    {
        _timeCounter.SetCountTime(true);
    }
    public void StartFreeFight()
    {
        _enemySpawner.SpawnEnemy().OnDeath.AddListener(OnEnemyDeath);
        StartTimer();
    }
    private void OnEnemyDeath(Enemy en)
    {
        en.OnDeath.RemoveListener(OnEnemyDeath);
        _enemySpawner.SpawnEnemy().OnDeath.AddListener(OnEnemyDeath);
    }
}
