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
        _playerMat.color = SaveGameData.GameData.customizationData.playerColor;
        for(int i=0;i< SaveGameData.GameData.customizationData.unlockableItemsData.Count;i++)
        {
            UnlockableItemData itemData = SaveGameData.GameData.customizationData.unlockableItemsData[i];
            if (_unlockableColors[i].Id == itemData.itemId && itemData.isUnlocked) _unlockableColors[i].Unlock();
        }
        GetPoints();
    }
    private void GetPoints()
    {
        int points = 0;
        foreach (StageData data in SaveGameData.GameData.stagesData)
        {
            points+=data.score;
        }
        for (int i = 0; i < SaveGameData.GameData.customizationData.unlockableItemsData.Count; i++)
        {
            UnlockableItemData itemData = SaveGameData.GameData.customizationData.unlockableItemsData[i];
            if (_unlockableColors[i].Id == itemData.itemId && itemData.isUnlocked) points -= _unlockableColors[i].Cost;
        }
        _points.value = points;
    }
    private void LoadOrCreateNewSave()
    {
        if (SaveGameData.LoadGameData() == false || _overrideSave)
        {
            SaveGameData.CreateGameData(_stageList.stages, _unlockableColors);
            int i = 0;
            foreach (StageData data in SaveGameData.GameData.stagesData)
            {
                if (data.completed) _stageList.stages[i].SetScore(0);
                i++;
            }
        }
        else
        {
            int i = 0;
            foreach (StageData data in SaveGameData.GameData.stagesData)
            {
                if (data.completed) _stageList.stages[i].SetScore(data.score);
                i++;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
