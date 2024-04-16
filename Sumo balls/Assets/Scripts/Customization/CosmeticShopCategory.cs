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

        GridLayoutGroupHelper.GetNumberOfItemsInRow(GetComponent<GridLayoutGroup>(), out int num, 1);
        _topClothesTab.SetSelectableOnDown(_cosmeticButtons[0].GetComponent<Selectable>());
        _middleClothesTab.SetSelectableOnDown(_cosmeticButtons[1].GetComponent<Selectable>());
        _bottomClothesTab.SetSelectableOnDown(_cosmeticButtons[num-1].GetComponent<Selectable>());

        foreach (CosmeticSelectionButton button in _cosmeticButtons)
        {
            button.OnCosmeticPicked += SelectItem;
            button.OnEditColorPressed.AddListener(OpenColorEditWindow);
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
    public void OpenColorEditWindow(CosmeticSO cosmetic)
    {
        _editColorWindow.OpenColorWindow(cosmetic, _category);
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
