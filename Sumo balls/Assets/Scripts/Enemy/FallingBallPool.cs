using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class FallingBallPool : MonoBehaviour
{
    [SerializeField, SearchContext("t:FallingBall")] FallingBall _fallingEnemyPrefab;
    private ObjectPool<FallingBall> _enemyPool;

    // Start is called before the first frame update
    void Awake()
    {
        _enemyPool = new ObjectPool<FallingBall>(CreateEnemy, OnTakeEnemyFromPool, OnReturnEnemyToPool);
    }

    public FallingBall GetFallingBall()
    {
        return _enemyPool.Get();
    }
    FallingBall CreateEnemy()
    {
        FallingBall enemy = Instantiate(_fallingEnemyPrefab);
        enemy.SetPool(_enemyPool);
        return enemy;

    }
    public void OnTakeEnemyFromPool(FallingBall enemy)
    {

    }
    public void OnReturnEnemyToPool(FallingBall enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
