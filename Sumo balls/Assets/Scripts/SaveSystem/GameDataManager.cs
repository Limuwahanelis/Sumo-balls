using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
namespace SaveSystem
{
    public static class GameDataManager 
    {
        public static string gameSaveFolderPath = Application.dataPath + @"\save";
        public static string gameSaveFilePath = gameSaveFolderPath + @"\data.json";


        public static GameData GameData =>_gameData;
        private static GameData _gameData;
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
        public static void UpdateGameData(int stageIndex, Stage stage)
        {
            GameData.stagesData[stageIndex].completed = stage.IsCompleted;
            GameData.stagesData[stageIndex].score = stage.Score;
            Save();
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
        public static void CreateGameData(List<Stage> stages,List<UnlockableItem> unlockableColors)
        {
            List<StageData> stagesData = new List<StageData>();
            foreach(Stage stage in stages)
            {
                stagesData.Add(new StageData(stage.IsCompleted, stage.Score));
            }
            GameData saveData = new GameData(stagesData, unlockableColors);
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
    }
}
