using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor.UI;
using UnityEditor;
#endif
public class SliderActionModifyEvent : Slider
{
    [SerializeField] InputActionReference _modifySliderValueAction;
    [SerializeField] InputActionReference _modifySliderValueOnceAction;
    [SerializeField] float _speed;
    [SerializeField] float _singleValueUpdate;
    float _holdTime;
    float _holdTreshold=0.5f;
    float _selTimer;
    bool _canSelect;
    private bool _isModifyingValue = false;
    float _direction;
    bool _isSelected = false;
    enum Axis
    {
        Horizontal = 0,
        Vertical = 1
    }
    bool reverseValue { get { return direction == Direction.RightToLeft || direction == Direction.TopToBottom; } }
    Axis axis { get { return (direction == Direction.LeftToRight || direction == Direction.RightToLeft) ? Axis.Horizontal : Axis.Vertical; } }

    protected override void Start()
    {
        base.Start();

    }
    protected override void Update()
    {
        base.Update();
        if (_isModifyingValue || _holdTime>= _holdTreshold)
        {
            value += _direction * Time.deltaTime * _speed;
        }
        if(_selTimer<0.2f)
        {
            _selTimer += Time.deltaTime;
            if(_selTimer>0.2f)
            {
                _canSelect = true;
            }
        }
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        _modifySliderValueAction.action.performed += ModifyValue;
        _modifySliderValueAction.action.canceled += EndModifyValue;
        _modifySliderValueOnceAction.action.performed += ModifySingle;
        _modifySliderValueAction.action.Enable();
        _modifySliderValueOnceAction.action.Enable();
        _isSelected = true;
        _canSelect = false;
        _selTimer = 0;
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        DisableActions();
    }
    private void ModifyValue(InputAction.CallbackContext callback)
    {
        if (callback.ReadValue<Vector2>().magnitude > 0)
        {
            switch (GetUpdateDirection(callback.ReadValue<Vector2>()))
            {
                case MoveDirection.Left:
                    _direction = callback.ReadValue<Vector2>().x;
                    if (axis == Axis.Horizontal) { if (reverseValue) _direction = -_direction; }
                    if (((int)axis) * _direction != 0) _direction = 0;
                    break;
                case MoveDirection.Right:
                    _direction = callback.ReadValue<Vector2>().x;
                    if (axis == Axis.Horizontal) {if (reverseValue) _direction = -_direction; }
                    if (((int)axis) * _direction != 0) _direction = 0;
                    break;
                case MoveDirection.Up:
                    _direction = callback.ReadValue<Vector2>().y;
                    if (axis == Axis.Vertical && FindSelectableOnUp() == null) { if (reverseValue) _direction = -_direction; }
                    if (((int)axis) * _direction == 0) _direction = 0;
                    break;
                case MoveDirection.Down:
                    _direction = callback.ReadValue<Vector2>().y;
                    if (axis == Axis.Vertical) { if (reverseValue) _direction = -_direction; }
                    if (((int)axis) * _direction == 0) _direction = 0;
                    break;
            }
            _isModifyingValue = true;
        }
        else _direction = 0;
    }
    private void ModifySingle(InputAction.CallbackContext callback)
    {
        if(_isModifyingValue) return;
        switch (GetUpdateDirection(callback.ReadValue<Vector2>()))
        {
            case MoveDirection.Left:
                _direction = -1;
                if (axis == Axis.Horizontal )
                    Set(reverseValue ? math.round( value + _singleValueUpdate) :math.round( value - _singleValueUpdate));
                break;
            case MoveDirection.Right:
                _direction = 1;
                if (axis == Axis.Horizontal )
                    Set(reverseValue ? math.round( value - _singleValueUpdate) : math.round( value + _singleValueUpdate));
                break;
            case MoveDirection.Up:
                _direction = 1;
                if (axis == Axis.Vertical )
                    Set(reverseValue ? math.round( value - _singleValueUpdate) : math.round( value + _singleValueUpdate));
                break;
            case MoveDirection.Down:
                _direction = -1;
                if (axis == Axis.Vertical )
                    Set(reverseValue ? math.round( value + _singleValueUpdate) : math.round( value - _singleValueUpdate));
                break;
        }
    }
    private void EndModifyValue(InputAction.CallbackContext callback)
    {
        _direction = 0;
        _isModifyingValue = false;
    }
    private MoveDirection GetUpdateDirection(Vector2 vector)
    {
        if (vector == Vector2.zero) return MoveDirection.None;
        if(math.abs(vector.x)>=math.abs(vector.y))
        {
            if (vector.x >= 0) return MoveDirection.Right;
            else return MoveDirection.Left;
        }
        else
        {
            if (vector.y >= 0) return MoveDirection.Up;
            else return MoveDirection.Down;
        }
    }
    public override void OnMove(AxisEventData eventData)
    {
        if(!_canSelect) return;
        switch (eventData.moveDir)
        {
            case MoveDirection.Right:
                if(axis==Axis.Vertical) Navigate(eventData, FindSelectableOnRight());
                break;

            case MoveDirection.Up:
                if (axis == Axis.Horizontal) Navigate(eventData, FindSelectableOnUp());
                break;

            case MoveDirection.Left:
                if (axis == Axis.Vertical) Navigate(eventData, FindSelectableOnLeft());
                break;

            case MoveDirection.Down:
                if (axis == Axis.Horizontal) Navigate(eventData, FindSelectableOnDown());
                break;
        }
    }
    private void Navigate(AxisEventData eventData, Selectable sel)
    {
        if (sel == null) return;
        if (sel.IsActive()) eventData.selectedObject = sel.gameObject;
    }
    private void DisableActions()
    {
        _modifySliderValueAction.action.Disable();
        _modifySliderValueOnceAction.action.Disable();
        _modifySliderValueAction.action.performed -= ModifyValue;
        _modifySliderValueAction.action.canceled -= EndModifyValue;
        _modifySliderValueOnceAction.action.performed -= ModifySingle;
        _isSelected = false;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (_isSelected) DisableActions();
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(SliderActionModifyEvent))]
    public class SliderTestEditr : SliderEditor
    {
        SerializedProperty _speed;
        SerializedProperty _ModifyValueOnceAction;
        SerializedProperty _ModifyValueAction;
        protected override void OnEnable()
        {
            base.OnEnable();
            _speed = serializedObject.FindProperty("_speed");
            _ModifyValueOnceAction = serializedObject.FindProperty("_modifySliderValueOnceAction");
            _ModifyValueAction = serializedObject.FindProperty("_modifySliderValueAction");

        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_ModifyValueAction);
            EditorGUILayout.PropertyField(_ModifyValueOnceAction);
            EditorGUILayout.PropertyField(_speed);
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
#endif
}
