using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class CursorLockControl : MonoBehaviour
{
    [SerializeField] bool _setCursorStateOnAwake;
    [SerializeField] bool _shouldLockToGameWindowOnAwake;
    static bool _isVisibilitForced=false;
    private void Awake()
    {
        if(_setCursorStateOnAwake) SetLockToGameWindow(_shouldLockToGameWindowOnAwake);
    }
    public void SetLockToGameWindow(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SetCursorVisibilty(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            SetCursorVisibilty(true);
        }
    }
    public void SetForcedCursorVisibilty(bool value)
    {
        Cursor.visible = value;
        _isVisibilitForced = value;
    }
    public void SetCursorVisibilty(bool value)
    {
        if (_isVisibilitForced) return;
        Cursor.visible=value;
    }
}
