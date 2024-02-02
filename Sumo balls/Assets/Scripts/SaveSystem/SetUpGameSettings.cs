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
        if (GameSettingsSaver.Load()== null)
        {
            Debug.Log("game configs not found");
            GameSettingsData newData = new GameSettingsData();
            GameSettingsSaver.Save(newData);
            _fastLoad.value = newData.fastLoad;
        }
        else
        {
            Debug.Log("got confs");
            GameSettingsData configs = GameSettingsSaver.Load();
            _fastLoad.value = configs.fastLoad;
        }
    }
}
