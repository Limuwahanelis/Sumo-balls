using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameModeSettings:ScriptableObject
{
    [SerializeField] protected Configs.Gamemode _gameMode;
    [SerializeField] protected bool _areEnemiesRandomized;
    

    
    public Configs.Gamemode GameMode =>_gameMode;
    public bool AreEnemiesRandomized => _areEnemiesRandomized;

    public abstract string GetDetailedDescription();
    public abstract string GetDescription();

    public abstract List<string> GetStarsDescription();

}
