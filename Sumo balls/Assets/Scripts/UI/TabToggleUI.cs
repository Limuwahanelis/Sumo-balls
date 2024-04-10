using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabToggleUI : Toggle
{
    private Graphic _startingtargetGraphic;
    protected override void Awake()
    {
        base.Awake();
        _startingtargetGraphic = targetGraphic;
        if (isOn) SwapTargetGraphic(isOn);
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
}
