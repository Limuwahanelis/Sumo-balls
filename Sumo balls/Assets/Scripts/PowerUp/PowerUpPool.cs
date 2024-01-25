using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class PowerUpPool : MonoBehaviour
{
    [SerializeField, SearchContext("t:PowerUp")] PowerUp _powerUpPrefab;
    private ObjectPool<PowerUp> _powerUpPool;

    // Start is called before the first frame update
    void Awake()
    {
        _powerUpPool = new ObjectPool<PowerUp>(CreatePowerUp, OnTakePowerUpFromPool, OnReturnPowerUpToPool);
    }

    public PowerUp GetPowerUp()
    {
        return _powerUpPool.Get();
    }
    PowerUp CreatePowerUp()
    {
        PowerUp powerUp = Instantiate(_powerUpPrefab);
        powerUp.SetPool(_powerUpPool);
        return powerUp;

    }
    public void OnTakePowerUpFromPool(PowerUp powerUp)
    {
        powerUp.gameObject.SetActive(true);
    }
    public void OnReturnPowerUpToPool(PowerUp powerUp)
    {
        powerUp.gameObject.SetActive(false);
    }
}
