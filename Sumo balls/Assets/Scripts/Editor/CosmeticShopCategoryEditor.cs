using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UI;

[CustomEditor(typeof(CosmeticShopCategory))]
public class CosmeticShopCategoryEditor: Editor
{
    CosmeticSOList _cosmeticsList;
    SerializedProperty _cosmeticSelectionButtonsList;
    SerializedProperty _cosmeticButtonPrefab;
    SerializedProperty _selectOnEnable;
    private void OnEnable()
    {
        _cosmeticsList = serializedObject.FindProperty("_listOfCosmeticsSOInCategory").objectReferenceValue as CosmeticSOList;
        _cosmeticButtonPrefab = serializedObject.FindProperty("_cosmeticButtonPrefab");
        _cosmeticSelectionButtonsList = serializedObject.FindProperty("_cosmeticButtons");

    }
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Reset category"))
        {
            ResetCategory(target as CosmeticShopCategory,serializedObject);
        }
        base.OnInspectorGUI();

    }

    public static void ResetCategory(CosmeticShopCategory cosmeticShopCategory, SerializedObject serializedObject)
    {
        CosmeticSOList cosmeticsList = serializedObject.FindProperty("_listOfCosmeticsSOInCategory").objectReferenceValue as CosmeticSOList;
        SerializedProperty cosmeticButtonPrefab = serializedObject.FindProperty("_cosmeticButtonPrefab");
        SerializedProperty cosmeticSelectionButtonsList = serializedObject.FindProperty("_cosmeticButtons");
        SelectSelectableOnEnable selectOnEnable = serializedObject.FindProperty("_selectSelectableOnEnable").objectReferenceValue as SelectSelectableOnEnable;
        serializedObject.Update();
        for (int i = cosmeticSelectionButtonsList.arraySize - 1; i >= 0; i--)
        {
            CosmeticSelectionButton toDestroy = cosmeticSelectionButtonsList.GetArrayElementAtIndex(i).objectReferenceValue as CosmeticSelectionButton;
            DestroyImmediate(toDestroy.gameObject);
            cosmeticSelectionButtonsList.DeleteArrayElementAtIndex(i);
        }
        for (int i = 0; i < cosmeticsList.Cosmetics.Count; i++)
        {
            cosmeticSelectionButtonsList.InsertArrayElementAtIndex(i);
            CosmeticSelectionButton cosmeticButton = PrefabUtility.InstantiatePrefab(cosmeticButtonPrefab.objectReferenceValue).GetComponent<CosmeticSelectionButton>();
            cosmeticSelectionButtonsList.GetArrayElementAtIndex(i).objectReferenceValue = cosmeticButton;
            cosmeticButton.transform.SetParent(cosmeticShopCategory.transform);
            cosmeticButton.SetUp(cosmeticsList.Cosmetics[i]);
        }
        selectOnEnable.SetSelectable((cosmeticSelectionButtonsList.GetArrayElementAtIndex(0).objectReferenceValue as CosmeticSelectionButton).GetComponent<Button>());
        serializedObject.ApplyModifiedProperties();
    }
}