using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSettingsData
{
    public bool fastLoad;
    public bool speedBar;
    public GameSettingsData(bool fastLoad,bool speedBar)
    {
        this.fastLoad = fastLoad;
        this.speedBar = speedBar;
    }

    public GameSettingsData()
    {
        fastLoad = false;
        speedBar = true;
    }
}
