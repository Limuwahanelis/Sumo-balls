using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, SearchContext("t:Enemy")] Enemy _enemyPrefab;

    [SerializeField] float _spawnRange;
    [SerializeField] float _arenaYpos;

    private ObjectPool<Enemy> _enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = new ObjectPool<Enemy>(CreateEnemy);
        //SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {

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
    public void OnReturnBulletToPool(Enemy enemy)
    {
        //bullet.gameObject.SetActive(false);
    }
    private void SpawnEnemy()
    {
        float spawnX = Random.Range(-_spawnRange, _spawnRange);
        float spawnZ = Random.Range(-_spawnRange, _spawnRange);
        Vector3 randomPos = new Vector3(spawnX, _arenaYpos, spawnZ);
        Enemy enemy = GetEnemy();
        enemy.transform.position = randomPos;

    }
}
