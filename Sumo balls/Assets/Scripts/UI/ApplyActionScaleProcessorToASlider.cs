using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class ApplyActionScaleProcessorToASlider : MonoBehaviour
{
    [SerializeField] InputBindingReference _inputBindingReference;
    [SerializeField] InputActionReference _actionToreadProcessorFrom;
    [SerializeField] SliderWithTextInput _slider;
    private InputBinding _bindingMask;
    float _scaleValue;
    private void Awake()
    {
        _bindingMask =InputBindingLib.NullifyBindingFields(_inputBindingReference.binding);
        _scaleValue = PlayerPrefs.GetFloat(_inputBindingReference.saveName);
        if (_scaleValue == 0) _scaleValue = _actionToreadProcessorFrom.action.GetParameterValue((ScaleProcessor s) => s.factor, _bindingMask).Value;
        _slider.UpdateSlider(_scaleValue.ToString());
        _slider.UpdateText(_scaleValue);
        Debug.Log(_actionToreadProcessorFrom.action.GetParameterValue((ScaleProcessor s) => s.factor, _bindingMask));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
