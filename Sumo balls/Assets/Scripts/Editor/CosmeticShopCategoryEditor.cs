using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor.VersionControl;
using Unity.EditorCoroutines.Editor;

[CustomEditor(typeof(CosmeticShopCategory))]
[CanEditMultipleObjects]
public class CosmeticShopCategoryEditor: Editor
{
    private void OnEnable()
    {

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
        Selectable closeButton = serializedObject.FindProperty("_returnToMainMenuButton").objectReferenceValue as Selectable;
        TabToggleUI topClothesTabToggle = serializedObject.FindProperty("_topClothesTab").objectReferenceValue as TabToggleUI;
        TabToggleUI middleClothesTabToggle = serializedObject.FindProperty("_middleClothesTab").objectReferenceValue as TabToggleUI;
        TabToggleUI bottomClothesTabToggle = serializedObject.FindProperty("_bottomClothesTab").objectReferenceValue as TabToggleUI;
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
         EditorCoroutineUtility.StartCoroutineOwnerless(WaitFrame(cosmeticShopCategory, cosmeticSelectionButtonsList, topClothesTabToggle, middleClothesTabToggle, bottomClothesTabToggle, closeButton, serializedObject));

        selectOnEnable.SetSelectable((cosmeticSelectionButtonsList.GetArrayElementAtIndex(0).objectReferenceValue as CosmeticSelectionButton).GetComponent<Button>());
        serializedObject.ApplyModifiedProperties();
    }
    private static void SetNavigation(CosmeticShopCategory cosmeticShopCategory, SerializedProperty cosmeticSelectionButtonsList,
        TabToggleUI topClothesTabToggle, TabToggleUI middleClothesTabToggle, TabToggleUI bottomClothesTabToggle, Selectable closeButton, SerializedObject serializedObject)
    {
        serializedObject.Update();
        int numberOfRowsInGrid;
        int numberOfItemsInRow;
        GridLayoutGroupHelper.GetColumnAndRow(cosmeticShopCategory.GetComponent<GridLayoutGroup>(), out _, out numberOfRowsInGrid);
        for (int i = 0; i < numberOfRowsInGrid; i++)
        {
            GridLayoutGroupHelper.GetNumberOfItemsInRow(cosmeticShopCategory.GetComponent<GridLayoutGroup>(), out numberOfItemsInRow, i + 1);
            for (int j = 0; j < numberOfItemsInRow; j++)
            {
                NavigationSetter navSet = (cosmeticSelectionButtonsList.GetArrayElementAtIndex(i + j).objectReferenceValue as CosmeticSelectionButton).GetComponent<NavigationSetter>();
                NavigationSetter editButtonNavSet = navSet.transform.GetChild(4).GetComponent<NavigationSetter>();

                if (i == 0)
                {
                    if (j == 0)
                    {
                        navSet.SetSelectableOnTop(topClothesTabToggle);
                        navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex((i + 1) * numberOfItemsInRow - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j + 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        if (cosmeticSelectionButtonsList.arraySize > (i + 1) * numberOfItemsInRow + j) editButtonNavSet.SetSelectableOnDown((cosmeticSelectionButtonsList.GetArrayElementAtIndex((i + 1) * numberOfItemsInRow + j).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        else editButtonNavSet.SetSelectableOnDown(closeButton);

                        continue;
                    }
                    if (j <= numberOfItemsInRow - 2)
                    {

                        navSet.SetSelectableOnTop(middleClothesTabToggle);
                        navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j + 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                    }
                    else
                    {
                        navSet.SetSelectableOnTop(bottomClothesTabToggle);
                        navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                    }
                    if (cosmeticSelectionButtonsList.arraySize > (i + 1) * numberOfItemsInRow + j) editButtonNavSet.SetSelectableOnDown((cosmeticSelectionButtonsList.GetArrayElementAtIndex((i + 1) * numberOfItemsInRow + j).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                    else editButtonNavSet.SetSelectableOnDown(closeButton);
                }
                else
                {
                    if (i == numberOfRowsInGrid - 1)
                    {
                        editButtonNavSet.SetSelectableOnDown(closeButton);
                        navSet.SetSelectableOnTop((cosmeticSelectionButtonsList.GetArrayElementAtIndex((i - 1) * numberOfItemsInRow + j).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        if (j == 0)
                        {
                            navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex((i + 1) * numberOfItemsInRow - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                            navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        }
                        else if (j <= numberOfRowsInGrid - 2)
                        {
                            navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                            navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j + 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        }
                        else if (j == numberOfRowsInGrid - 1)
                        {
                            navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                            navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        }
                    }
                    else
                    {
                        navSet.SetSelectableOnTop((cosmeticSelectionButtonsList.GetArrayElementAtIndex((i - 1) * numberOfItemsInRow + j).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        if (cosmeticSelectionButtonsList.arraySize > (i + 1) * numberOfItemsInRow + j) editButtonNavSet.SetSelectableOnDown((cosmeticSelectionButtonsList.GetArrayElementAtIndex((i + 1) * numberOfItemsInRow + j).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        else editButtonNavSet.SetSelectableOnDown(closeButton);
                        if (j == 0)
                        {
                            navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex((i + 1) * numberOfItemsInRow - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                            navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j + 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        }
                        else if (j <= numberOfRowsInGrid - 2)
                        {
                            navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                            navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j + 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        }
                        else if (j == numberOfRowsInGrid - 1)
                        {
                            navSet.SetSelectableOnLeft((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                            navSet.SetSelectableOnRight((cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>());
                        }
                    }
                }
            }

        }
        serializedObject.ApplyModifiedProperties();
    }
    private static IEnumerator WaitFrame(CosmeticShopCategory cosmeticShopCategory, SerializedProperty cosmeticSelectionButtonsList,
        TabToggleUI topClothesTabToggle, TabToggleUI middleClothesTabToggle, TabToggleUI bottomClothesTabToggle, Selectable closeButton, SerializedObject serializedObject)
    {
        yield return null;
        SetNavigation(cosmeticShopCategory,cosmeticSelectionButtonsList,topClothesTabToggle,middleClothesTabToggle,bottomClothesTabToggle,closeButton,serializedObject);
    }
}