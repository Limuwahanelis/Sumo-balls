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
    SerializedProperty _enemiesInStage;
    private void OnEnable()
    {
        exclude = new string[] { "_timeRequiredForStar", "_wallsRequiredForStar", "_enemiesInStage" };
        _inCage = serializedObject.FindProperty("_isInCage");
        _wallsRequiredForStar = serializedObject.FindProperty("_wallsRequiredForStar");
        _timeRequiredForStar = serializedObject.FindProperty("_timeRequiredForStar");
        _enemiesInStage = serializedObject.FindProperty("_enemiesInStage");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject,exclude);
        if(_inCage.boolValue) EditorGUILayout.PropertyField(_wallsRequiredForStar);
        else EditorGUILayout.PropertyField(_timeRequiredForStar);
        EditorGUILayout.PropertyField(_enemiesInStage);
        serializedObject.ApplyModifiedProperties();
    }
}
