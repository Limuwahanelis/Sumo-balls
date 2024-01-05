using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

[CreateAssetMenu(menuName ="Stage")]
[Serializable]
public class Stage : ScriptableObject
{
    public GameModeSettings GameModeSettings => _gameMode;
    public Texture stageScreenshot => _stageScreenshot;
    [SerializeField] GameModeSettings _gameMode;
    [SerializeField, SearchContext("p: dir=\"StageScreens\"")] Texture _stageScreenshot;
}
