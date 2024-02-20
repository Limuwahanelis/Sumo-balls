using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseSetter : MonoBehaviour
{
    [SerializeField] InputActionReference _playerPause;
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    private static bool _isForcedPause = false;
    public void SetPause(bool value)
    {
        if (_isForcedPause) return;
        GlobalSettings.SetPause(value);
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
    }

    public void SetForcedPause(bool value)
    {
        if (value) _playerPause.action.Disable();
        else _playerPause.action.Enable();
        _isForcedPause = value;
        GlobalSettings.SetPause(value);
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
    }
}
