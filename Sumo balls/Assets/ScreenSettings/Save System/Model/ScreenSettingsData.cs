using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SaveSystem
{
    [System.Serializable]
    public class ScreenSettingsData
    {
        public ScreenSettings.MyResolution resolution;
        public bool fullScreen;
        public ScreenSettingsData(ScreenSettings.MyResolution resolution, bool fullScreen)
        {
            this.resolution = resolution;
            this.fullScreen = fullScreen;
        }
    }
}
