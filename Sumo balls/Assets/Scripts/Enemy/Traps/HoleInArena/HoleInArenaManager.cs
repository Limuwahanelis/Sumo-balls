using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class HoleInArenaManager : MonoBehaviour
{
    public float MaxRadius=>_holeInArena.MaxRadius;
    public UnityEvent<HoleInArenaManager> OnHoleCycleCompleted;
    private IObjectPool<HoleInArenaManager> _pool;
    [SerializeField] HoleInArena _holeInArena;
    [SerializeField] bool _debug;
    [SerializeField] HoleInArenaSettings _holeInArenaSettings;
    private void Awake()
    {
        _holeInArena.OnHoleCycleCompleted.AddListener(OnHoleCompletedCycle);
    }
    // Start is called before the first frame update
    void Start()
    {
        if(_debug)
        {
            SetUpHole(_holeInArenaSettings.MaxHoleMaxRadius, _holeInArenaSettings.TimeToReachMaxRadius, _holeInArenaSettings.TimeToStayAtMaxRadius, _holeInArenaSettings.TimeToBeginGrowth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpHole(float maxHoleRadius, float timeToGetToMaxSize, float timeToStayAtMaxSize, float timeToBeginGrow)
    {
        _holeInArena.SetUp(maxHoleRadius, timeToGetToMaxSize, timeToStayAtMaxSize, timeToBeginGrow);
    }
    public void OnHoleCompletedCycle(HoleInArena hole)
    {
        OnHoleCycleCompleted?.Invoke(this);
        if(_debug) SetUpHole(_holeInArenaSettings.MaxHoleMaxRadius, _holeInArenaSettings.TimeToReachMaxRadius, _holeInArenaSettings.TimeToStayAtMaxRadius, _holeInArenaSettings.TimeToBeginGrowth);
    }
    public void SetPool(IObjectPool<HoleInArenaManager> pool) => _pool = pool;
    public void ReturnToPool() => _pool.Release(this);
    private void OnDestroy()
    {
        _holeInArena.OnHoleCycleCompleted.RemoveListener(OnHoleCompletedCycle);
    }
}
