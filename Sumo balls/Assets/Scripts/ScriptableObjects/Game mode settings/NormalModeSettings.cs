using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Normal")]
public class NormalModeSettings : StandardGameModeSettings
{
    public float SimultaneouslNumberOfEnemies => _simultaneouslNumberOfEnemies;
    public float NumberOfEnemiesToDefeat => _numberOfEnemiesToDefeat;
    public float PowerUpSpawnRateInSeconds => _powerUpSpawnrateInSeconds;
    public bool IsInCage => _isInCage;
    public List<float> TimeRequiredForStar => _timeRequiredForStar;
    public List<int> WallsRequiredForStar=>_wallsRequiredForStar;
    [SerializeField] bool _isInCage;
    [SerializeField] float _simultaneouslNumberOfEnemies;
    [SerializeField] float _numberOfEnemiesToDefeat;
    [SerializeField] float _powerUpSpawnrateInSeconds;
    [SerializeField] List<float> _timeRequiredForStar = new List<float> { 0, 0, 0 };
    [SerializeField] List<int> _wallsRequiredForStar = new List<int> { 0, 0, 0 };
    private void Awake()
    {
        _gameMode = Configs.Gamemode.NORMAL;
    }
    public override string GetDetailedDescription()
    {
        string s = $"Simultaneousl number of enemies: {SimultaneouslNumberOfEnemies}\n" +
                   $"Spawn rate of power up: one per {PowerUpSpawnRateInSeconds}\n";
        return s;
    }
    public override string GetDescription()
    {
        string s = $"Defeat {NumberOfEnemiesToDefeat} enemies";
        return s;
    }

    public override List<string> GetStarsDescription()
    {
        List<string> toReturn;
        
        if (_isInCage) toReturn = new List<string> { $"Destroy lass than {_wallsRequiredForStar[0]} walls", $"Destroy lass than {_wallsRequiredForStar[1]} walls", $"Destroy lass than {_wallsRequiredForStar[2]} walls" };
        else toReturn = new List<string> { $"Beat stage in {_timeRequiredForStar[0]} seconds", $"Beat stage in {_timeRequiredForStar[1]} seconds", $"Beat stage in {_timeRequiredForStar[2]} seconds" };
        return toReturn;
    }
}
