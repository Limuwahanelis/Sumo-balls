using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsSaver : MonoBehaviour
{
    [SerializeField] GlobalAudioManager _audioManager;
    [SerializeField] IntReference _masterVolume;
    [SerializeField] IntReference _sfxVolume;
    public static readonly string audioSettingsFileName = "audioConfigs";

    public void SaveAudioSettings()
    {
        AudioSettingsData data = new AudioSettingsData(_masterVolume.value, _sfxVolume.value);
        JsonSave.SaveToFile(data, audioSettingsFileName);
    }
    public static AudioSettingsData LoadAudioSettings()
    {
        return JsonSave.GetDataFromJson<AudioSettingsData>(audioSettingsFileName);
    }
    public static void SaveAudioSettings(AudioSettingsData audioData)
    {
        JsonSave.SaveToFile(audioData, audioSettingsFileName);
    }
}
