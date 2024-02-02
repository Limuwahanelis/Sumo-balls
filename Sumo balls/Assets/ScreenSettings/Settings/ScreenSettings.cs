using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System;
using System.Linq;
using SaveSystem;
public class ScreenSettings : MonoBehaviour
{
    [Serializable]
    // normal Resolution struct is non serializable, so i made my own
    public class MyResolution
    {
        
        public MyResolution(Resolution res)
        {
            width = res.width;
            height = res.height;
        }
        public int width;
        public int height;
        public static bool operator !=(MyResolution res1, MyResolution res2)
        {
            if(res1==res2) return true;
            return false;
        }
        public static bool operator== (MyResolution res1, MyResolution res2)
        {
            if (res1.width != res2.width) return false;
            if(res1.height != res2.height) return false;
            return true;
        }
        public static Resolution GetNormalResolution(MyResolution res)
        {
            Resolution resolution = new Resolution()
            { height = res.height, width = res.width};
            return resolution;
        }

        public override bool Equals(object obj)
        {
            return obj is Resolution resolution &&
                   width == resolution.width &&
                   height == resolution.height;
        }


    }
    int _currentResIndex;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public MyResolution selectedResolution;
    public bool fullScreen;
    Resolution[] allResolutions;
    List<Resolution> availableResolutions = new List<Resolution>();

    // Start is called before the first frame update

    private void Awake()
    {
    }
    void OnEnable()
    {
        ScreenSettingsData configs = ScreenSettingsSaver.LoadScreenSettings();
        _currentResIndex = 0;
        allResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        availableResolutions = allResolutions.ToList().FindAll(x => x.refreshRate == Screen.currentResolution.refreshRate);
        for (int i = 0; i < availableResolutions.Count; i++)
        {
            availableResolutions.Sort((r1, r2) => r1.height.CompareTo(r2.height));
            availableResolutions.Sort((r1, r2) => r1.width.CompareTo(r2.width));
        }
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < availableResolutions.Count; i++)
        {
            resolutionOptions.Add(availableResolutions[i].width + " x " + availableResolutions[i].height);
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        fullScreenToggle.isOn = configs.fullScreen;
        _currentResIndex = availableResolutions.FindIndex(x => x.width == configs.resolution.width && x.height == configs.resolution.height);
        foreach (Resolution resolution in availableResolutions)
        {
            Debug.Log(resolution.ToString());
        }
        fullScreen = configs.fullScreen;
        Debug.Log(_currentResIndex);
        if (_currentResIndex != -1)
        {
            
            SetResolution(_currentResIndex);
            resolutionDropdown.value = _currentResIndex;
        }
        else
        {
            Screen.SetResolution(availableResolutions[availableResolutions.Count - 1].width, availableResolutions[availableResolutions.Count - 1].height, Screen.fullScreen);
            selectedResolution = new MyResolution(availableResolutions[availableResolutions.Count - 1]);
            resolutionDropdown.value = _currentResIndex;
        }
        SetFullScreen(fullScreen);

    }
    public void SetResolution(int resolutionIndex)
    {
        _currentResIndex = resolutionIndex;
        selectedResolution =new MyResolution(availableResolutions[_currentResIndex]);
        Screen.SetResolution(availableResolutions[_currentResIndex].width, availableResolutions[_currentResIndex].height,fullScreen);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        fullScreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
        fullScreenToggle.isOn = isFullscreen;
    }

    public bool GetFullScreen()
    {
        return fullScreen;
    }
}
