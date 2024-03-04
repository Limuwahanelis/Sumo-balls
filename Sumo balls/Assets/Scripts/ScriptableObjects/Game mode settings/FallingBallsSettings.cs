using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Falling balls settings")]
public class FallingBallsSettings : ScriptableObject
{
    public float PlayerScale=>_playerScale.value;
    public float ArenaHeight => _arenaHeight;
    public float MinSpawnHeight=>_minSpawnHeight;
    public float MaxSpawnHeight=>_maxSpawnHeight;
    public float MinBallSize => _minBallSize;
    public float MaxBallSize => _maxBallSize;
    public float MinBallSpeed=>_minBallSpeed;
    public float MaxBallSpeed=>_maxBallSpeed;
    public int NumberOfConcurrentFallingBalls=>_numberOfConcurrentFallingBalls;
    public float TimeToSpawnNewBall=>_timeToSpawnNewBall;

    [SerializeField] FloatReference _playerScale;
    [SerializeField] float _arenaHeight;
    [SerializeField] float _minSpawnHeight;
    [SerializeField] float _maxSpawnHeight;
    [SerializeField] float _minBallSize;
    [SerializeField] float _maxBallSize;
    [SerializeField] float _minBallSpeed;
    [SerializeField] float _maxBallSpeed;
    [SerializeField] float _timeToSpawnNewBall;
    [SerializeField] int _numberOfConcurrentFallingBalls;

}
