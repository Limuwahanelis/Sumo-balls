using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    [SerializeField] StageList _stageList;
    [SerializeField] bool _overrideSave;
    [SerializeField] IntReference _points;
    [SerializeField] Material _playerMat;
    [SerializeField] List<UnlockableItem> _unlockableColors;
    // Start is called before the first frame update
    void Start()
    {
        LoadOrCreateNewSave();
        _playerMat.color = GameDataManager.GameData.customizationData.playerColor;
        GetPoints();
    }
    private void GetPoints()
    {
        int points = 0;
        foreach (StageData data in GameDataManager.GameData.stagesData)
        {
            points+=data.score;
        }
        for (int i = 0; i < GameDataManager.GameData.customizationData.unlockableItemsData.Count; i++)
        {
            UnlockableItemData itemData = GameDataManager.GameData.customizationData.unlockableItemsData[i];
            if (_unlockableColors[i].Id == itemData.itemId && itemData.isUnlocked) points -= _unlockableColors[i].Cost;
        }
        _points.value = points;
    }
    private void LoadOrCreateNewSave()
    {
        if (GameDataManager.LoadGameData() == false || _overrideSave)
        {
            Debug.Log("Creating new save");
            GameDataManager.CreateGameData(_stageList.stages, _unlockableColors);
        }
        else
        {
            GameDataManager.VerifyGameData(_stageList.stages, _unlockableColors);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
