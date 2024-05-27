using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderWithMultipleLabels : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] GameObject _labelPrefab;
    [SerializeField] float _handleDistance=10f;
    [SerializeField] float _yLabelOffset=18f;
    [SerializeField] List<TMP_Text> _labels=new List<TMP_Text>();
    // slider width, minus x for handle width
    // e.g 160 -10 for left and -10 for right is 140
    // then divide by max value - 1 
    // e.g 140 with max 7, then separation is by 140/(6)=23.(3)
    // 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
