using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Values/Int")]
public class IntValue : ScriptableObject
{
    public int value;

    public void SetValue(float newValue)
    {
        value = ((int)newValue);
    }
}
