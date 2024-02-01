using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowGameVersion : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    private void Awake()
    {
        _text.text = $"v: {Application.version}";
    }
}
