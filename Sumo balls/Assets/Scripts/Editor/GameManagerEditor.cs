using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GameModeManager),true)]
public class GameManagerEditor : Editor
{
    SerializedProperty _onResetStage;
    SerializedProperty _onStageCompleted;
    SerializedProperty _onStageFailed;
    SerializedProperty _onStageStart;
    SerializedProperty _debug;
    SerializedProperty _debugSkipCountdown;
    SerializedProperty _debugGameModeSettings;
    List<string> _toExlcude;
    private void OnEnable()
    {
        _onResetStage = serializedObject.FindProperty("OnResetStage");
        _onStageCompleted = serializedObject.FindProperty("OnStageCompleted");
        _onStageFailed = serializedObject.FindProperty("OnStageFailed");
        _onStageStart = serializedObject.FindProperty("OnStageStarted");
        _debugGameModeSettings = serializedObject.FindProperty("_debugSettings");
        _debug = serializedObject.FindProperty("debug");
        _toExlcude = new List<string>() { "OnResetStage", "OnStageCompleted","OnStageFailed", "OnStageStarted", "_debugSettings", "_skipCountdown", "debug" };
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, _toExlcude.ToArray());
        EditorGUILayout.PropertyField(_debug);
        if (_debug.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_debugGameModeSettings);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.PropertyField(_onResetStage);
        EditorGUILayout.PropertyField(_onStageStart);
        EditorGUILayout.PropertyField(_onStageCompleted);
        EditorGUILayout.PropertyField(_onStageFailed);
        serializedObject.ApplyModifiedProperties();
    }
}
