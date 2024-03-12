using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Holes in arena settings")]
public class HoleInArenaSettings : ScriptableObject
{
    public float ArenaHeight => _arenaHeight;
    public float MinMaxHoleRadius => _minHoleMaxRadius;
    public float MaxHoleMaxRadius => _maxHoleMaxRadius;
    public float TimeToReachMaxRadius => _timeToReachMaxRadius;
    public float TimeToStayAtMaxRadius => _timeToStayAtMaxRadius;
    public float TimeToBeginGrowth => _timeToBeginGrowth;
    public int NumberOfConcurrentHoles => _numberOfConcurrentHoles;

    [SerializeField] float _arenaHeight;
    [SerializeField] float _minHoleMaxRadius;
    [SerializeField] float _maxHoleMaxRadius;
    [SerializeField] float _timeToReachMaxRadius;
    [SerializeField] float _timeToStayAtMaxRadius;
    [SerializeField] float _timeToBeginGrowth;
    [SerializeField] int _numberOfConcurrentHoles;
}
