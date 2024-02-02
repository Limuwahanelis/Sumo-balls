using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSetup : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;

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
        float remappedValueMaster = math.remap(0,100,0.01f,100, configs.masterVolume);
        float remappedValueSfx = math.remap(0, 100, 0.01f, 100, configs.sfxVolume);
        _mixer.SetFloat(GlobalAudioManager.MIXER_MASTER, Mathf.Log10(remappedValueMaster / 100) * 20f);
        _mixer.SetFloat(GlobalAudioManager.MIXER_SFX, Mathf.Log10(remappedValueSfx / 100) * 20f);
    }
}
