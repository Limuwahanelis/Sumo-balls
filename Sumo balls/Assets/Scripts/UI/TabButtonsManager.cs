using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TabButtonsManager : MonoBehaviour
{
    public TabButtonUI CurrentTab => _currentlySelectedButton;
    [SerializeField] GameObject _panelWithButtons;
    [SerializeField] InputActionReference _changeTabsAction;
    private TabButtonUI _currentlySelectedButton;
    private List<TabButtonUI> _buttons;
    private int _tabIndex = 0;
    private void Awake()
    {
        _buttons = _panelWithButtons.GetComponentsInChildren<TabButtonUI>(true).ToList();
        foreach (TabButtonUI button in _buttons)
        {
            button.OnButtonClicked.AddListener(OnButtonPressed);
        }
        _currentlySelectedButton = _buttons[0];
        _currentlySelectedButton.Select();

    }
    private void OnEnable()
    {
       SetActive(true);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetActive( bool value)
    {
        if(value)
        {
            _changeTabsAction.action.performed += ChangeTab;
            _changeTabsAction.action.Enable();
            _currentlySelectedButton.Deselect();
            _currentlySelectedButton = _buttons[0];
            _currentlySelectedButton.Select();
        }
        else
        {
            _changeTabsAction.action.performed -= ChangeTab;
            _changeTabsAction.action.Disable();
        }
    }
    private void ChangeTab(InputAction.CallbackContext context)
    {
        _tabIndex += ((int)context.ReadValue<float>());
        _currentlySelectedButton.Deselect();
        if (_tabIndex >= _buttons.Count) _tabIndex = 0;
        else if (_tabIndex < 0) _tabIndex = _buttons.Count - 1;
        _currentlySelectedButton = _buttons[_tabIndex];
        _currentlySelectedButton.Select();
    }
    private void OnButtonPressed(TabButtonUI button)
    {
        if (button == _currentlySelectedButton) return;
        _currentlySelectedButton.Deselect();
        _currentlySelectedButton = button;
        _tabIndex = _buttons.FindIndex(x => x == _currentlySelectedButton);
    }

    private void OnDisable()
    {
        SetActive(false);
    }
    private void OnDestroy()
    {
        foreach (TabButtonUI button in _buttons)
        {
            button.OnButtonClicked.RemoveListener(OnButtonPressed);
        }
    }
}
