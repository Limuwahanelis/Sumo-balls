using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.HID.HID;
using static UnityEngine.Rendering.DebugUI;

public class SelectPanelToMoveIn : MonoBehaviour
{
    [SerializeField] InputActionReference _changePanelAction;
    [SerializeField] List<GameObject> _panels;
    [SerializeField] List<InputActionReference> _panelNavigationAction;
    [SerializeField] List<InputActionReference> _panelSubmitAction;
    [SerializeField] InputActionReference _defaultNavigationAction;
    private GameObject _currentPanel;
    private List<Selectable> _firstSelectables;
    private int _currentPanelIndex=0;

    private void Awake()
    {
        _changePanelAction.action.performed += ChangePanel;
    }
    // Start is called before the first frame update
    void Start()
    {
        _firstSelectables=new List<Selectable>();
        for (int i=_panels.Count-1; i>=0; i--)
        {
            Selectable tmp = _panels[i].GetComponentInChildren<Selectable>();
            if (tmp == null)
            {
                Debug.LogError($"Game object lack selectable {_panels[i]}");
                _panels.RemoveAt(i);
            }
            else
            {
                tmp.Select();
                _firstSelectables.Add(tmp);
            }
        }
        foreach (InputActionReference actionRef in _panelNavigationAction) actionRef.action.Disable();
        _panelNavigationAction[_currentPanelIndex].action.Enable();
    }
    private void OnEnable()
    {
        foreach (InputActionReference actionRef in _panelNavigationAction) actionRef.action.Disable();
        _panelNavigationAction[_currentPanelIndex].action.Enable();
        _changePanelAction.action.Enable();
    }

    private void ChangePanel(InputAction.CallbackContext context)
    {

        //if(_firstSelectables[_currentPanelIndex] is  TabButtonUI tabButton) tabButton.Deselect();
        foreach(InputActionReference actionRef in _panelNavigationAction) actionRef.action.Disable();
        _currentPanelIndex += (int)context.ReadValue<float>();
        if (_currentPanelIndex >= _firstSelectables.Count) _currentPanelIndex = 0;
        else if (_currentPanelIndex < 0) _currentPanelIndex = _firstSelectables.Count - 1;
        _panelNavigationAction[_currentPanelIndex].action.Enable();
        //_firstSelectables[_currentPanelIndex].Select();

    }
    private void OnDisable()
    {
        _changePanelAction.action.Disable();
        foreach (InputActionReference actionRef in _panelNavigationAction) actionRef.action.Disable();
        _defaultNavigationAction.action.Enable();
    }
    private void OnDestroy()
    {
        _changePanelAction.action.performed -= ChangePanel;
    }
}
