using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Normal")]
public class NormalModeSettings : GameModeSettings
{
    public float SimultaneouslNumberOfEnemies => _simultaneouslNumberOfEnemies;
    public float NumberOfEnemiesToDefeat => _numberOfEnemiesToDefeat;

    [SerializeField] float _simultaneouslNumberOfEnemies;
    [SerializeField] float _numberOfEnemiesToDefeat;

    private void Awake()
    {
        _gameMode = Configs.Gamemode.NORMAL;
    }

}
