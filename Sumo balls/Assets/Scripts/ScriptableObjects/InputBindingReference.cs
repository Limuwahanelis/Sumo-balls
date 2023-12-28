using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="Values/Input binding")]
public class InputBindingReference : ScriptableObject
{
    public InputBinding binding;
    public string saveName;
}
