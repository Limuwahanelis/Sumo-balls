using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnlockableItem))]
[CanEditMultipleObjects]
public class UnlockableItemEditor : Editor
{
    SerializedProperty _isInitalised;
    SerializedProperty _id;
    SerializedProperty _instanceId;
    UnlockableItem item;
    private void OnEnable()
    {
         item = target as UnlockableItem;
        _isInitalised = serializedObject.FindProperty("_isInitalised");
        _id = serializedObject.FindProperty("_id");
        _instanceId = serializedObject.FindProperty("_instanceId");

    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        //if (_isInitalised.boolValue == false) Initalise();
       // _instanceId.intValue = item.GetInstanceID();
        serializedObject.ApplyModifiedProperties();
    }
    private void Initalise()
    {
        _id.stringValue = GUID.Generate().ToString();
        _isInitalised.boolValue = true;
    }
}
