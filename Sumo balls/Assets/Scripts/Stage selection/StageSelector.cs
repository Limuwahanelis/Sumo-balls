using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    [SerializeField] LoadScene _sceneLoader;
    [SerializeField] StageInGrid _stagePrefab;
    [SceneName, SerializeField] private string _survivalScene;
    [SceneName, SerializeField] private string _normalScene;
    [SerializeField] StageList _stageList;
    [SerializeField]private List<StageInGrid> _stagesInGrid = new List<StageInGrid>();
    private void Awake()
    {
        _stagesInGrid = GetComponentsInChildren<StageInGrid>().ToList();
        foreach(StageInGrid stage in _stagesInGrid)
        {
            stage.OnSelectStage += SetStage;
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
    private void SetStage(Stage stage)
    {
        //GlobalSettings.SetStage(stage.GameModeSettings);
        GlobalSettings.SetStage(stage, _stageList.stages.IndexOf(stage));
        switch (GlobalSettings.SelectedStage.GameModeSettings.GameMode)
        {
            case Configs.Gamemode.NORMAL: _sceneLoader.Load(_normalScene);break;
            case Configs.Gamemode.SURVIVAL: _sceneLoader.Load(_survivalScene);break;
            case Configs.Gamemode.SPECIAL:_sceneLoader.Load((GlobalSettings.SelectedStage.GameModeSettings as SpecialGameModeSettings).StageScene);break;

        }
    }
    private void OnDestroy()
    {
        foreach (StageInGrid stage in _stagesInGrid)
        {
            stage.OnSelectStage -= SetStage;
        }
    }
}
