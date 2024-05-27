using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace SaveSystem
{
    public static class GameDataManager 
    {
        public static string gameSaveFolderPath = Application.dataPath + @"\save";
        public static string gameSaveFilePath = gameSaveFolderPath + @"\data.json";


        public static GameData GameData =>_gameData;
        private static GameData _gameData;
        public static int Points => _points;
        private static int _points;
        public static void IncreasePoints(int value)
        {
            _points += value;
        }

        public static void UpdateControlsTutorial(bool value)
        {
            GameData.isControlsTutorialCompleted = value;
            Save();
        }
        public static void UpdateCombatTutorial(bool value)
        {
            GameData.isCombatTutorialCompleted = value;
            Save();
        }
        public static void UpdateGameData(int stageIndex, int score)
        {
            GameData.stagesData[stageIndex].completed = true;
            GameData.stagesData[stageIndex].score = score;
            Save();
        }
        public static int GetStageScore(string stageID)
        {
            return GameData.stagesData.Find(x=>x.stageID==stageID).score;
        }
        public static int GetStageScore(int stageIndex)
        {
            return GameData.stagesData[stageIndex].score;
        }
        public static void UpdateCustomizationData(string itemId,bool isUnlocked)
        {
            UnlockableItemData itemData = _gameData.customizationData.unlockableItemsData.Find(x => x.itemId == itemId);
            if (itemData == null)
            {
                Debug.LogError($"No item with id{itemId}");
                return;
            }

            itemData.isUnlocked = isUnlocked;
            Save();
        }
        public static bool IsItemUnlocked(string itemId)
        {
            return _gameData.customizationData.unlockableItemsData.Find((x) => x.itemId == itemId).isUnlocked;
        }
        public static void Save()
        {
            string json = JsonUtility.ToJson(_gameData);

            if (!Directory.Exists(gameSaveFolderPath))
            {
                Directory.CreateDirectory(gameSaveFolderPath);
            }
            File.WriteAllText(gameSaveFilePath, json);
        }
        public static void CreateGameData(List<Stage> stages,List<UnlockableItem> unlockableColors, List<CosmeticSO> cosmeticSOs)
        {
            List<StageData> stagesData = new List<StageData>();
            foreach(Stage stage in stages)
            {
                stagesData.Add(new StageData(stage.Id));
            }
            GameData saveData = new GameData(stagesData, unlockableColors, cosmeticSOs);
            string json = JsonUtility.ToJson(saveData);

            if (!Directory.Exists(gameSaveFolderPath))
            {
                Directory.CreateDirectory(gameSaveFolderPath);
            }
            File.WriteAllText(gameSaveFilePath, json);
            _gameData = saveData;
        }
        public static bool LoadGameData()
        {
            string json;
            Debug.Log("get save from: " + gameSaveFilePath);
            if (File.Exists(gameSaveFilePath))
            {
                json = File.ReadAllText(gameSaveFilePath);
                _gameData = JsonUtility.FromJson<GameData>(json);
                return true;
            }

            return false;
        }
        public static void VerifyGameData(List<Stage> stageList, List<UnlockableItem> unlockables, List<CosmeticSO> cosmeticSOs, CustomizationData customizationData)
        {
            if (customizationData.topCosmeticId == null) customizationData.topCosmeticId = "e9076c53d7a4a9d489e3b10363614ddb";
            if (customizationData.midddleCosmeticId == null) customizationData.midddleCosmeticId = "f194953c1695ede4480f4f7ce0c8b0f9";
            if (customizationData.bottomCosmeticId == null) customizationData.bottomCosmeticId = "ece10e96e1dc5bb4ba8f17b3216725ee";
            if (customizationData.cosmeticsData == null) customizationData.cosmeticsData = new List<CosmeticData>();
            if (customizationData.cosmeticsData.Count == 0)
            {
                foreach (CosmeticSO cosmetic in cosmeticSOs)
                {
                    _gameData.customizationData.cosmeticsData.Add(new CosmeticData(cosmetic.Id, cosmetic.Colors));
                }
            }
            foreach (Stage stage in stageList)
            {
                if (_gameData.stagesData.Exists((x) => x.stageID == stage.Id)) continue;
                _gameData.stagesData.Add(new StageData(stage.Id));
            }
            foreach (UnlockableItem unlockableItem in unlockables)
            {
                if (_gameData.customizationData.unlockableItemsData.Exists((x) => x.itemId == unlockableItem.Id)) continue;
                _gameData.customizationData.unlockableItemsData.Add(new UnlockableItemData(unlockableItem.Id, false));
            }
            foreach (CosmeticData cosmeticData in customizationData.cosmeticsData)
            {
                if (_gameData.customizationData.cosmeticsData.Exists((x) => x.cosmeticId == cosmeticData.cosmeticId)) continue;
                _gameData.customizationData.cosmeticsData.Add(new CosmeticData(cosmeticData.cosmeticId, cosmeticData.colors));
            }
        }
    }
}
