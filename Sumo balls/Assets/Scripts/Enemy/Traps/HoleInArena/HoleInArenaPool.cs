using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class HoleInArenaPool : MonoBehaviour
{
    [SerializeField, SearchContext("t:HoleInArenaManager")] HoleInArenaManager _holePrefab;
    private ObjectPool<HoleInArenaManager> _holePool;

    // Start is called before the first frame update
    void Awake()
    {
        _holePool = new ObjectPool<HoleInArenaManager>(CreateHole, OnTakeHoleFromPool, OnReturnHoleToPool);
    }

    public HoleInArenaManager GetHole()
    {
        return _holePool.Get();
    }
    HoleInArenaManager CreateHole()
    {
        HoleInArenaManager hole = Instantiate(_holePrefab);
        hole.SetPool(_holePool);
        return hole;

    }
    public void OnTakeHoleFromPool(HoleInArenaManager enemy)
    {
        //enemy.gameObject.SetActive(true);
    }
    public void OnReturnHoleToPool(HoleInArenaManager hole)
    {
        hole.gameObject.SetActive(false);
    }
}
