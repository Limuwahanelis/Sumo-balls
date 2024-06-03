using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicSettingsSaver : MonoBehaviour
{
    public static readonly string graphicSettingsFileName = "graphicConfigs";
    [SerializeField] GraphicOptions _graphicSettings;

    public void SaveGraphicSettings()
    {
        GraphicSettingsData graphicData = new GraphicSettingsData(_graphicSettings.SelectedQualitySetting);
        JsonSave.SaveToFile(graphicData, graphicSettingsFileName);
    }
    public static GraphicSettingsData LoadGraphicSettings()
    {
        return JsonSave.GetDataFromJson<GraphicSettingsData>(graphicSettingsFileName);
    }
    public static void SaveGraphicSettings(GraphicSettingsData screenData)
    {
        JsonSave.SaveToFile(screenData, graphicSettingsFileName);
    }
}
