using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class EnemyPool : MonoBehaviour
{
    [SerializeField, SearchContext("t:Enemy")] Enemy _enemyPrefab;
    private ObjectPool<Enemy> _enemyPool;

    // Start is called before the first frame update
    void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(CreateEnemy,OnTakeEnemyFromPool,OnReturnEnemyToPool);
    }

    public Enemy GetEnemy()
    {
        return _enemyPool.Get();
    }
    Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.SetPool(_enemyPool);
        return enemy;

    }
    public void OnTakeEnemyFromPool(Enemy enemy)
    {
        //enemy.gameObject.SetActive(true);
    }
    public void OnReturnEnemyToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemy.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
