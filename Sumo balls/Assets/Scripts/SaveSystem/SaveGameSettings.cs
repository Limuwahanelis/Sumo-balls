using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveGameSettings
    {
        public static string gameConfigsFolderPath = Application.dataPath + @"\configs";
        public static string gameConfigsFilePath = gameConfigsFolderPath + @"\configs.json";
        public static void SaveSettings(bool fastLoad)
        {
            GameSettingsData configs = new GameSettingsData(fastLoad);
            string json = JsonUtility.ToJson(configs);

            if (!Directory.Exists(gameConfigsFolderPath))
            {
                Directory.CreateDirectory(gameConfigsFolderPath);
            }
            File.WriteAllText(gameConfigsFilePath, json);
        }
        public static void SaveSettings(GameSettingsData configs)
        {
            string json = JsonUtility.ToJson(configs);

            if (!Directory.Exists(gameConfigsFolderPath))
            {
                Directory.CreateDirectory(gameConfigsFolderPath);
            }
            File.WriteAllText(gameConfigsFilePath, json);
        }

        public static GameSettingsData GetGameSettings()
        {
            GameSettingsData configs = null;
            string json;
            Debug.Log("get configs from: " + gameConfigsFilePath);
            if (File.Exists(gameConfigsFilePath))
            {
                json = File.ReadAllText(gameConfigsFilePath);
                configs = JsonUtility.FromJson<GameSettingsData>(json);
            }

            return configs;
        }


    }
}
