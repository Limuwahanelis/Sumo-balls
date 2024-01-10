using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridButtonsNaviagtion))]
public class GridButtonsNavigationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Add navigation"))
        {
            (target as GridButtonsNaviagtion).SetUpNavigation();
        }
    }
}
