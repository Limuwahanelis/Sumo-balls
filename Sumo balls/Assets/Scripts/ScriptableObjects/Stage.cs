using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Search;

[CreateAssetMenu(menuName ="Stage")]
[Serializable]
public class Stage : ScriptableObject
{
    public string Id { get { return _id; } private set { _id = value; } }
    [SerializeField] private string _id;
    public bool IsInitalised { get { return _isInitalised; } private set { _isInitalised = value; } }
    [SerializeField] private bool _isInitalised;

    public GameModeSettings GameModeSettings => _gameMode;
    public Texture stageScreenshot => _stageScreenshot;

    [SerializeField] GameModeSettings _gameMode;
    [SerializeField, SearchContext("p: dir=\"StageScreens\"")] Texture _stageScreenshot;

    private void Reset()
    {
        Init();
    }
    private void Init()
    {
        Debug.Log("in");

#if UNITY_EDITOR

        if (!_isInitalised)
        {
            Id = GUID.Generate().ToString();
            _isInitalised = true;
        }
#endif
        if (!_isInitalised)
        {
            Debug.LogError("Item was not initialised !");
        }
    }
}
