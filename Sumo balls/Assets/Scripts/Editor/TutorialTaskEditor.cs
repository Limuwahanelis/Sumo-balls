using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TutorialTask), true)]
[CanEditMultipleObjects]
public class TutorialTaskEditor : Editor
{
    private SerializedProperty _onTaskStarted;
    private SerializedProperty _onTaskCompleted;
    private string[] _toExclude = { "OnTaskStarted", "OnTaskCompleted" };
    private void Awake()
    {
        _onTaskStarted = serializedObject.FindProperty("OnTaskStarted");
        _onTaskCompleted = serializedObject.FindProperty("OnTaskCompleted");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _toExclude);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_onTaskStarted);
        EditorGUILayout.PropertyField(_onTaskCompleted);
        serializedObject.ApplyModifiedProperties();
    }

}
