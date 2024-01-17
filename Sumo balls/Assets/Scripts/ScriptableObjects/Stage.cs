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
    public int Score => _score;
    public bool IsCompleted=>_isCompleted;
    [SerializeField] GameModeSettings _gameMode;
    [SerializeField, SearchContext("p: dir=\"StageScreens\"")] Texture _stageScreenshot;
    private int _score=0;
    private bool _isCompleted;

    public void SetScore(int score)
    {
        _isCompleted = true;
        _score = score;
    }
}
