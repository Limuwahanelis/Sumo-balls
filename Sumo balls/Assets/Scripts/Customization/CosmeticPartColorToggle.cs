using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CosmeticPartColorToggle : MonoBehaviour
{
    public UnityEvent<int> OnPartSelected;
    [SerializeField] TMP_Text _text;
    private int index;
    public void SetUp(int index,string partName)
    {
        this.index = index;
        _text.text = partName;
    }

    public void SelectTrigger(bool value)
    {
        if (value) OnPartSelected?.Invoke(index);
    }
}
