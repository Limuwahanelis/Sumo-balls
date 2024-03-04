using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingBallsSpawner : MonoBehaviour
{
    [SerializeField] FallingBallPool _pool;
    [SerializeField] float _playerScale;
    [SerializeField] float _arenaHeight;
    [SerializeField] float _minHeight;
    [SerializeField] float _maxHeight;
    [SerializeField] float _minSize;
    [SerializeField] float _maxSize;
    [SerializeField] float _minSpeed;
    [SerializeField] float _maxSpeed;
    [SerializeField] int _numberOfContinousFallingBalls;
    [SerializeField] float _timeToSpawnFallingBall;
    [SerializeField] HexagonSpawn _hexagonSpawn;
    [SerializeField] TimeCounter _timeCounter;

    [SerializeField] GameObject _debugSphere;
    [SerializeField] GameObject _debugCube;

    private List<FallingBall> _allfallingBalls = new List<FallingBall>();
    private List<FallingBall> _fallingBalls = new List<FallingBall>();
    private List<Spawncandidate> _candidatesToSpawn=new List<Spawncandidate>();

    private bool _isSpawningBalls;
    private bool _isSetUp;

    private struct Spawncandidate
    {
        public Vector3 position;
        public List<float> distances;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isSpawningBalls) return;
        if(_timeCounter.CurrentTime>=_timeToSpawnFallingBall)
        {
            if (_fallingBalls.Count<=_numberOfContinousFallingBalls)
            {
                FallingBall ball= _pool.GetFallingBall();
                
                float ballScale = Random.Range(_minSize, _maxSize);
                Vector3 ballSpawnPos = SelectSpawnPos(ballScale);
                ballSpawnPos.y = Random.Range(_minHeight, _maxHeight);
                ball.SetUp(Random.Range(_minSpeed, _maxSpeed), ballScale, ballSpawnPos,_arenaHeight, _playerScale/2);
                ball.gameObject.SetActive(true);
                ball.OnHitFloorAndDisappeared.AddListener(OnBallDisappear);
                _fallingBalls.Add(ball);
                if (!_allfallingBalls.Contains(ball)) _allfallingBalls.Add(ball);
                _timeCounter.ResetTimer();
            }
        }
        
    }
    public void SetSpawning(bool value)
    {
        if(!_isSetUp)
        {
            enabled = false;
            return;
        }
        _timeCounter.SetCountTime(value);
        _isSpawningBalls = value;
    }
    public void SetSpawnParameters(FallingBallsSettings settings)
    {
        _arenaHeight = settings.ArenaHeight;
        _minHeight = settings.MinSpawnHeight;
        _maxHeight = settings.MaxSpawnHeight;
        _minSize = settings.MinBallSize;
        _maxSize = settings.MaxBallSize;
        _minSpeed = settings.MinBallSpeed;
        _maxSpeed = settings.MaxBallSpeed;
        _numberOfContinousFallingBalls = settings.NumberOfConcurrentFallingBalls;
        _timeToSpawnFallingBall = settings.TimeToSpawnNewBall;
        _playerScale = settings.PlayerScale;
        _isSetUp = true;
    }
    public void ClearSpawn()
    {
        for(int i= _fallingBalls.Count-1; i>=0; i--)
        {
            OnBallDisappear(_fallingBalls[i]);
        }
        _timeCounter.ResetTimer();

    }
    Vector3 SelectSpawnPos(float enemyScale)
    {
        _candidatesToSpawn.Clear();
        Vector3 enemySpawnPos = _hexagonSpawn.GetAvilablePosition();
        bool isSelectingSpawnpos = true;
        int selectiontries = 1;
        bool canSpawn = true;
        Spawncandidate winner;
        while (isSelectingSpawnpos)
        {
            canSpawn = true;
            Spawncandidate candidate = new Spawncandidate()
            {
                position = enemySpawnPos,
                distances = new List<float>()
            };
            _candidatesToSpawn.Add(candidate);
            for (int i = 0; i < _fallingBalls.Count; i++)
            {
                Vector3 posToCheck = _fallingBalls[i].GetXZPos();
                float distanceBetweenSpawnPoints = Vector3.Distance(enemySpawnPos, posToCheck);
                if (distanceBetweenSpawnPoints < _fallingBalls[i].GetScale() / 2 + enemyScale / 2)
                {
                    canSpawn = false;
                }
                candidate.distances.Add(distanceBetweenSpawnPoints);
            }
            if(!canSpawn)
            {
                selectiontries++;
                enemySpawnPos = _hexagonSpawn.GetAvilablePosition();
                
                if (selectiontries==5) isSelectingSpawnpos=false;
            }
            else isSelectingSpawnpos=false;
        }
        if (canSpawn) return _candidatesToSpawn[_candidatesToSpawn.Count - 1].position;
        else
        {
            winner = _candidatesToSpawn[0];
            winner.distances.Sort();
            for (int i = 1; i < _candidatesToSpawn.Count; i++)
            {
                //Instantiate(_debugCube, _candidatesToSpawn[i].position, _debugCube.transform.rotation);
                _candidatesToSpawn[i].distances.Sort();
                if (_candidatesToSpawn[i].distances[0] > winner.distances[0])
                {
                    winner = _candidatesToSpawn[i];
                }
            }
            //Instantiate(_debugSphere, winner.position, _debugSphere.transform.rotation);
        }
        return winner.position;
    }
    private void OnBallDisappear(FallingBall ball)
    {
        ball.OnHitFloorAndDisappeared.RemoveListener(OnBallDisappear);
        ball.ReturnToPool();
        _fallingBalls.Remove(ball);
    }
}
