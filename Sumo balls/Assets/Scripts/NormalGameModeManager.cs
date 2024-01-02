using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGameModeManager : MonoBehaviour
{
    [SerializeField] NormalModeSettings _normalModeSettings;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] PowerUpSpawner _powerUpSpawner;
    private float _currentTime;
    private int _powerUpSpawns = 1;
    private int _spawnedEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<_normalModeSettings.SimultaneouslNumberOfEnemies;i++)
        {
            _spawnedEnemies++;
            _enemySpawner.SpawnEnemy().OnDeath += OnEnemyDeath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime +=Time.deltaTime;
        if (_currentTime >= _normalModeSettings.PowerUpSpawnRateInSeconds * _powerUpSpawns)
        {
            _powerUpSpawns++;
            _powerUpSpawner.SpawnPowerUp();
        }
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        enemy.OnDeath -= OnEnemyDeath;
        if (_spawnedEnemies < _normalModeSettings.NumberOfEnemiesToDefeat)
        {
            _enemySpawner.SpawnEnemy().OnDeath += OnEnemyDeath;
            _spawnedEnemies++;
        }
        else Debug.Log("Completed");
    }
}
