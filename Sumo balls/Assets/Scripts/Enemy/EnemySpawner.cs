using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Mathematics;
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
    private List<NormalEnemy> _spawnedEnemies= new List<NormalEnemy>();
    private List<TransformToAvoid> _transformsToAvoidSpawn = new List<TransformToAvoid>();
    private List<SpawnCandidate> _spawnCandidates = new List<SpawnCandidate>();
    private struct TransformToAvoid
    {
        public Transform transform;
        public float distance;
        public bool hasPiority;
    }
    private struct SpawnCandidate
    {
        public Vector3 position;
        public List<DistancePriority> distances;
    }
    private struct DistancePriority
    {
        public float distance;
        public bool hasPriority;
    }

    public NormalEnemy SpawnEnemy()
    {
        NormalEnemy enemy = _enemyPool.GetEnemy();
        if(!_allEnemies.Contains(enemy)) _allEnemies.Add(enemy);
        _spawnedEnemies.Add(enemy);
        enemy.OnDeath.AddListener(OnEnemyDeath);
        enemy.SetRBPos(SelectSpawnPos(enemy.transform.localScale.y));
        _transformsToAvoidSpawn.Add(new TransformToAvoid()
        {
            transform = enemy.Rigidbody.transform,
            distance=1f,
            hasPiority=true
        });
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.SetPlayer(_playerPos.gameObject);
        enemy.SetAudioPool(_audioPool);
        enemy.gameObject.SetActive(true);
        return enemy;
    }
    private Vector3 SelectSpawnPos(float enemySacle)
    {
        _spawnCandidates.Clear();
        float leftXrange = _playerPos.position.x - _playerSafeSpace;
        float upZrange = _playerPos.position.z + _playerSafeSpace;
        float downZrang = _playerPos.position.z - _playerSafeSpace;
        float rightXrange = _playerPos.position.x + _playerSafeSpace;
        float spawnZ=-20;
        float spawnX = -10;
        bool isSelectingSpawnPos = true;
        bool canSpawn = true;
        int selectiontries = 1;
        SpawnCandidate winner;
        while (isSelectingSpawnPos)
        {
            canSpawn = true;
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
            SpawnCandidate candidate = new SpawnCandidate()
            {
                position = new Vector3(spawnX, _arenaYpos * enemySacle, spawnZ),
                distances = new List<DistancePriority>()
            };
            _spawnCandidates.Add(candidate);
            for (int i=0;i<_transformsToAvoidSpawn.Count;i++)
            {
                TransformToAvoid toCheck = _transformsToAvoidSpawn[i];
                Vector3 checkVec = new Vector3(toCheck.transform.position.x, _arenaYpos * enemySacle, toCheck.transform.position.z);
                if(math.pow(candidate.position.x-toCheck.transform.position.x,2)+ math.pow(candidate.position.z - toCheck.transform.position.z, 2) <= math.pow(toCheck.distance + enemySacle, 2))
                {
                    canSpawn = false;
                }
                candidate.distances.Add(new DistancePriority()
                {
                    distance = Vector3.Distance(checkVec, candidate.position),
                    hasPriority = toCheck.hasPiority
                });
            }
            if (!canSpawn)
            {
                selectiontries++;
                if (selectiontries == 5) isSelectingSpawnPos = false;
            }
            else isSelectingSpawnPos = false;
        }
       if (canSpawn)
        {
            return new Vector3(_spawnCandidates[_spawnCandidates.Count - 1].position.x, _arenaYpos * enemySacle, _spawnCandidates[_spawnCandidates.Count - 1].position.z);
        }
        else
        {
            winner = _spawnCandidates[0];
            winner.distances.Sort(delegate (DistancePriority x, DistancePriority y)
            {
                if (x.distance > y.distance) return 1;
                else if (x.distance < y.distance) return -1;
                else return 0;
            });
            for (int i = 1; i < _spawnCandidates.Count; i++)
            {
                _spawnCandidates[i].distances.Sort(delegate (DistancePriority x, DistancePriority y)
                {
                    if (x.distance > y.distance) return 1;
                    else if (x.distance < y.distance) return -1;
                    else return 0;
                });
                if (_spawnCandidates[i].distances[0].distance > winner.distances[0].distance && !_spawnCandidates[i].distances[0].hasPriority) winner = _spawnCandidates[i];
            }
        }
        Vector3 randomPos = new Vector3(winner.position.x, _arenaYpos* enemySacle, winner.position.z);
        return randomPos;
    }
    public void ReturnAllEnemiesToPool()
    {
        Debug.Log("ret all"+_allEnemies.Count);
        foreach(NormalEnemy enemy in _allEnemies)
        {
            enemy.OnDeath.RemoveAllListeners();
            if(enemy.gameObject.activeSelf) enemy.ReturnToPool();

        }
        _transformsToAvoidSpawn.Clear();
        _spawnedEnemies.Clear();
    }
    public void SetAllEnemyScript(bool value)
    {
        foreach (NormalEnemy enemy in _allEnemies)
        {
            enemy.enabled = value;
        }
    }
    public void UpdateAvoidTransforms(Transform trans,float distance,bool remove)
    {
        if(remove)
        {
            TransformToAvoid tr= _transformsToAvoidSpawn.Find((x) => x.transform == trans);
            _transformsToAvoidSpawn.Remove(tr);
        }
        else
        {
            TransformToAvoid tr = new TransformToAvoid()
            {
                transform = trans,
                distance = distance,
                hasPiority = false
            };
            _transformsToAvoidSpawn.Add(tr);
        }
    }
    private void OnEnemyDeath(Enemy en)
    {
        en.OnDeath.RemoveListener(OnEnemyDeath);
        _spawnedEnemies.Remove(en as NormalEnemy);
        TransformToAvoid tr= _transformsToAvoidSpawn.Find((x) => x.transform == (en as NormalEnemy).Rigidbody.transform);
        _transformsToAvoidSpawn.Remove(tr);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(new Vector3(0, _arenaYpos, 0), new Vector3(2*_spawnRange, 2*_spawnRange, 2*_spawnRange));
        Gizmos.color = Color.green;
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
