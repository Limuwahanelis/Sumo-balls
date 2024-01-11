using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InStageDescription : MonoBehaviour
{
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] TMP_Text _valueText;

    public void SetDescription(string text)
    {
        _descriptionText.text = text;
    }
    public void SetValue(string text)
    {
        _valueText.text = text;
    }
}
