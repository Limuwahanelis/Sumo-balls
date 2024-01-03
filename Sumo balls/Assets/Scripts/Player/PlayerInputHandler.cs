using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] PauseSetter _pauseSetter;
    bool _isPushing = false;
    float _pushdirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.IsGamePaused) return;
        if (_isPushing) _player.PushBall(_pushdirection);
    }

    void OnPush(InputValue val)
    {
        if (GlobalSettings.IsGamePaused) return;
        _pushdirection = val.Get<float>();
        if (math.abs(_pushdirection) >= 1f) _isPushing = true;
        else _isPushing = false;

    }

    void OnPause(InputValue val)
    {
        _pauseSetter.SetPause(!GlobalSettings.IsGamePaused);
    }
}
