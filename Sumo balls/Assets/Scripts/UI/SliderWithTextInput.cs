using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI;

public class SliderWithTextInput : MonoBehaviour
{
    [SerializeField] FloatReference _valueReference;
    [SerializeField] TMP_InputField _inputTextField;
    [SerializeField] Slider _slider;
    [SerializeField] float _minValue;
    [SerializeField] float _maxValue;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText(_slider.value);
    }
    public void UpdateSlider(string text)
    {
        float value;
        if (string.IsNullOrEmpty(text)) value = _minValue;
        else value = float.Parse(text);
        _slider.SetValueWithoutNotify(value);
        _valueReference.value = value;
    }
    public void UpdateText(float value)
    {
        _inputTextField.text = value.ToString("0.00");
    }

    public void CheckText(string text)
    {

        float value;
        string toOutput;
        if (string.IsNullOrEmpty(text)) return;
        else
        {
            if(text==",")
            {
                toOutput = "0,0";
                value = float.Parse(toOutput);
                _inputTextField.text = toOutput;
                _slider.value = value;
                return;
            }
            value = float.Parse(text);
            value = Mathf.Clamp(value, _minValue, _maxValue) * 1.0f;
            if (text[text.Length - 1] == ',') toOutput = text;
            else if(text[0] == ',') toOutput = text;
            else toOutput = value.ToString();

            value = float.Parse(text);
            value = Mathf.Clamp(value, _minValue, _maxValue) * 1.0f;
            _inputTextField.text = toOutput;
            _slider.SetValueWithoutNotify(value);
            _valueReference.value = value;
        }


    }

    private void OnValidate()
    {
        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;
    }
}
