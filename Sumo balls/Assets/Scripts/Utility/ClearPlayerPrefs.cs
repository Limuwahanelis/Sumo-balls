using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class ClearPlayerPrefs : MonoBehaviour
{

}
#if UNITY_EDITOR
[CustomEditor(typeof(ClearPlayerPrefs))]
public class ClearPlayerPrefsEditor:Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset player prefs"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif