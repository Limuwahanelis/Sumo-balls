using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpGameSettings : MonoBehaviour
{
    [SerializeField] BoolValue _fastLoad;
    // Start is called before the first frame update
    void Awake()
    {
        if (SaveGameSettings.GetGameSettings() == null)
        {
            Debug.Log("game configs not found");
            GameSettingsData newData = new GameSettingsData();
            SaveGameSettings.SaveSettings(newData);
            _fastLoad.value = newData.fastLoad;
        }
        else
        {
            Debug.Log("got confs");
            GameSettingsData configs = SaveGameSettings.GetGameSettings();
            _fastLoad.value = configs.fastLoad;
        }
    }
}
