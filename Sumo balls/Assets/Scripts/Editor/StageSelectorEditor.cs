using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(StageSelector))]
public class StageSelectorEditor : Editor
{
    SerializedProperty _stagesInGrid;
    SerializedProperty _stageButtonPrefab;
    SerializedProperty _selectSelectableOnEnable;
    StageList _stagelist;
    StageSelector _stageSelector;
    void OnEnable()
    {
        _stageSelector = target as StageSelector;
        _selectSelectableOnEnable = serializedObject.FindProperty("_selectSelectableOnEnable");
        _stageButtonPrefab = serializedObject.FindProperty("_stagePrefab");
        _stagesInGrid = serializedObject.FindProperty("_stagesInGrid");
        _stagelist = serializedObject.FindProperty("_stageList").objectReferenceValue as StageList;
        serializedObject.Update();
        for (int i = _stagesInGrid.arraySize - 1; i >= 0; i--)
        {
            StageInGrid toDestroy = _stagesInGrid.GetArrayElementAtIndex(i).objectReferenceValue as StageInGrid;
            DestroyImmediate(toDestroy.gameObject);
            _stagesInGrid.DeleteArrayElementAtIndex(i);
        }
        for (int i = 0; i < _stagelist.stages.Count; i++)
        {
            _stagesInGrid.InsertArrayElementAtIndex(i);
            StageInGrid stageInGrid = PrefabUtility.InstantiatePrefab(_stageButtonPrefab.objectReferenceValue) as StageInGrid;
            _stagesInGrid.GetArrayElementAtIndex(i).objectReferenceValue = stageInGrid;
            stageInGrid.transform.SetParent(_stageSelector.transform);
            stageInGrid.SetStage(_stagelist.stages.ElementAt(i));
            stageInGrid.SetIndex(i+1);
            stageInGrid.SetStageIcon(_stagelist.stages.ElementAt(i).stageScreenshot);
        }
        (_selectSelectableOnEnable.objectReferenceValue as SelectSelectableOnEnable).SetSelectable(((_stagesInGrid.GetArrayElementAtIndex(0).objectReferenceValue) as StageInGrid).GetComponent<Button>());
        serializedObject.ApplyModifiedProperties();
    }
}
