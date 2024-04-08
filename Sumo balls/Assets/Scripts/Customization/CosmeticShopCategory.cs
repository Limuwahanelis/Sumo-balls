using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CosmeticShopCategory : MonoBehaviour
{
    public enum CosmeticCategory
    {
        TOP,MIDDLE,BOTTOM
    }
    [SerializeField] CosmeticCategory category;
    [SerializeField] GameObject _gameObjectWithButtons;
    [SerializeField] CosmeticSOList _listOfCosmeticsSOInCategory;
    [SerializeField] GameObject _cosmeticButtonPrefab;
    [SerializeField] List<CosmeticSelectionButton> _cosmeticButtons = new List<CosmeticSelectionButton>();
    private CosmeticSelectionButton _currentlySelectedCosmeticButton;
    public UnityEvent<CosmeticSO, CosmeticCategory> OnItemSelected;
    // Start is called before the first frame update
    void Awake()
    {
        _cosmeticButtons = _gameObjectWithButtons.GetComponentsInChildren<CosmeticSelectionButton>().ToList();
    }
    private void OnEnable()
    {
        switch (category)
        {
            case CosmeticCategory.TOP: _currentlySelectedCosmeticButton = _cosmeticButtons.Find(x => x.GetComponent<Unlockable>().UnlockableItem.Id == GameDataManager.GameData.customizationData.topCosmeticId);break; //SelectItem(_currentlySelectedCosmeticButton) break;
            case CosmeticCategory.MIDDLE: _currentlySelectedCosmeticButton = _cosmeticButtons.Find(x => x.GetComponent<Unlockable>().UnlockableItem.Id == GameDataManager.GameData.customizationData.midddleCosmeticId); break;
            case CosmeticCategory.BOTTOM: _currentlySelectedCosmeticButton = _cosmeticButtons.Find(x => x.GetComponent<Unlockable>().UnlockableItem.Id == GameDataManager.GameData.customizationData.bottomCosmeticId); break;
        }
        _currentlySelectedCosmeticButton.SetSelectionTick(true);
        
        foreach (CosmeticSelectionButton button in _cosmeticButtons)
        {
            button.OnCosmeticPicked += SelectItem;
        }
    }
    private void SelectItem(CosmeticSO cosmetic, ICosmeticPickable caller)
    {
        _currentlySelectedCosmeticButton.SetSelectionTick(false);
        _currentlySelectedCosmeticButton = _cosmeticButtons.Find(x => (x as ICosmeticPickable) == caller);
        _currentlySelectedCosmeticButton.SetSelectionTick(true);
        switch (category)
        {
            case CosmeticCategory.TOP: GameDataManager.GameData.customizationData.topCosmeticId = cosmetic.Id; break; //SelectItem(_currentlySelectedCosmeticButton) break;
            case CosmeticCategory.MIDDLE: GameDataManager.GameData.customizationData.midddleCosmeticId = cosmetic.Id; break;
            case CosmeticCategory.BOTTOM: GameDataManager.GameData.customizationData.bottomCosmeticId = cosmetic.Id; break;
        }
        GameDataManager.Save();
        // add set tick for selected item
        OnItemSelected?.Invoke(cosmetic, category);
    }
    private void SetCosmeticForPlayer(CosmeticSO cosmeticSO, CosmeticSelectionButton button)
    {

    }
    //public void ResetCategory()
    //{
    //    for(int i=_cosmeticButtons.Count-1;i>=0;i--)
    //    {
    //        CosmeticSelectionButton toDestroy = _cosmeticButtons[i];
    //        DestroyImmediate(toDestroy.gameObject);
    //        _cosmeticButtons.RemoveAt(i);
    //    }
    //    for(int i=0;i<_cosmeticButtons.Count;i++) 
    //    {
    //        CosmeticSelectionButton button = Instantiate
    //    }
    //}
    private void OnDisable()
    {
        foreach (CosmeticSelectionButton button in _cosmeticButtons)
        {
            button.OnCosmeticPicked -= SelectItem;
        }
    }
    private void OnDestroy()
    {

    }
}
