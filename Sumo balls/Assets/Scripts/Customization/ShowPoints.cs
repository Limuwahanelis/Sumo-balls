using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ShowPoints : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    void Update()
    {
        _text.text=GameDataManager.Points.ToString();
    }

}
#if UNITY_EDITOR

[CustomEditor(typeof(ShowPoints))]
public class ShowPointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Add Points"))
        {
            if (Application.isPlaying)
            {
                GameDataManager.IncreasePoints(10);
            }
        }
        if (GUILayout.Button("Force reset points"))
        {
            GameDataManager.IncreasePoints(10);
        }
    }
}

#endif
