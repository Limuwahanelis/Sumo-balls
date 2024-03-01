using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpGameSettings : MonoBehaviour
{
    [SerializeField] BoolValue _fastLoad;
    [SerializeField] BoolValue _showSpeedBar;
    [SerializeField] BoolValue _showEnemyBelts;
    // Start is called before the first frame update
    void Awake()
    {
        if (GameSettingsSaver.Load()== null)
        {
            Debug.Log("game configs not found");
            GameSettingsData newData = new GameSettingsData();
            GameSettingsSaver.Save(newData);
            _fastLoad.value = newData.fastLoad;
            _showSpeedBar.value = newData.speedBar;
            _showEnemyBelts.value = newData.enemyBelts;
        }
        else
        {
            Debug.Log("got confs");
            GameSettingsData configs = GameSettingsSaver.Load();
            _fastLoad.value = configs.fastLoad;
            _showSpeedBar.value = configs.speedBar;
            _showEnemyBelts.value=configs.enemyBelts;
        }
    }
}
