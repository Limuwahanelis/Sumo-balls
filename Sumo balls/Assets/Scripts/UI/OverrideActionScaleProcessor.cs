using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class OverrideActionScaleProcessor : MonoBehaviour
{
    [SerializeField] InputActionReference _actionReference;
    [SerializeField] InputBindingReference _bindingReference;
    private InputBinding _bindingMask;
    // Start is called before the first frame update
    void Start()
    {
        _bindingMask=InputBindingLib.NullifyBindingFields(in _bindingReference.binding);
        //foreach (InputBinding bind in _actionReference.action.bindings)
        //{
        //    Debug.Log(bind.name);
        //    if (_bindingReference.binding.Matches(bind)) Debug.Log("machss");
        //}
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void ApplyOverride(float newValue)
    {
        _actionReference.action.ApplyParameterOverride((ScaleProcessor s) => s.factor, newValue, _bindingMask);
    }
    public void Save()
    {
        PlayerPrefs.SetFloat(_bindingReference.saveName,_actionReference.action.GetParameterValue((ScaleProcessor s) => s.factor, _bindingMask).Value);
    }
    public void Load()
    {
        float rebind = PlayerPrefs.GetFloat(_bindingReference.saveName);
        if (rebind == 0) rebind = _actionReference.action.GetParameterValue((ScaleProcessor s) => s.factor, _bindingMask).Value;
        _actionReference.action.ApplyParameterOverride((ScaleProcessor s) => s.factor, rebind, _bindingMask);
    }

    private void OnDisable()
    {
        Save();
    }
}
