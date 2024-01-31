using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowIntReference : MonoBehaviour
{
    [SerializeField] IntReference _reference;
    [SerializeField] TMP_Text _text;

    // Update is called once per frame
    void Update()
    {
        _text.text = _reference.value.ToString();
    }
}
