using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] Toggle _fastLoadToggle;
    private void OnEnable()
    {
        GameSettingsData configs = GameSettingsSaver.Load();
        _fastLoadToggle.isOn = configs.fastLoad;
    }
}
