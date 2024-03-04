using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SurvivalModeSettings))]
public class SurvivalModeSettingsEditor: Editor
{
    string[] exclude;
    SerializedProperty _enemiesInStage;
    SerializedProperty _fallingBalls;
    SerializedProperty _fallingBallsSettings;
    SerializedProperty _enemiesToDefeatForStar;
    private void OnEnable()
    {
        exclude = new string[] {"_enemiesInStage", "_enemiesToDefeatForStar" };
        _enemiesInStage = serializedObject.FindProperty("_enemiesInStage");
        _fallingBalls = serializedObject.FindProperty("_fallingBalls");
        _fallingBallsSettings = serializedObject.FindProperty("_fallingBallsSettings");
        _enemiesToDefeatForStar = serializedObject.FindProperty("_enemiesToDefeatForStar");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, exclude);
        if (_fallingBalls.boolValue) EditorGUILayout.PropertyField(_fallingBallsSettings);
        EditorGUILayout.PropertyField(_enemiesToDefeatForStar);
        EditorGUILayout.PropertyField(_enemiesInStage);
        serializedObject.ApplyModifiedProperties();
    }
}