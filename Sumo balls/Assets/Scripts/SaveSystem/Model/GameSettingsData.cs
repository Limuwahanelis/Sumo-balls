using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsData
{
    public bool fastLoad;
    public GameSettingsData(bool fastLoad)
    {
        this.fastLoad = fastLoad;
    }

    public GameSettingsData()
    {
        fastLoad = false;
    }
}
