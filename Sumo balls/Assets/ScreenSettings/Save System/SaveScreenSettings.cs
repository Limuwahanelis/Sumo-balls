using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
namespace SaveSystem
{
    public static class SaveScreenSettings
    {
        public static string screenConfigsFolderPath = SaveGameSettings.gameConfigsFolderPath;
        public static string screenConfigsFilePath = screenConfigsFolderPath + @"\screenConfigs.json";
        public static void SaveScreenConfigs(ScreenSettings.MyResolution resolution, bool fullScreen)
        {
            Debug.Log($"Save configs full: {fullScreen}");
            ScreenSettingsData configs = new ScreenSettingsData(resolution, fullScreen);
            string json = JsonUtility.ToJson(configs);

            if (!Directory.Exists(screenConfigsFolderPath))
            {
                Directory.CreateDirectory(screenConfigsFolderPath);
            }
            File.WriteAllText(screenConfigsFilePath, json);
        }

        public static ScreenSettingsData GetScreenSettings()
        {
            ScreenSettingsData configs = null;
            string json;
            Debug.Log("get configs from: " + screenConfigsFilePath);
            if (File.Exists(screenConfigsFilePath))
            {
                json = File.ReadAllText(screenConfigsFilePath);
                configs = JsonUtility.FromJson<ScreenSettingsData>(json);
            }

            return configs;
        }


    }
}
