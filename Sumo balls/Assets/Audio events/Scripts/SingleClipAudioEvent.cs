using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio Event/SingleClipEvent")]
public class SingleClipAudioEvent : AudioEvent
{
    public AudioClip audioClip;
    [SerializeField] StringReference _musicMasterChannel;
    [SerializeField] StringReference _musicChannel;
    [SerializeField] AudioMixer _audioMixer;
    public override void Play(AudioSource audioSource)
    {
        float mixerValue;
        _audioMixer.GetFloat(_musicMasterChannel.value, out mixerValue);
        if (mixerValue <= -80) audioSource.mute = true; // mute of master is 0
        else
        {
            _audioMixer.GetFloat(_musicChannel.value, out mixerValue);
            if (mixerValue <= -80) audioSource.mute = true; // mute if selected music channel is set to 0
            audioSource.mute = false;
        }
        
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }
    public override void Play(AudioSource audioSource, bool overPlay = false)
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        if (!overPlay) return;
        audioSource.Play();
    }

}
