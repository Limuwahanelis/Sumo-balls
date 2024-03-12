using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class HoleInArenaPool : MonoBehaviour
{
    [SerializeField, SearchContext("t:HoleInArena")] HoleInArena _holePrefab;
    private ObjectPool<HoleInArena> _holePool;

    // Start is called before the first frame update
    void Awake()
    {
        _holePool = new ObjectPool<HoleInArena>(CreateHole, OnTakeHoleFromPool, OnReturnHoleToPool);
    }

    public HoleInArena GetHole()
    {
        return _holePool.Get();
    }
    HoleInArena CreateHole()
    {
        HoleInArena hole = Instantiate(_holePrefab);
        hole.SetPool(_holePool);
        return hole;

    }
    public void OnTakeHoleFromPool(HoleInArena enemy)
    {
        //enemy.gameObject.SetActive(true);
    }
    public void OnReturnHoleToPool(HoleInArena hole)
    {
    }
}
