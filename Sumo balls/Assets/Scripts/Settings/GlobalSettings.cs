using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSettings
{
    public static bool IsGamePaused=>_isGamePaused;
    private static bool _isGamePaused;

    //public static GameModeSettings SelectedGameModeSettings => _selectedGameModeSettings;
    //private static GameModeSettings _selectedGameModeSettings;
    private static Stage _selectedStage;
    public static Stage SelectedStage => _selectedStage;

    public static int StageIndex=>_stageIndex;
    public static string StageID => _stageID;
    private static string _stageID;
    private static int _stageIndex;
    public static void SetPause(bool value)
    {
        _isGamePaused = value;
        if (value) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public static void SetStage(Stage stage,string stageID,int stageIndex)
    {
        _selectedStage = stage;
        _stageIndex = stageIndex;
        _stageID = stageID;
    }
}
