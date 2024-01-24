using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(menuName = "Game mode/Special")]
public class SpecialGameModeSettings:GameModeSettings
{
    public string StageScene=>_stageScene;
    [SceneName, SerializeField] private string _stageScene;
    [SerializeField] List<string> _starsDescriptions = new List<string>() { "", "", "" };


    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override string GetDetailedDescription()
    {
        throw new System.NotImplementedException();
    }

    public override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public override List<string> GetStarsDescription()
    {
        return _starsDescriptions;
    }
}
