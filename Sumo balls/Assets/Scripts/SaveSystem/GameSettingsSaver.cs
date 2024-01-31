using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsSaver : MonoBehaviour
{
    public void Save(bool value)
    {
        SaveGameSettings.SaveSettings(value);
    }
}
