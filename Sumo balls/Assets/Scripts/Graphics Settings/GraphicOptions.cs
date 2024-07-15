using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GraphicOptions : MonoBehaviour
{
    public enum ShadowQualitySettings
    {
        LOW,MEDIUM,HIGH
    }
    public int SelectedQualitySetting => _selectedShqdowQualityIndex;
    private int _selectedShqdowQualityIndex;
    [SerializeField] TMP_Dropdown _shadowSettingsDropdown;
    private void Start()
    {
        GraphicSettingsData config = GraphicSettingsSaver.LoadGraphicSettings();
        _shadowSettingsDropdown.value = config.qualitySettingsIndex;
    }
    public void SetQuality(int index)
    {
        _selectedShqdowQualityIndex = index;
        QualitySettings.SetQualityLevel(index);
        QualitySettings.GetQualityLevel();
    }
}
