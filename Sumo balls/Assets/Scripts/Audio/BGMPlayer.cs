using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioEvent _musicToPlay;
    [SerializeField] IntReference _musicChannel;
    [SerializeField] IntReference _masterMusicChannel;
    [SerializeField] BoolReference _continueMusic;
    [SerializeField] bool _playOnStart;
    // Start is called before the first frame update
    void Start()
    {
        if(_audioSource == null) _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _musicChannel.variable?.OnValueChanged.AddListener(ChangeVolume);
        _masterMusicChannel.variable?.OnValueChanged.AddListener(ChangeVolume);
        if(_playOnStart|| _continueMusic.value) Play();
    }
    public void Play()
    {
        _musicToPlay.Play(_audioSource);

    }
    private void ChangeVolume(int value)
    {
        _audioSource.volume = (_masterMusicChannel.value / 100f) * (_musicChannel.value / 100f);
    }
    private void OnValidate()
    {
        if(_audioSource == null) _audioSource = GetComponent<AudioSource>();
    }
    private void OnDestroy()
    {
        _musicChannel.variable?.OnValueChanged.RemoveListener(ChangeVolume);
        _masterMusicChannel.variable?.OnValueChanged.RemoveListener(ChangeVolume);
    }

}
