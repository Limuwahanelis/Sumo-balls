using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] Toggle _fastLoadToggle;
    [SerializeField] Toggle _showSpeedBarToggle;
    [SerializeField] Toggle _showEnemyBeltsToggle;
    private void OnEnable()
    {
        GameSettingsData configs = GameSettingsSaver.Load();
        _fastLoadToggle.isOn = configs.fastLoad;
        _showSpeedBarToggle.isOn = configs.speedBar;
        _showEnemyBeltsToggle.isOn = configs.enemyBelts;
    }
}
