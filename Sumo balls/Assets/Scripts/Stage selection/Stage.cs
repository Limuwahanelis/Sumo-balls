using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Action<GameModeSettings> OnSelectGameMode;
    [SerializeField] GameModeSettings _gameModeSettings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectGameModeSettings()
    {
        OnSelectGameMode?.Invoke(_gameModeSettings);
    }
}
