using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBindingLib
{
    public static InputBinding NullifyBindingFields(in InputBinding binding)
    {
        InputBinding inputBinding = new InputBinding(binding.path, binding.action, binding.groups, binding.processors, binding.interactions, binding.name);
        if (string.IsNullOrEmpty(inputBinding.name)) inputBinding.name = null;
        if (string.IsNullOrEmpty(inputBinding.path)) inputBinding.path = null;
        if (string.IsNullOrEmpty(inputBinding.action)) inputBinding.action = null;
        if (string.IsNullOrEmpty(inputBinding.interactions)) inputBinding.interactions = null;
        if (string.IsNullOrEmpty(inputBinding.groups)) inputBinding.groups = null;
        return inputBinding;
    }
}
