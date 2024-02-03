using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioSettingsData 
{
    public int masterVolume;
    public int sfxVolume;

    public AudioSettingsData(int masterVolume, int sfxVolume)
    {
        this.masterVolume = masterVolume;
        this.sfxVolume = sfxVolume;
    }

    public AudioSettingsData()
    {
        masterVolume = 50;
        sfxVolume = 50;
    }
}
