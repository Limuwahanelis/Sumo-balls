using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class EnemyPool : MonoBehaviour
{
    [SerializeField, SearchContext("t:Enemy")] NormalEnemy _enemyPrefab;
    private ObjectPool<NormalEnemy> _enemyPool;

    // Start is called before the first frame update
    void Awake()
    {
        _enemyPool = new ObjectPool<NormalEnemy>(CreateEnemy,OnTakeEnemyFromPool,OnReturnEnemyToPool);
    }

    public NormalEnemy GetEnemy()
    {
        return _enemyPool.Get();
    }
    NormalEnemy CreateEnemy()
    {
        NormalEnemy enemy = Instantiate(_enemyPrefab);
        enemy.SetPool(_enemyPool);
        return enemy;

    }
    public void OnTakeEnemyFromPool(NormalEnemy enemy)
    {
        //enemy.gameObject.SetActive(true);
    }
    public void OnReturnEnemyToPool(NormalEnemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.angularVelocity = Vector3.zero;
        enemy.ResetEnemy();
    }
}
