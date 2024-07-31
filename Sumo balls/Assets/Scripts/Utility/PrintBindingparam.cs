using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UIElements;

public class PrintBindingparam : MonoBehaviour
{
    [SerializeField] InputActionReference _actionReference;
    [SerializeField] InputBindingReference _bindingReference;
    [SerializeField] bool _isVector;
    private InputBinding _bindingMask;
    private void OnEnable()
    {
        if (_isVector)
        {
            string xPart = _bindingReference.saveName + "X";
            string yPart = _bindingReference.saveName + "Y";
            _bindingMask = InputBindingLib.NullifyBindingFields(in _bindingReference.binding);
            float x = PlayerPrefs.GetFloat(xPart);
            float y = PlayerPrefs.GetFloat(yPart);
            Logger.Log("X: " + x + "Y: " + y);
            Logger.Log($"x factor: {_actionReference.action.GetParameterValue((ScaleVector2Processor v) => v.x, _bindingMask)}, y factor: {_actionReference.action.GetParameterValue((ScaleVector2Processor v) => v.y, _bindingMask).Value} ");
        }
        else
        {
            Logger.Log(PlayerPrefs.GetFloat(_bindingReference.saveName));

            _bindingMask = InputBindingLib.NullifyBindingFields(in _bindingReference.binding);
            Logger.Log(_actionReference.action.GetParameterValue((ScaleProcessor s) => s.factor, _bindingMask));
        }
    }
}
