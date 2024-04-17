using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CosmeticShopCategory : MonoBehaviour
{
    public enum CosmeticCategory
    {
        TOP,MIDDLE,BOTTOM
    }
    [SerializeField] CosmeticCategory _category;
    [SerializeField] GameObject _gameObjectWithButtons;
    [SerializeField] CosmeticSOList _listOfCosmeticsSOInCategory;
    [SerializeField] GameObject _cosmeticButtonPrefab;
    [SerializeField] List<CosmeticSelectionButton> _cosmeticButtons = new List<CosmeticSelectionButton>();
    [SerializeField] SelectSelectableOnEnable _selectSelectableOnEnable;
    [SerializeField] CosmeticColorSelection _editColorWindow;
    [SerializeField] TabToggleUI _topClothesTab;
    [SerializeField] TabToggleUI _middleClothesTab;
    [SerializeField] TabToggleUI _bottomClothesTab;
    [SerializeField] Selectable _returnToMainMenuButton;
    private CosmeticSelectionButton _currentlySelectedCosmeticButton;
    public UnityEvent<CosmeticSO, CosmeticCategory> OnItemSelected;
    // Start is called before the first frame update
    void Awake()
    {
        _cosmeticButtons = _gameObjectWithButtons.GetComponentsInChildren<CosmeticSelectionButton>().ToList();
    }
    private void OnEnable()
    {
        switch (_category)
        {
            case CosmeticCategory.TOP: _currentlySelectedCosmeticButton = _cosmeticButtons.Find(x => x.GetComponent<Unlockable>().UnlockableItem.Id == GameDataManager.GameData.customizationData.topCosmeticId);break; //SelectItem(_currentlySelectedCosmeticButton) break;
            case CosmeticCategory.MIDDLE: _currentlySelectedCosmeticButton = _cosmeticButtons.Find(x => x.GetComponent<Unlockable>().UnlockableItem.Id == GameDataManager.GameData.customizationData.midddleCosmeticId); break;
            case CosmeticCategory.BOTTOM: _currentlySelectedCosmeticButton = _cosmeticButtons.Find(x => x.GetComponent<Unlockable>().UnlockableItem.Id == GameDataManager.GameData.customizationData.bottomCosmeticId); break;
        }
        _currentlySelectedCosmeticButton.CheckItem(false);
        _currentlySelectedCosmeticButton.SetSelectionTick(true);

        
        StartCoroutine(WaitFrame());
        foreach (CosmeticSelectionButton button in _cosmeticButtons)
        {
            button.CheckItem(false);
            button.OnCosmeticPicked += SelectItem;
            button.OnEditColorPressed.AddListener(OpenColorEditWindow);
        }
    }
    IEnumerator WaitFrame()
    {
        yield return null;
        GridLayoutGroupHelper.GetNumberOfItemsInRow(GetComponent<GridLayoutGroup>(), out int num, 1);
        _topClothesTab.SetSelectableOnDown(_cosmeticButtons[0].GetComponent<Selectable>());
        _middleClothesTab.SetSelectableOnDown(_cosmeticButtons[1].GetComponent<Selectable>());
        _bottomClothesTab.SetSelectableOnDown(_cosmeticButtons[num - 1].GetComponent<Selectable>());
        SetNavigation();
    }
    private void SetNavigation()
    {
        int numberOfRowsInGrid;
        int numberOfItemsInRow;
        GridLayoutGroupHelper.GetColumnAndRow(GetComponent<GridLayoutGroup>(), out _, out numberOfRowsInGrid);
        for (int i = 0; i < numberOfRowsInGrid; i++)
        {
            GridLayoutGroupHelper.GetNumberOfItemsInRow(GetComponent<GridLayoutGroup>(), out numberOfItemsInRow, i + 1);
            for (int j = 0; j < numberOfItemsInRow; j++)
            {
                NavigationSetter navSet = _cosmeticButtons[i+j].GetComponent<NavigationSetter>();
                NavigationSetter editButtonNavSet = navSet.transform.GetChild(4).GetComponent<NavigationSetter>();
                navSet.SetNavigationMode(true);
                editButtonNavSet.SetNavigationMode(true);
                if (i == 0)
                {
                    if (j == 0)
                    {
                        //CosmeticSelectionButtonEditor.SetNavigation(topClothesTabToggle, (cosmeticSelectionButtonsList.GetArrayElementAtIndex(i * numberOfItemsInRow + j + 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>(), null, (cosmeticSelectionButtonsList.GetArrayElementAtIndex((i + 1) * numberOfItemsInRow - 1).objectReferenceValue as CosmeticSelectionButton).GetComponent<Selectable>(), serButton);

                        navSet.SetSelectableOnTop(_topClothesTab);
                        navSet.SetSelectableOnLeft(_cosmeticButtons[(i + 1) * numberOfItemsInRow - 1].GetComponent<Selectable>());
                        navSet.SetSelectableOnRight(_cosmeticButtons[i * numberOfItemsInRow + j + 1].GetComponent<Selectable>());
                        if (_cosmeticButtons.Count > (i + 1) * numberOfItemsInRow + j) _cosmeticButtons[i+j].SetEditButtonNavigation(_cosmeticButtons[(i + 1) * numberOfItemsInRow + j].GetComponent<Selectable>());
                        else _cosmeticButtons[i + j].SetEditButtonNavigation(_returnToMainMenuButton);

                        continue;
                    }
                    if (j <= numberOfItemsInRow - 2)
                    {

                        navSet.SetSelectableOnTop(_middleClothesTab);
                        navSet.SetSelectableOnLeft(_cosmeticButtons[i * numberOfItemsInRow + j - 1].GetComponent<Selectable>());
                        navSet.SetSelectableOnRight(_cosmeticButtons[i * numberOfItemsInRow + j + 1].GetComponent<Selectable>());
                    }
                    else
                    {
                        navSet.SetSelectableOnTop(_bottomClothesTab);
                        navSet.SetSelectableOnLeft(_cosmeticButtons[i * numberOfItemsInRow + j - 1].GetComponent<Selectable>());
                        navSet.SetSelectableOnRight(_cosmeticButtons[ i * numberOfItemsInRow].GetComponent<Selectable>());
                    }
                    if (_cosmeticButtons.Count > (i + 1) * numberOfItemsInRow + j) _cosmeticButtons[i + j].SetEditButtonNavigation(_cosmeticButtons[(i + 1) * numberOfItemsInRow + j].GetComponent<Selectable>());
                    else _cosmeticButtons[i + j].SetEditButtonNavigation(_returnToMainMenuButton);
                }
                else
                {
                    if (i == numberOfRowsInGrid - 1)
                    {
                        _cosmeticButtons[i + j].SetEditButtonNavigation(_returnToMainMenuButton);
                        navSet.SetSelectableOnTop(_cosmeticButtons[(i - 1) * numberOfItemsInRow + j].GetComponent<Selectable>());
                        if (j == 0)
                        {
                            navSet.SetSelectableOnLeft(_cosmeticButtons[(i + 1) * numberOfItemsInRow - 1].GetComponent<Selectable>());
                            navSet.SetSelectableOnRight(_cosmeticButtons[i * numberOfItemsInRow + j].GetComponent<Selectable>());
                        }
                        else if (j <= numberOfRowsInGrid - 2)
                        {
                            navSet.SetSelectableOnLeft(_cosmeticButtons[i * numberOfItemsInRow + j - 1].GetComponent<Selectable>());
                            navSet.SetSelectableOnRight(_cosmeticButtons[i * numberOfItemsInRow + j + 1].GetComponent<Selectable>());
                        }
                        else if (j == numberOfRowsInGrid - 1)
                        {
                            navSet.SetSelectableOnLeft(_cosmeticButtons[i * numberOfItemsInRow + j - 1].GetComponent<Selectable>());
                            navSet.SetSelectableOnRight(_cosmeticButtons[i * numberOfItemsInRow].GetComponent<Selectable>());
                        }
                    }
                    else
                    {
                        navSet.SetSelectableOnTop(_cosmeticButtons[(i - 1) * numberOfItemsInRow + j].GetComponent<Selectable>());
                        if (_cosmeticButtons.Count> (i + 1) * numberOfItemsInRow + j) _cosmeticButtons[i + j].SetEditButtonNavigation(_cosmeticButtons[(i + 1) * numberOfItemsInRow + j].GetComponent<Selectable>());
                        else _cosmeticButtons[i + j].SetEditButtonNavigation(_returnToMainMenuButton);
                        if (j == 0)
                        {
                            navSet.SetSelectableOnLeft(_cosmeticButtons[(i + 1) * numberOfItemsInRow - 1].GetComponent<Selectable>());
                            navSet.SetSelectableOnRight(_cosmeticButtons[i * numberOfItemsInRow + j + 1].GetComponent<Selectable>());
                        }
                        else if (j <= numberOfRowsInGrid - 2)
                        {
                            navSet.SetSelectableOnLeft(     _cosmeticButtons[i * numberOfItemsInRow + j - 1].GetComponent<Selectable>());
                            navSet.SetSelectableOnRight(_cosmeticButtons[i * numberOfItemsInRow + j + 1].GetComponent<Selectable>());
                        }
                        else if (j == numberOfRowsInGrid - 1)
                        {
                            navSet.SetSelectableOnLeft(_cosmeticButtons[i * numberOfItemsInRow + j - 1].GetComponent<Selectable>());
                            navSet.SetSelectableOnRight(_cosmeticButtons[i * numberOfItemsInRow].GetComponent<Selectable>());
                        }
                    }
                }
            }
        }
    }
    private void SelectItem(CosmeticSO cosmetic, ICosmeticPickable caller)
    {
        _currentlySelectedCosmeticButton.SetSelectionTick(false);
        _currentlySelectedCosmeticButton = _cosmeticButtons.Find(x => (x as ICosmeticPickable) == caller);
        _currentlySelectedCosmeticButton.SetSelectionTick(true);
        switch (_category)
        {
            case CosmeticCategory.TOP: GameDataManager.GameData.customizationData.topCosmeticId = cosmetic.Id; break; //SelectItem(_currentlySelectedCosmeticButton) break;
            case CosmeticCategory.MIDDLE: GameDataManager.GameData.customizationData.midddleCosmeticId = cosmetic.Id; break;
            case CosmeticCategory.BOTTOM: GameDataManager.GameData.customizationData.bottomCosmeticId = cosmetic.Id; break;
        }
        GameDataManager.Save();
        // add set tick for selected item
        OnItemSelected?.Invoke(cosmetic, _category);
    }
    public void OpenColorEditWindow(CosmeticSO cosmetic,Selectable caller)
    {
        _editColorWindow.OpenColorWindow(cosmetic, _category, caller);
    }
    private void OnDisable()
    {
        foreach (CosmeticSelectionButton button in _cosmeticButtons)
        {
            button.OnCosmeticPicked -= SelectItem;
            button.OnEditColorPressed.RemoveListener(OpenColorEditWindow);
        }
    }
    private void OnDestroy()
    {

    }
}
