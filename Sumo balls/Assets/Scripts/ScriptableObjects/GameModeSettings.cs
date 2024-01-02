using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSettings:ScriptableObject
{
    [SerializeField] protected Configs.Gamemode _gameMode;
    public Configs.Gamemode GameMode =>_gameMode;
}
