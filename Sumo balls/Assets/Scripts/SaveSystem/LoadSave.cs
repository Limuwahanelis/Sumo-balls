using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    [SerializeField] StageList _stageList;
    [SerializeField] bool _overrideSave;
    [SerializeField] Material _playerMat;
    [SerializeField] List<UnlockableItem> _unlockableColors;
    [SerializeField] List<CosmeticSOList> _allCosmeticsSOLists;
    private List<CosmeticSO> _allCosmeticsSO=new List<CosmeticSO>();
    private List<UnlockableItem> _allCosmeticsAsUnlockableItems=new List<UnlockableItem>();
    private List<UnlockableItem> _allUnlockables;
    [SerializeField] CosmeticsSettings _cosmeticSettings;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in _allCosmeticsSOLists)
        {
            foreach (var cosmetic in item.Cosmetics)
            {
                _allCosmeticsAsUnlockableItems.Add(cosmetic);
                _allCosmeticsSO.Add(cosmetic);
            }
            
        }
        _allUnlockables = new List<UnlockableItem>().Concat(_unlockableColors).Concat(_allCosmeticsAsUnlockableItems).ToList();
        //foreach(UnlockableItem it in _allUnlockables)
        //{
        //    Debug.Log(it.Id);
        //}
        LoadOrCreateNewSave();
        _playerMat.color = GameDataManager.GameData.customizationData.playerColor;
        _cosmeticSettings.InitateList(_allCosmeticsSO.Find(x => x.Id == GameDataManager.GameData.customizationData.topCosmeticId), _allCosmeticsSO.Find(x => x.Id == GameDataManager.GameData.customizationData.midddleCosmeticId), _allCosmeticsSO.Find(x => x.Id == GameDataManager.GameData.customizationData.bottomCosmeticId));
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
            if (_allUnlockables[i].Id == itemData.itemId && itemData.isUnlocked) points -= _allUnlockables[i].Cost;
        }
        GameDataManager.IncreasePoints(points);
    }
    private void LoadOrCreateNewSave()
    {
        if (GameDataManager.LoadGameData() == false || _overrideSave)
        {
            Debug.Log("Creating new save");
            GameDataManager.CreateGameData(_stageList.stages, _allUnlockables,_allCosmeticsSO);
        }
        else
        {
            GameDataManager.VerifyGameData(_stageList.stages, _allUnlockables, _allCosmeticsSO,GameDataManager.GameData.customizationData);
        }
    }
}
