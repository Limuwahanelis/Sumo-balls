using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class OverrideActionScaleVector2Processor : MonoBehaviour
{
    [SerializeField] InputActionReference _actionReference;
    [SerializeField] InputBindingReference _bindingReference;
    private InputBinding _bindingMask;
    string xPart;
    string yPart;
    // Start is called before the first frame update
    void Start()
    {
         xPart = _bindingReference.saveName + "X";
         yPart = _bindingReference.saveName + "Y";
        _bindingMask = InputBindingLib.NullifyBindingFields(in _bindingReference.binding);
    }

    public void ApplyOverrideX(float newValue)
    {
        _actionReference.action.ApplyParameterOverride((ScaleVector2Processor v) => v.x, newValue, _bindingMask);
    }
    public void ApplyOverrideY(float newValue)
    {
        _actionReference.action.ApplyParameterOverride((ScaleVector2Processor v) => v.y, newValue, _bindingMask);
    }
    public void Save()
    {
        PlayerPrefs.SetFloat(xPart, _actionReference.action.GetParameterValue((ScaleVector2Processor v) => v.x, _bindingMask).Value);
        PlayerPrefs.SetFloat(yPart, _actionReference.action.GetParameterValue((ScaleVector2Processor v) => v.y, _bindingMask).Value);
    }

    public void Load()
    {
        float x = PlayerPrefs.GetFloat(xPart);
        float y = PlayerPrefs.GetFloat(yPart);
        if (x == 0) x = _actionReference.action.GetParameterValue((ScaleVector2Processor v) => v.x, _bindingMask).Value;
        if (y == 0) y = _actionReference.action.GetParameterValue((ScaleVector2Processor v) => v.y, _bindingMask).Value;
        _actionReference.action.ApplyParameterOverride((ScaleVector2Processor v) => v.x, x, _bindingMask);
        _actionReference.action.ApplyParameterOverride((ScaleVector2Processor v) => v.y, y, _bindingMask);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        Save();
    }
}
