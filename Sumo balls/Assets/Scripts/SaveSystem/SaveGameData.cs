using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
namespace SaveSystem
{
    public static class SaveGameData 
    {
        public static string gameSaveFolderPath = Application.dataPath + @"\save";
        public static string gameSaveFilePath = gameSaveFolderPath + @"\data.json";
        public static GameData GameData=>_gameData;
        private static GameData _gameData;
        public static void UpdateTutorial(bool value)
        {
            GameData.isTutorialCompleted = value;
            Save();
        }
        public static void UpdateGameData(int stageIndex, Stage stage)
        {
            GameData.stagesData[stageIndex].completed = stage.IsCompleted;
            GameData.stagesData[stageIndex].score = stage.Score;
            Save();
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
        public static void CreateGameData(List<Stage> stages)
        {
            List<StageData> stagesData = new List<StageData>();
            foreach(Stage stage in stages)
            {
                stagesData.Add(new StageData(stage.IsCompleted, stage.Score));
            }
            GameData saveData = new GameData(stagesData);
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
