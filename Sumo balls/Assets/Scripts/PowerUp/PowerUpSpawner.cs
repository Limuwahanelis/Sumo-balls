using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] float _spawnRange;
    [SerializeField] float _arenaYpos;
    [SerializeField] PowerUp _powerUpPrefab;

    public void SpawnPowerUp()
    {
        PowerUp powerUp = Instantiate(_powerUpPrefab);
        Vector3 pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), _arenaYpos, Random.Range(-_spawnRange, _spawnRange));
        powerUp.transform.position = pos;
    }
}
