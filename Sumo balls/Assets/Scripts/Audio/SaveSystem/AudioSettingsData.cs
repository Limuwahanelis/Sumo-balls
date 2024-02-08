using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioSettingsData 
{
    public int masterVolume;
    public int sfxVolume;
    public int musicVolume;
    public AudioSettingsData(int masterVolume, int sfxVolume, int musicVolume)
    {
        this.masterVolume = masterVolume;
        this.sfxVolume = sfxVolume;
        this.musicVolume = musicVolume;
    }

    public AudioSettingsData()
    {
        masterVolume = 50;
        sfxVolume = 50;
        musicVolume = 50;
    }
}
