using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class ApplyActionScaleVector2ProcessorToSlider : MonoBehaviour
{
    [SerializeField] InputBindingReference _inputBindingReference;
    [SerializeField] InputActionReference _actionToreadProcessorFrom;
    [SerializeField] SliderWithTextInput _slider;
    [SerializeField] bool _useXValue;
    private InputBinding _bindingMask;
    float _scaleXValue;
    float _scaleYValue;
    // Start is called before the first frame update
    void Start()
    {
        _bindingMask = InputBindingLib.NullifyBindingFields(_inputBindingReference.binding);
        _scaleXValue = PlayerPrefs.GetFloat(_inputBindingReference.saveName+"X");
        _scaleYValue = PlayerPrefs.GetFloat(_inputBindingReference.saveName + "Y");
        if (_scaleXValue == 0) _scaleXValue = _actionToreadProcessorFrom.action.GetParameterValue((ScaleVector2Processor v) => v.x, _bindingMask).Value;
        if (_scaleYValue == 0) _scaleYValue = _actionToreadProcessorFrom.action.GetParameterValue((ScaleVector2Processor v) => v.y, _bindingMask).Value;
        if(_useXValue)
        {
            _slider.UpdateSlider(_scaleXValue.ToString());
            _slider.UpdateText(_scaleXValue);
        }
        else
        {
            _slider.UpdateSlider(_scaleYValue.ToString());
            _slider.UpdateText(_scaleYValue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
