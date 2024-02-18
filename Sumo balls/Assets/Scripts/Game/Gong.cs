using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gong : MonoBehaviour
{
    [SerializeField] SingleClipAudioEvent _audioEvent;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Animator _animator;
    [SerializeField] Countdown _countdown;
    [SerializeField] BoolReference _fastLoad;
    private bool _fired;
    private void Awake()
    {
        if (_fastLoad.value) gameObject.SetActive(false);
        _audioEvent.MusicChannelVolume?.variable.OnValueChanged.AddListener(ChangeVolume);
        _audioEvent.MusicMasterChannelVolume?.variable.OnValueChanged.AddListener(ChangeVolume);
    }
    public void StartGongAnim()
    {
        _animator.SetBool("Play",true);
    }
    public void PlayTheGong()
    {
        _audioEvent.Play(_audioSource,true);
        enabled = false;
    }
    public void ResetGong()
    {
        if (_fastLoad.value) gameObject.SetActive(false);
        else gameObject.SetActive(true);
        enabled = true;
        _fired = false;
        _animator.SetBool("Play",false);
    }
    private void Update()
    {
        if (_fired) return;
        if(_countdown.StartingTime-_countdown.CountTime<=0.45f) // time required for stick to hit the gong
        {
            _fired=true;
            StartGongAnim();
        }
    }
    private void ChangeVolume(int value)
    {
        _audioSource.volume=_audioEvent.volume* (_audioEvent.MusicChannelVolume.value / 100.0f) * (_audioEvent.MusicMasterChannelVolume.value / 100.0f);
    }
    private void OnDestroy()
    {
        _audioEvent.MusicChannelVolume?.variable.OnValueChanged.RemoveListener(ChangeVolume);
        _audioEvent.MusicMasterChannelVolume?.variable.OnValueChanged.RemoveListener(ChangeVolume);
    }
}
