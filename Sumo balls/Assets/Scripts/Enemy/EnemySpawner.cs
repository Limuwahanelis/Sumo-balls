using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] float _spawnRange;
    [SerializeField] float _arenaYpos;
    [SerializeField] float _playerSafeSpace = 2f;
    [SerializeField] EnemyPool _enemyPool;
    [SerializeField] AudioPool _audioPool;
    [SerializeField] Transform _playerPos;
    private List<NormalEnemy> _allEnemies = new List<NormalEnemy>();
    public NormalEnemy SpawnEnemy()
    {
        NormalEnemy enemy = _enemyPool.GetEnemy();
        if(!_allEnemies.Contains(enemy)) _allEnemies.Add(enemy);
        enemy.transform.position = SelectSpawnPos(enemy.transform.localScale.y);
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemy.SetPlayer(_playerPos.gameObject);
        enemy.SetAudioPool(_audioPool);
        enemy.gameObject.SetActive(true);
        return enemy;
    }
    private Vector3 SelectSpawnPos(float enemySacle)
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

                if (spawnZ >= downZrang && spawnZ <= upZrange) // z spawn is inside _playerBody so x must be outside
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
        Vector3 randomPos = new Vector3(spawnX, _arenaYpos* enemySacle, spawnZ);
        return randomPos;
    }
    public void ReturnAllEnemiesToPool()
    {
        foreach(NormalEnemy enemy in _allEnemies)
        {
            _enemyPool.OnReturnEnemyToPool(enemy);
        }
        
    }
    public void ReturnEnemyToPool(NormalEnemy enemy)
    {
        _enemyPool.OnReturnEnemyToPool(enemy);
    }
    public void SetAllEnemyScript(bool value)
    {
        foreach (NormalEnemy enemy in _allEnemies)
        {
            enemy.enabled = value;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new Vector3(0, _arenaYpos, 0), new Vector3(2*_spawnRange, 2*_spawnRange, 2*_spawnRange));
        if(_playerPos != null) Gizmos.DrawWireCube(new Vector3(_playerPos.position.x, _playerPos.position.y, _playerPos.position.z), new Vector3(2 * _playerSafeSpace, 2 * _playerSafeSpace, 2 * _playerSafeSpace));

    }
    private void OnDestroy()
    {
        foreach(NormalEnemy enemy in _allEnemies)
        {
            enemy.OnDeath.RemoveAllListeners();
        }
    }
}
