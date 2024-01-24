using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GameModeManager),true)]
public class GameManagerEditor : Editor
{
    SerializedProperty _onResetStage;
    SerializedProperty _onStageCompleted;
    SerializedProperty _onStageFailed;
    List<string> _toExlcude;
    private void OnEnable()
    {
        _onResetStage = serializedObject.FindProperty("OnResetStage");
        _onStageCompleted = serializedObject.FindProperty("OnStageCompleted");
        _onStageFailed = serializedObject.FindProperty("OnStageFailed");
        _toExlcude = new List<string>() { "OnResetStage", "OnStageCompleted","OnStageFailed" };
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _toExlcude.ToArray());
        EditorGUILayout.PropertyField(_onResetStage);
        EditorGUILayout.PropertyField(_onStageCompleted);
        EditorGUILayout.PropertyField(_onStageFailed);
        serializedObject.ApplyModifiedProperties();
    }
}
