using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] float _spawnRange;
    [SerializeField] float _arenaYpos;
    [SerializeField] PowerUpPool _powerUpPool;
    private List<PowerUp> _allPowerUps=new List<PowerUp>();

    public void SpawnPowerUp()
    {
        PowerUp powerUp = _powerUpPool.GetPowerUp();
        if(!_allPowerUps.Contains(powerUp))_allPowerUps.Add(powerUp);
        Vector3 pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), _arenaYpos, Random.Range(-_spawnRange, _spawnRange));
        powerUp.transform.position = pos;
    }
    public void ReturnAllPowerUpsToPool()
    {
        foreach (PowerUp powerUp in _allPowerUps)
        {
            _powerUpPool.OnReturnPowerUpToPool(powerUp);
        }

    }
}
