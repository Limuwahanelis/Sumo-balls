using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSettings
{
    public static bool IsGamePaused=>_isGamePaused;
    private static bool _isGamePaused;
    public static GameModeSettings SelectedGameModeSettings => _selectedGameModeSettings;
    private static GameModeSettings _selectedGameModeSettings;

    public static int StateIndex=>_stageIndex;
    private static int _stageIndex;
    public static void SetPause(bool value)
    {
        _isGamePaused = value;
        if (value) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public static void SetStage(GameModeSettings gameModeSettings,int stageIndex)
    {
        _selectedGameModeSettings = gameModeSettings;
        _stageIndex = stageIndex;
    }
}
