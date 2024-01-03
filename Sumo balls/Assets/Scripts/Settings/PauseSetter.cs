using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSetter : MonoBehaviour
{
    public void SetPause(bool value) => GlobalSettings.SetPause(value);
}
