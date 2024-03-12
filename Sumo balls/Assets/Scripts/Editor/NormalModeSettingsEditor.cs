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
    SerializedProperty _fallingBalls;
    SerializedProperty _holesInArena;
    SerializedProperty _fallingBallsSettings;
    SerializedProperty _holesInArenaSettings;
    private void OnEnable()
    {
        exclude = new string[] { "_timeRequiredForStar", "_wallsRequiredForStar", "_enemiesInStage" };
        _inCage = serializedObject.FindProperty("_isInCage");
        _wallsRequiredForStar = serializedObject.FindProperty("_wallsRequiredForStar");
        _timeRequiredForStar = serializedObject.FindProperty("_timeRequiredForStar");
        _enemiesInStage = serializedObject.FindProperty("_enemiesInStage");
        _fallingBalls = serializedObject.FindProperty("_fallingBalls");
        _fallingBallsSettings = serializedObject.FindProperty("_fallingBallsSettings");
        _holesInArena = serializedObject.FindProperty("_holesInArena");
        _holesInArenaSettings = serializedObject.FindProperty("_holesInArenaSettings");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject,exclude);
        if (_fallingBalls.boolValue) EditorGUILayout.PropertyField(_fallingBallsSettings);
        if (_holesInArena.boolValue) EditorGUILayout.PropertyField(_holesInArenaSettings);
        if (_inCage.boolValue) EditorGUILayout.PropertyField(_wallsRequiredForStar);
        else EditorGUILayout.PropertyField(_timeRequiredForStar);
        EditorGUILayout.PropertyField(_enemiesInStage);
        serializedObject.ApplyModifiedProperties();
    }
}
