using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Survival")]
public class SurvivalModeSettings : StandardGameModeSettings
{
    public float TimeToSurvive => _timeToSurvive;
    public float StartingNumberOfEnemies => _startingNumberOfEnemies;
    public float SecondsToSpawnEnemies => _secondsToSpawnEnemies;
    public float NumbeOfEnemiesToSpawn => _numberOfEnemiesToSpawn;
    public float TimeToSpawnPowerUp => _timeToSpawnPowerUP;

    public List<int> EnemiesToDefeatForStar=>_enemiesToDefeatForStar;

    [SerializeField] float _timeToSurvive;
    [SerializeField] int _startingNumberOfEnemies;
    [SerializeField] float _secondsToSpawnEnemies;
    [SerializeField] float _numberOfEnemiesToSpawn;
    [SerializeField] float _timeToSpawnPowerUP;
    [SerializeField] List<int> _enemiesToDefeatForStar= new List<int> { 0,0,0};
    private void Awake()
    {
        _gameMode = Configs.Gamemode.SURVIVAL;
    }
    public override string GetDetailedDescription()
    {
        throw new System.NotImplementedException();
    }

    public override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public override List<string> GetStarsDescription()
    {
        List<string> toReturn = new List<string> { $"Defeat {_enemiesToDefeatForStar[0]} enemies", $"Defeat {_enemiesToDefeatForStar[1]} enemies", $"Defeat {_enemiesToDefeatForStar[2]} enemies" };
        return toReturn;
    }

}
