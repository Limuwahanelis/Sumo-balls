using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameModeSettings:ScriptableObject
{
    [SerializeField] protected Configs.Gamemode _gameMode;
    public Configs.Gamemode GameMode =>_gameMode;

    public abstract string GetDetailedDescription();
    public abstract string GetDescription();
}
