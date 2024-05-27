using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveSystem;

public class ScreenSetUp : MonoBehaviour
{
    private Resolution[] allResolutions;
    private List<Resolution> availableResolutions = new List<Resolution>();
    private void Awake()
    {
        if (ScreenSettingsSaver.LoadScreenSettings()==null)
        {
            GetAllResolutions();
            Debug.Log("org res: " + availableResolutions[availableResolutions.Count - 1]);
            ScreenSettingsData screenData = new ScreenSettingsData(new ScreenSettings.MyResolution(availableResolutions[availableResolutions.Count - 1]), true,1,0,false,-1);
            ScreenSettingsSaver.SaveScreenSettings(screenData);
            Screen.SetResolution(availableResolutions[availableResolutions.Count - 1].width, availableResolutions[availableResolutions.Count - 1].height, true);
        }
        else
        {
            ScreenSettingsData configs = ScreenSettingsSaver.LoadScreenSettings();
            Screen.SetResolution(configs.resolution.width,configs.resolution.height,configs.fullScreen);
            if (configs.VSync) QualitySettings.vSyncCount = configs.VSyncCountIndex;
            else QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = configs.targetFrameRate;
        }
    }
    void GetAllResolutions()
    {
        ScreenSettingsData configs = ScreenSettingsSaver.LoadScreenSettings();
        allResolutions = Screen.resolutions;
        availableResolutions = allResolutions.ToList().FindAll(x => x.refreshRate == Screen.currentResolution.refreshRate);
        for (int i = 0; i < availableResolutions.Count; i++)
        {
            availableResolutions.Sort((r1, r2) => r1.height.CompareTo(r2.height));
            availableResolutions.Sort((r1, r2) => r1.width.CompareTo(r2.width));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
