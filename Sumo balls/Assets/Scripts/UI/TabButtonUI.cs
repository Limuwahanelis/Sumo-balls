using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButtonUI : Selectable//,ISubmitHandler
{
    public UnityEvent<TabButtonUI> OnButtonClicked;
    bool _isBeingPressed = false;
    bool _isPointerInside = false;
    bool _isSelected = false;
    SelectionState _stateToBeOnEnable;
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnEnable()
    {
        Debug.Log(base.currentSelectionState);
        DoStateTransition(_stateToBeOnEnable,true);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(_isSelected) return;
        DoStateTransition(SelectionState.Highlighted, false);
        _isPointerInside = true;
    }
   
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (_isSelected) return;
        if (!_isSelected)
        {
            DoStateTransition(SelectionState.Normal, false);
            _isPointerInside = false;
            _isBeingPressed = false;
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (_isSelected) return;
        if (_isBeingPressed)
        {
            DoStateTransition(SelectionState.Selected, false);
            _isSelected = true;
            OnButtonClicked?.Invoke(this);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (_isSelected) return;
        if (_isPointerInside)
        {
            DoStateTransition(SelectionState.Pressed, false);
            _isBeingPressed = true;
        }
    }
    public void OnSubmit(BaseEventData eventData)
    {
        if (_isSelected) return;
        DoStateTransition(SelectionState.Selected, false);
        OnButtonClicked?.Invoke(this);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (_isSelected) return;
        DoStateTransition(SelectionState.Selected, false);
        _isSelected = true;
        //base.OnSelect(eventData);
        _stateToBeOnEnable = SelectionState.Selected;
        OnButtonClicked?.Invoke(this);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        _isSelected = false;
        _stateToBeOnEnable = SelectionState.Normal;
        base.OnDeselect(eventData);
    }
    public override void Select()
    {
        BaseEventData eventData = new BaseEventData(EventSystem.current);
        OnSelect(eventData);
        //base.Select();
    }
    public void Deselect()
    {
        BaseEventData eventData = new BaseEventData(EventSystem.current);
        OnDeselect(eventData);
    }
}
