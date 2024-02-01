using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GlobalAudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer _masterMixer;
    [SerializeField] AudioMixer _sfxMixer;
    [SerializeField] Slider _masterVolumeSlider;
    [SerializeField] Slider _sfxVolumeSlider;

    const string MIXER_MUSIC = "Master volume";
    const string MIXER_SFX = "Sfx volume";
    private void Awake()
    {
        //_masterVolumeSlider.
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetMasterVolume(float value)
    {
        _masterMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }


    public void SetSfxVolume(float value)
    {
        _masterMixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}

