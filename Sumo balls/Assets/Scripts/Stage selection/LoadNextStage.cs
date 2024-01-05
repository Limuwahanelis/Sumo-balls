using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextStage : MonoBehaviour
{
    [SerializeField] StageList _stageList;
    [SerializeField] LoadScene _sceneLoader;
    [SceneName, SerializeField] private string _survivalScene;
    [SceneName, SerializeField] private string _normalScene;
    public void PlayNextStage()
    {
        GlobalSettings.SetStage(_stageList.stages[GlobalSettings.StateIndex + 1].GameModeSettings, GlobalSettings.StateIndex + 1);
        switch (GlobalSettings.SelectedGameModeSettings.GameMode)
        {
            case Configs.Gamemode.NORMAL: _sceneLoader.Load(_normalScene); break;
            case Configs.Gamemode.SURVIVAL: _sceneLoader.Load(_survivalScene); break;
        }
    }
}
