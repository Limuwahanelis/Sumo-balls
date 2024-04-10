using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabToggleUI : Toggle
{
    [SerializeField]private Graphic _startingtargetGraphic;
    protected override void Awake()
    {
        base.Awake();
        _startingtargetGraphic = targetGraphic;
        if (isOn) SwapTargetGraphic(isOn);
    }
    protected override void Start()
    {
        base.Start();
        if (group != null)
        {
            foreach (Toggle tog in group.ActiveToggles())
            {
                if (tog == this) continue;
                tog.onValueChanged.AddListener(ReturnTargetGraphic);
            }
        }
    }
    public void ReturnTargetGraphic(bool value)
    {
        if(value) targetGraphic = _startingtargetGraphic;
    }
    public void SwapTargetGraphic(bool isOn)
    {
        if (isOn) targetGraphic = graphic;
        else targetGraphic = _startingtargetGraphic;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (isOn) return;
        else base.OnPointerDown(eventData);
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        if (isOn) return;
        base.OnSubmit(eventData);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (isOn) return;
        base.OnPointerClick(eventData);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (group != null)
        {
            foreach (Toggle tog in group.ActiveToggles())
            {
                if (tog == this) continue;
                tog.onValueChanged.RemoveListener(ReturnTargetGraphic);
            }
        }
    }
}
