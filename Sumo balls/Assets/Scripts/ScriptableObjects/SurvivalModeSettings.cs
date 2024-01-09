using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Survival")]
public class SurvivalModeSettings : GameModeSettings
{
    public float TimeToSurvive => _timeToSurvive;
    public float StartingNumberOfEnemies => _startingNumberOfEnemies;
    public float SecondsToSpawnEnemies => _secondsToSpawnEnemies;
    public float NumbeOfEnemiesToSpawn => _numberOfEnemiesToSpawn;
    public float TimeToSpawnPowerUp => _timeToSpawnPowerUP;

    [SerializeField] float _timeToSurvive;
    [SerializeField] int _startingNumberOfEnemies;
    [SerializeField] float _secondsToSpawnEnemies;
    [SerializeField] float _numberOfEnemiesToSpawn;
    [SerializeField] float _timeToSpawnPowerUP;

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
}
