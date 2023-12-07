using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField,SearchContext("t:Enemy")] GameObject _enemyPrefab;

    [SerializeField] float _spawnRange;
    [SerializeField] float _arenaYpos;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        float spawnX = Random.Range(-_spawnRange, _spawnRange);
        float spawnZ = Random.Range(-_spawnRange, _spawnRange);
        Vector3 randomPos = new Vector3(spawnX, _arenaYpos, spawnZ);
        Enemy enemy = Instantiate(_enemyPrefab, randomPos, _enemyPrefab.transform.rotation, null).GetComponent<Enemy>();

    }
}
