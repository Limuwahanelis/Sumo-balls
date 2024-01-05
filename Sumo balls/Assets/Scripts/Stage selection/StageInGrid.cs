using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StageInGrid : MonoBehaviour
{
    public Action<Stage> OnSelectStage;
    [SerializeField] Stage _stage;
    [SerializeField] TMP_Text _indexTextField;
    [SerializeField] RawImage _stageScreen;
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
        OnSelectStage?.Invoke(_stage);
    }
    public void SetStage(Stage stage)
    {
        _stage = stage;
    }
    public void SetIndex(int index)
    {
        _indexTextField.text = index.ToString();
    }

    public void SetStageIcon(Texture textire)
    {
        _stageScreen.texture = textire;
    }
}
