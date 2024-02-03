using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GlobalAudioManager : MonoBehaviour
{
    public enum MusicChannel
    {
        SFX
    }

    [SerializeField] AudioMixer _mixer;
    [SerializeField] IntReference _masterMusicVolume;
    [SerializeField] IntReference _sfxMusicVolume;
    [SerializeField] Slider _masterVolumeSlider;
    [SerializeField] Slider _sfxVolumeSlider;

    private void Awake()
    {
        _masterVolumeSlider.value = _masterMusicVolume.value;

        _sfxVolumeSlider.value = _sfxMusicVolume.value;
    }
}

