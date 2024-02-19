using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsSaver : MonoBehaviour
{
    public static readonly string fileName = "configs";
    [SerializeField] BoolValue _isLevelFastLoad;
    [SerializeField] BoolValue _isSpeedBarDisplayed;
    public void Save()
    {
        //SaveGameSettings.SaveSettings(value);
        GameSettingsData gameSettingsData = new GameSettingsData(_isLevelFastLoad.value,_isSpeedBarDisplayed.value);
        JsonSave.SaveToFile(gameSettingsData, fileName);
    }

    public static void Save(GameSettingsData gameSettingsData)
    {
        JsonSave.SaveToFile(gameSettingsData, fileName);
    }

    public static GameSettingsData Load()
    {
        return JsonSave.GetDataFromJson<GameSettingsData>(fileName);
    }
}
