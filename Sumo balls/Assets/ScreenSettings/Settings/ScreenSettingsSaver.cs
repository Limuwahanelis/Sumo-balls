using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettingsSaver : MonoBehaviour
{
    [SerializeField] ScreenSettings _screenSettings;

    public void SaveScreenSettings()
    {
        SaveSystem.SaveScreenSettings.SaveScreenConfigs(_screenSettings.selectedResolution, _screenSettings.fullScreen);
    }
}
