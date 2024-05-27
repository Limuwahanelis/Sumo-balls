using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    private float _time = 0f;
    int _framesCount = 0;
    private void Start()
    {
        //Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 4;
    }
    // Update is called once per frame
    void Update()
    {
        _time += Time.unscaledDeltaTime;
        _framesCount++;
        if( _time >= 1f )
        {
            _text.text = (_framesCount / _time).ToString();
            _time = 0f;
            _framesCount = 0;
        }
       
    }
}
