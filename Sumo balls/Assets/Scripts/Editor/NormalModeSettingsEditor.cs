using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NormalModeSettings))]
public class NormalModeSettingsEditor : Editor
{
    string[] exclude;// = new string[] { };
    SerializedProperty _inCage;
    SerializedProperty _timeRequiredForStar;
    SerializedProperty _wallsRequiredForStar;
    private void OnEnable()
    {
        exclude = new string[] { "_timeRequiredForStar", "_wallsRequiredForStar" };
        _inCage = serializedObject.FindProperty("_isInCage");
        _wallsRequiredForStar = serializedObject.FindProperty("_wallsRequiredForStar");
        _timeRequiredForStar = serializedObject.FindProperty("_timeRequiredForStar");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject,exclude);
        if(_inCage.boolValue) EditorGUILayout.PropertyField(_wallsRequiredForStar);
        else EditorGUILayout.PropertyField(_timeRequiredForStar);
        serializedObject.ApplyModifiedProperties();
    }
}
