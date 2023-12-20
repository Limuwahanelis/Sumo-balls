using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetControlSchemeBindings : MonoBehaviour
{
    [SerializeField] InputActionAsset _actionAsset;
    [SerializeField] TabButtonsManager _tabManager;
    [SerializeField] TabButtonUI _keyboardAndMousetab;
    [SerializeField] TabButtonUI _gamepadTab;
    [SerializeField] string _keyboardScheme;
    [SerializeField] string _mouseScheme;
    [SerializeField] string _gamepadScheme;
    private List<string> _targetSchemes= new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetSelectedControlSchemeBindings()
    {
        _targetSchemes.Clear();
        if (_tabManager.CurrentTab == _gamepadTab) _targetSchemes.Add(_gamepadScheme);
        else
        {
            _targetSchemes.Add(_keyboardScheme);
            _targetSchemes.Add(_mouseScheme);
        }
        foreach(InputActionMap map in _actionAsset.actionMaps)
        {
            foreach(InputAction action in map.actions)
            {
                action.RemoveBindingOverride(InputBinding.MaskByGroups(_targetSchemes.ToArray()));
            }
        }
    }
}
