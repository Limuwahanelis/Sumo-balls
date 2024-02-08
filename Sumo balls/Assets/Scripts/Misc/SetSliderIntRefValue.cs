using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderIntRefValue : MonoBehaviour
{
    [SerializeField] IntReference _value;
    [SerializeField] Slider _slider;

    private void OnEnable()
    {
        _slider.value = _value.value;
    }

    private void OnValidate()
    {
        if(_slider == null) _slider = GetComponent<Slider>();
    }
}
