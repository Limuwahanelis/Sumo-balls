using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Event")]
public class AudioEvent : ScriptableObject
{
    [Range(0,1)]
    public float volume;
    [Range(0, 2)]
    public float pitch;

    public virtual void Play(AudioSource audioSource) { }
    /// <summary>
    /// Plays sounds but allows for restarting it.
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="overPlay"></param>
    public virtual void Play(AudioSource audioSource, bool overPlay = false) { }

}
