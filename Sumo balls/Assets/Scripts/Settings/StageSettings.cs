using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSettings : MonoBehaviour
{
    [SerializeField] BoolValue _stageFastLoad;
    public void SetStageFastLoad(bool value)
    {
        _stageFastLoad.value = value;
    }
}
