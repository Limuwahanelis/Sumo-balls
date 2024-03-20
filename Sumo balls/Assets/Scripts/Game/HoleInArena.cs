using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Pool;

public class HoleInArena : MonoBehaviour
{
    public UnityEvent<HoleInArena> OnHoleCycleCompleted;
    public float MaxRadius => _context.holeMaxRadius;
    [SerializeField] TimeCounter _timeCounter;
    [SerializeField] ShadowQuad _shadow;
    [SerializeField] Collider _trigger;
    [SerializeField] Collider _wallCollider;
    [SerializeField,Layer] int _playerLayer;
    [SerializeField,Layer] int _enemyLayer;
    [SerializeField,Layer] int _playerIgnoreArenaMask;
    [SerializeField,Layer] int _enemyIgnoreArenaMask;
    [SerializeField] float _pullForce;
    [SerializeField] float _holeRadius;
    [SerializeField] float _timeToGetMaxSize;
    [SerializeField] float _timeToStayAtMaxSize;
    [SerializeField] float _timeToBeginGrow;
    private HoleInArenaState _currentHoleState;
    private Dictionary<Type, HoleInArenaState> _holeStates = new Dictionary<Type, HoleInArenaState>();
    private HoleInArenaContext _context;
    List<BallData> _ballsAtTheHole=new List<BallData>();
    Vector3 _holePos;
    private struct BallData
    {
        public Collider col;
        public float ballRadius;
        public bool isEnemy;
    }
    // Start is called before the first frame update
    void Awake()
    {
        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
    .Where(type => typeof(HoleInArenaState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        _context = new HoleInArenaContext()
        {
            timeCounter = _timeCounter,
            ChangeHoleState = ChangeCurrentState,
            GetStateType = GetStateFromDictionary,
            SetColliders = SetColliders,
            EndHoleCycle = EndHoleCycle,
            SetShadow= SetQuadVisibilty,
            hasEaten=false,
            holeMaxRadius = _holeRadius,
            timeToGetMaxToSize = _timeToGetMaxSize,
            timeToStayAtMaxSize = _timeToStayAtMaxSize,
            timeToBeginGrow = _timeToBeginGrow,
            SetHoleRadius=SetHoleRadius,
        };
        foreach (Type state in states)
        {
            _holeStates.Add(state, (HoleInArenaState)Activator.CreateInstance(state));
        }
    }
    public void SetUp(float maxHoleRadius, float timeToGetToMaxSize,float timeToStayAtMaxSize, float timeToBeginGrow)
    {
        _holeRadius = maxHoleRadius;
        _timeToGetMaxSize = timeToGetToMaxSize;
        _timeToStayAtMaxSize = timeToStayAtMaxSize;
        _timeToBeginGrow = timeToBeginGrow;
        _context.holeMaxRadius = _holeRadius;
        _context.timeToGetMaxToSize = timeToGetToMaxSize;
        _context.timeToStayAtMaxSize = timeToStayAtMaxSize;
        _context.timeToBeginGrow = timeToBeginGrow;
        _context.hasEaten = false;
        ChangeCurrentState( GetStateFromDictionary(typeof(DormantHoleInArenaState)));
        _currentHoleState.SetUpState(_context);
        _shadow.SetUp(maxHoleRadius * 2, maxHoleRadius*2 );
        _holePos = new Vector3(transform.position.x, 0, transform.position.z);
        _ballsAtTheHole.Clear();
        SetHoleRadius(0.01f);
    }
    // Update is called once per frame
    void Update()
    {
        _currentHoleState?.Update();
        for(int i= _ballsAtTheHole.Count-1; i>=0;i--)
        {
            BallData ball = _ballsAtTheHole[i];
            if (!ball.col.gameObject.activeInHierarchy)
            {
                if (ball.isEnemy) ball.col.gameObject.layer = _enemyLayer;
                else ball.col.gameObject.layer = _playerLayer;
                _ballsAtTheHole.Remove(ball);
                _context.hasEaten = true;
                continue;
            }
            ball.col.attachedRigidbody.AddForce(Vector3.down * _pullForce * Time.deltaTime);
        }
    }
    private void SetColliders(bool value)
    {
        _trigger.enabled = value;
        _wallCollider.enabled = value;
    }
    public HoleInArenaState GetStateFromDictionary(Type state)
    {
        return _holeStates[state];
    }
    private void ChangeCurrentState(HoleInArenaState newState)
    {
        _currentHoleState = newState;
        Debug.Log(_currentHoleState);
    }
    private void SetHoleRadius(float newRadius)
    {
        Vector3 newScale = new Vector3(newRadius * 2, transform.localScale.y, newRadius * 2);
        transform.localScale = newScale;
        _shadow.SetRadius(newRadius,true);
    }
    public void SetQuadVisibilty(bool value)
    {
        _shadow.gameObject.SetActive(value);
    }
    public void EndHoleCycle() => OnHoleCycleCompleted?.Invoke(this);

    private void OnTriggerEnter(Collider other)
    {
        if(!_ballsAtTheHole.Exists((x)=>x.col==other))
        {
            BallData ballData = new BallData();
            ballData.col = other.GetComponent<Collider>();
            ballData.isEnemy = other.attachedRigidbody.GetComponentInParent<Enemy>() ? true : false;
            ballData.ballRadius = other.transform.localScale.x/2;
            _ballsAtTheHole.Add(ballData);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        BallData ballToCheck = _ballsAtTheHole.Find((x) => x.col == other);
        Vector3 posTocheck = new Vector3(other.transform.position.x, 0, other.transform.position.z);
        Debug.Log(Vector3.Distance(_holePos, posTocheck));

       Debug.Log(Vector3.Distance(_holePos, posTocheck));
        if (Vector3.Distance(_holePos, posTocheck) < _holeRadius)
        {
            if(ballToCheck.isEnemy) other.attachedRigidbody.gameObject.layer = _enemyIgnoreArenaMask;
            else other.attachedRigidbody.gameObject.layer = _playerIgnoreArenaMask;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BallData ballToCheck = _ballsAtTheHole.Find((x) => x.col == other);
        if(ballToCheck.isEnemy) other.attachedRigidbody.gameObject.layer = _enemyLayer;
        else other.attachedRigidbody.gameObject.layer = _playerLayer;
        _ballsAtTheHole.Remove(ballToCheck);

    }
    private void OnValidate()
    {
        if (_holeRadius < 0) _holeRadius = 0;
        transform.localScale = new Vector3(_holeRadius*2,transform.localScale.y, _holeRadius*2);
    }
}
