using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSetup : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;
    [SerializeField] IntReference _masterVolume;
    [SerializeField] IntReference _sfxVolume;
    [SerializeField] IntReference _musicVolume;
    public void LoadAudioSettings()
    {
        AudioSettingsData configs;
        if (AudioSettingsSaver.LoadAudioSettings() == null)
        {
            Debug.Log("audio configs not found");
            configs = new AudioSettingsData();
            AudioSettingsSaver.SaveAudioSettings(configs);
        }
        else
        {
            Debug.Log("got confs");
            configs = AudioSettingsSaver.LoadAudioSettings();

        }
        _masterVolume.value = configs.masterVolume;
        _sfxVolume.value = configs.sfxVolume;
        _musicVolume.value = configs.musicVolume;
    }
}
