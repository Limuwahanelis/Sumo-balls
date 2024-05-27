using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(SliderWithMultipleLabels))]
public class SliderWithMultipleLabelsEditor: Editor
{
    SerializedProperty _slider;
    SerializedProperty _labels;
    SerializedProperty _labelPrefab;
    SerializedProperty _labelYOffset;
    float _handleDistance;
    bool _wholeNumbers;
    int _maxValue;
    private void OnEnable()
    {
        _labelPrefab = serializedObject.FindProperty("_labelPrefab");
        _slider = serializedObject.FindProperty("_slider");
        _labels = serializedObject.FindProperty("_labels");
        _labelYOffset = serializedObject.FindProperty("_yLabelOffset");
        _handleDistance = serializedObject.FindProperty("_handleDistance").floatValue;
        _wholeNumbers = (_slider.objectReferenceValue as Slider).wholeNumbers;
        
        GameObject aa;

    }
    public override void OnInspectorGUI()
    {
        _maxValue = (int)(_slider.objectReferenceValue as Slider).maxValue;
        base.OnInspectorGUI();
        serializedObject.Update();
        if(_wholeNumbers)
        {
            if(_labels.arraySize< _maxValue)
            {
                for (int i = _labels.arraySize; i < _maxValue ; i++)
                {
                    _labels.InsertArrayElementAtIndex(i);
                    TMP_Text label = Instantiate(_labelPrefab.objectReferenceValue as GameObject,(_slider.objectReferenceValue as Slider).transform).GetComponent<TMP_Text>();
                    float sliderWidth = (_slider.objectReferenceValue as Slider).GetComponent<RectTransform>().rect.width;
                    label.transform.localPosition = new Vector3(((sliderWidth - _handleDistance * 2) / (_maxValue - 1)) * i - (sliderWidth-_handleDistance*2)/2, _labelYOffset.floatValue, 0);
                    _labels.GetArrayElementAtIndex(i).objectReferenceValue = label;
                    label.text = i.ToString();
                }
            }
            else if (_labels.arraySize>_maxValue)
            {
                for (int i = _labels.arraySize-1; i >= _maxValue; i--)
                {
                     
                    TMP_Text label = _labels.GetArrayElementAtIndex(i).objectReferenceValue as TMP_Text;
                    DestroyImmediate(label.gameObject);
                    _labels.DeleteArrayElementAtIndex(i);
                }
                for(int i=0;i<_maxValue; i++)
                {
                    TMP_Text label = _labels.GetArrayElementAtIndex(i).objectReferenceValue as TMP_Text;
                    float sliderWidth = (_slider.objectReferenceValue as Slider).GetComponent<RectTransform>().rect.width;
                    label.transform.localPosition = new Vector3(((sliderWidth - _handleDistance * 2) / (_maxValue - 1)) * i - (sliderWidth - _handleDistance * 2) / 2, _labelYOffset.floatValue, 0);
                    _labels.GetArrayElementAtIndex(i).objectReferenceValue = label;
                    label.text = i.ToString();
                }
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

}