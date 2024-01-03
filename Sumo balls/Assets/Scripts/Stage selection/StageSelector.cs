using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageSelector : MonoBehaviour
{
    private List<Stage> stages = new List<Stage>();
    [SerializeField] LoadScene _sceneLoader;
    [SceneName, SerializeField] private string _survivalScene;
    [SceneName, SerializeField] private string _normalScene;
    private void Awake()
    {
        stages = GetComponentsInChildren<Stage>().ToList();
        foreach(Stage stage in stages)
        {
            stage.OnSelectGameMode += SetGameMode;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetGameMode(GameModeSettings settings)
    {
        GlobalSettings.SetGameMode(settings);
        switch(GlobalSettings.SelectedGameModeSettings.GameMode)
        {
            case Configs.Gamemode.NORMAL: _sceneLoader.Load(_normalScene);break;
            case Configs.Gamemode.SURVIVAL: _sceneLoader.Load(_survivalScene);break;
        }
    }
    private void OnDestroy()
    {
        foreach (Stage stage in stages)
        {
            stage.OnSelectGameMode -= SetGameMode;
        }
    }
}
