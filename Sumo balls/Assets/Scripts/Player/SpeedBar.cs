using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{
    [SerializeField] BoolReference _showSpeedBar;
    [SerializeField] Slider _slider;
    [SerializeField] Image _sliderFill;
    [SerializeField] float _maxDisplayedSpeed;
    [SerializeField] Color _safeSpeedColor;
    [SerializeField] Color _mediumSpeedColor;
    [SerializeField] Color _fastSpeedColor;
    [SerializeField] List<float> _speedLimits = new List<float>();
    private void Awake()
    {
        if(_showSpeedBar.value) _slider.GetComponentInParent<Canvas>().gameObject.SetActive(true);
        else _slider.GetComponentInParent<Canvas>().gameObject.SetActive(false);
        _showSpeedBar?.variable.OnValueChanged.AddListener(SetSpeedBar);
        _slider.maxValue = _maxDisplayedSpeed;

    }
    public void SetSpeed(float value)
    {
        value = Mathf.Clamp(value,0,_maxDisplayedSpeed);
        _slider.value = value;
        if (value < _speedLimits[0])
        {
            _sliderFill.color = _safeSpeedColor;
            return;
        }
        else
        {
            if(value > _speedLimits[0] && value < _speedLimits[1])
            {
                _sliderFill.color = _mediumSpeedColor;
                return;
            }
            if(value > _speedLimits[1])
            {
                _sliderFill.color = _fastSpeedColor;
                return;
            }
        }
    }
    private void SetSpeedBar(bool value)
    {
        _slider.GetComponentInParent<Canvas>(true).gameObject.SetActive(value);
    }

    private void OnDestroy()
    {
        _showSpeedBar?.variable.OnValueChanged.RemoveListener(SetSpeedBar);
    }
}
