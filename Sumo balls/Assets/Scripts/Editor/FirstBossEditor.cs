using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(FirstBoss))]
public class FirstBossEditor : Editor
{
    private SerializedProperty _onDeathEvenet;
    private string[] _exclude = { "OnDeath" };
    private void OnEnable()
    {
        _onDeathEvenet = serializedObject.FindProperty("OnDeath");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _exclude);
        EditorGUILayout.PropertyField(_onDeathEvenet);
        serializedObject.ApplyModifiedProperties();
    }
}
