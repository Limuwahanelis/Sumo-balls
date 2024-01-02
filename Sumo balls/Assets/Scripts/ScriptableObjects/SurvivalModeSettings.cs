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

    [SerializeField] float _timeToSurvive;
    [SerializeField] int _startingNumberOfEnemies;
    [SerializeField] float _secondsToSpawnEnemies;
    [SerializeField] float _numberOfEnemiesToSpawn;

    private void Awake()
    {
        _gameMode = Configs.Gamemode.SURVIVAL;
    }
}
