using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] PauseSetter _pauseSetter;
    [SerializeField] InputActionAsset _inputActionAsset;
    [SerializeField] string _playerActions;
    bool _isPushing = false;
    float _pushdirection;
    // Start is called before the first frame update
    void Start()
    {
        //_inputActionAsset.FindActionMap(_playerActions).Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.IsGamePaused) return;
        if (_isPushing) _player.PushBall(_pushdirection);
    }
    public void ResetActionMap()
    {
        _inputActionAsset.FindActionMap(_playerActions).Enable();
    }
    public void DisableActionMap()
    {
        _inputActionAsset.FindActionMap(_playerActions).Disable();
    }
    void OnPush(InputValue val)
    {
        _pushdirection = val.Get<float>();
        if (math.abs(_pushdirection) >= 1f) _isPushing = true;
        else _isPushing = false;

    }

    void OnPause(InputValue val)
    {
        _pauseSetter.SetPause(!GlobalSettings.IsGamePaused);
    }
    public void DisableAction(InputActionReference action)
    {
        action.action.Disable();
    }
}
