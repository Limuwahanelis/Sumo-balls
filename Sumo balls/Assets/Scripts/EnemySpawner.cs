using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class EnemySpawner : MonoBehaviour
{
    

    [SerializeField] float _spawnRange;
    [SerializeField] float _arenaYpos;
    [SerializeField] float _playerSafeSpace = 2f;
    [SerializeField] EnemyPool _enemyPool;
    [SerializeField] Transform _playerPos;
    public void SpawnEnemy()
    {
        Enemy enemy = _enemyPool.GetEnemy();
        enemy.transform.position = SelectEnemySpawnPos();
    }
    private Vector3 SelectEnemySpawnPos()
    {
        float leftXrange = _playerPos.position.x - _playerSafeSpace;
        float upZrange = _playerPos.position.z + _playerSafeSpace;
        float downZrang = _playerPos.position.z - _playerSafeSpace;
        float rightXrange = _playerPos.position.x + _playerSafeSpace;
        float spawnZ;
        float spawnX = -10;
        spawnZ = Random.Range(-_spawnRange, _spawnRange);
        if (downZrang >= _spawnRange || upZrange <= -_spawnRange) // outside spawn
        {
            spawnX = Random.Range(-_spawnRange, _spawnRange);
        }
        else
        {
            if (leftXrange >= -_spawnRange && rightXrange <= _spawnRange) // inside spawn from both sides
            {

                if (spawnZ >= downZrang && spawnZ <= upZrange) // z spawn is inside player so x must be outside
                {
                    int a = Random.Range(0, 2); // select which side
                    if (a == 1) //left
                    {
                        spawnX = Random.Range(-_spawnRange, leftXrange);
                    }
                    else //right
                    {
                        spawnX = Random.Range(rightXrange, _spawnRange);
                    }
                }
                else
                {
                    spawnX = Random.Range(-_spawnRange, _spawnRange);
                }

            }
            else
            {
                if (leftXrange <= -_spawnRange) // outside on left
                {
                    if (spawnZ >= downZrang && spawnZ <= upZrange) spawnX = Random.Range(rightXrange, _spawnRange);
                    else spawnX = Random.Range(-_spawnRange, _spawnRange);
                }
                else if (rightXrange >= _spawnRange) // outside on right
                {
                    if (spawnZ >= downZrang && spawnZ <= upZrange) spawnX = Random.Range(-_spawnRange, leftXrange);
                    else spawnX = Random.Range(-_spawnRange, _spawnRange);
                }
            }
        }
        Vector3 randomPos = new Vector3(spawnX, _arenaYpos, spawnZ);
        return randomPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new Vector3(0, _arenaYpos, 0), new Vector3(2*_spawnRange, 2*_spawnRange, 2*_spawnRange));
        if(_playerPos != null) Gizmos.DrawWireCube(new Vector3(_playerPos.position.x, _playerPos.position.y, _playerPos.position.z), new Vector3(2 * _playerSafeSpace, 2 * _playerSafeSpace, 2 * _playerSafeSpace));

    }
}
