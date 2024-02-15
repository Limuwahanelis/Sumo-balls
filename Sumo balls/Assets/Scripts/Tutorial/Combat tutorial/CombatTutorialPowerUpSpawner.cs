using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTutorialPowerUpSpawner : MonoBehaviour
{
    [SerializeField] float _spawnRange;
    [SerializeField] float _arenaYpos;
    [SerializeField] PowerUpPool _powerUpPool;
    private List<CombatTutorialPowerUp> _allPowerUps = new List<CombatTutorialPowerUp>();

    public CombatTutorialPowerUp SpawnPowerUp()
    {
        CombatTutorialPowerUp powerUp = _powerUpPool.GetPowerUp() as CombatTutorialPowerUp;
        if (!_allPowerUps.Contains(powerUp))_allPowerUps.Add(powerUp);
        Vector3 pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), _arenaYpos, Random.Range(-_spawnRange, _spawnRange));
        powerUp.transform.position = pos;
        return powerUp;
    }
    public void ReturnAllPowerUpsToPool()
    {
        foreach (PowerUp powerUp in _allPowerUps)
        {
            _powerUpPool.OnReturnPowerUpToPool(powerUp);
        }

    }
    private void OnDestroy()
    {
        foreach (CombatTutorialPowerUp powerUp in _allPowerUps)
        {
            powerUp.OnPickedUp.RemoveAllListeners();
        }
    }
}
