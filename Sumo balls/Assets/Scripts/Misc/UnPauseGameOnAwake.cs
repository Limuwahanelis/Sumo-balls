using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UnPauseGameOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GlobalSettings.SetPause(false);
    }
}
