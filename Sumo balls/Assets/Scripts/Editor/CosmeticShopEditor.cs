using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CosmeticsShop))]
public class CosmeticShopEditor: Editor
{
    SerializedProperty _shopCategories;
    private void OnEnable()
    {

        //aa.tar
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Reset all categories"))
        {
            ResetShop();
        }
    }
    private void ResetShop()
    {
        _shopCategories = serializedObject.FindProperty("_categories");
        for (int i = 0; i < _shopCategories.arraySize; i++)
        {
            CosmeticShopCategory cat = _shopCategories.GetArrayElementAtIndex(i).objectReferenceValue as CosmeticShopCategory;
            SerializedObject ser = new SerializedObject(cat);
            CosmeticShopCategoryEditor.ResetCategory(cat, ser);
        }
    }
}