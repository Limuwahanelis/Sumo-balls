using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Normal")]
public class NormalModeSettings : GameModeSettings
{
    public float SimultaneouslNumberOfEnemies => _simultaneouslNumberOfEnemies;
    public float NumberOfEnemiesToDefeat => _numberOfEnemiesToDefeat;
    public float PowerUpSpawnRateInSeconds => _powerUpSpawnrateInSeconds;
    [SerializeField] float _simultaneouslNumberOfEnemies;
    [SerializeField] float _numberOfEnemiesToDefeat;
    [SerializeField] float _powerUpSpawnrateInSeconds;

    private void Awake()
    {
        _gameMode = Configs.Gamemode.NORMAL;
    }

}
