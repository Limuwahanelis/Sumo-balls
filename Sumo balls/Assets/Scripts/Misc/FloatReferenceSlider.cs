using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatReferenceSlider : MonoBehaviour
{
    [SerializeField] FloatReference _floatReference;
    [SerializeField] Slider _slider;

    private void Reset()
    {
        if(_slider ==null) _slider = GetComponent<Slider>();
    }

    public void SetFloatReferenceValue(float value)
    {
        _floatReference.value = value;
    }
}
