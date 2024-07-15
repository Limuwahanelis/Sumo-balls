using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicSetUp : MonoBehaviour
{
    private void Awake()
    {
        GraphicSettingsData data = GraphicSettingsSaver.LoadGraphicSettings();
        if (data == null)
        {
            data = new GraphicSettingsData(2);
            GraphicSettingsSaver.SaveGraphicSettings(data);
        }
        QualitySettings.SetQualityLevel(data.qualitySettingsIndex);
    }
}
