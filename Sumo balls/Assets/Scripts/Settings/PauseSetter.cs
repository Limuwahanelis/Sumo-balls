using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseSetter : MonoBehaviour
{
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public void SetPause(bool value)
    {
        Debug.Log($"Pause: {value}");
        GlobalSettings.SetPause(value);
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
    }
}
