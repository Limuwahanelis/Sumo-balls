using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSettingsData
{
    public bool fastLoad;
    public bool speedBar;
    public bool enemyBelts;
    public GameSettingsData(bool fastLoad,bool speedBar,bool enemBelts)
    {
        this.fastLoad = fastLoad;
        this.speedBar = speedBar;
        this.enemyBelts = enemBelts;
    }

    public GameSettingsData()
    {
        fastLoad = false;
        speedBar = true;
        enemyBelts = true;
    }
}
