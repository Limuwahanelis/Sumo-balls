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
        GameSettingsData configs = SaveGameSettings.GetGameSettings();
        _fastLoadToggle.isOn = configs.fastLoad;
    }
}
