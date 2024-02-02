using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GlobalAudioManager : MonoBehaviour
{
    public static readonly string MIXER_MASTER = "Master volume";
    public static readonly string MIXER_SFX = "Sfx volume";

    public float MasterVolume => _masterVolumeSlider.value;
    public float SfxVolume => _sfxVolumeSlider.value;

    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _masterVolumeSlider;
    [SerializeField] Slider _sfxVolumeSlider;

    private void Awake()
    {
        float value;
        _mixer.GetFloat(MIXER_MASTER, out value);
        float sliderValue = math.pow(10, value / 20) * 100;
        _masterVolumeSlider.value = sliderValue;

        _mixer.GetFloat(MIXER_SFX, out value);
        sliderValue = math.pow(10, value / 20) * 100;
        _sfxVolumeSlider.value = sliderValue;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetMasterVolume(float value)
    {
        float remappedValue= math.remap(0, 100, 0.01f, 100, value);
        _mixer.SetFloat(MIXER_MASTER, Mathf.Log10(remappedValue / 100) * 20);
    }


    public void SetSfxVolume(float value)
    {
        float remappedValue = math.remap(0, 100, 0.01f, 100, value);
        _mixer.SetFloat(MIXER_SFX, Mathf.Log10(remappedValue / 100) * 20);
    }
}

