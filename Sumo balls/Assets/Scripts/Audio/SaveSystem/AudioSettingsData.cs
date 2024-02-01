using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsData 
{
    public float masterVolume;
    public float sfxVolume;

    public AudioSettingsData(float masterVolume, float sfxVolume)
    {
        this.masterVolume = masterVolume;
        this.sfxVolume = sfxVolume;
    }
}
