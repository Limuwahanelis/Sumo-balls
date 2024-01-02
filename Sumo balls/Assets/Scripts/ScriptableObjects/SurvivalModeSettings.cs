using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Survival")]
public class SurvivalModeSettings : GameModeSettings
{
    public float timeToSurvive;
    public int startingNumberOfEnemies;
    public int finalNumberOfEnemies;
    public float secondsToIncreaseEnemyCount; 

    private void Awake()
    {
        _gameMode = Configs.Gamemode.SURVIVAL;
    }
}
