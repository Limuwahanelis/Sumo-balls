using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CosmeticSelectionButton: ShopItemSelectionButton, ICosmeticPickable
{
    public event ICosmeticPickable.CosmeticPickedEventHandler OnCosmeticPicked;
    public UnityEvent<CosmeticSO,Selectable> OnEditColorPressed;
    [SerializeField] RawImage _comseticImage;
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] GameObject _coinContainer;
    [SerializeField] Button _editColorButton;
    [SerializeField,HideInInspector] private bool _hasEditableColors;
    public override void CheckItem(bool tryUnlock = true)
    {
        if (GameDataManager.IsItemUnlocked(_unlockable.UnlockableItem.Id))
        {
            OnCosmeticPicked?.Invoke(_unlockable.UnlockableItem as CosmeticSO,this);
            if(_hasEditableColors) _editColorButton.gameObject.SetActive(true);
        }
        else if (tryUnlock)
        {
            if(_unlockable.TryUnlock())
            {
                if (_hasEditableColors) _editColorButton.gameObject.SetActive(true);
            }
        }
    }
    public void SetEditButtonNavigation(Selectable onDown)
    {
        if(_hasEditableColors) _editColorButton.GetComponent<NavigationSetter>().SetSelectableOnDown(onDown);
        else GetComponent<NavigationSetter>().SetSelectableOnDown(onDown);
    }
    public void EditColorPressed()
    {
        OnEditColorPressed?.Invoke(_unlockable.UnlockableItem as CosmeticSO,GetComponent<Selectable>());
    }
    public void SetUp(CosmeticSO cosmetic)
    {
        _comseticImage.texture = cosmetic.CosmeticIcon;
        for(int i=0;i<cosmetic.Cost;i++)
        {
            Instantiate(_coinPrefab, _coinContainer.transform);    
        }
        _unlockable.SetUnlockable(cosmetic);
        if (cosmetic.PartsNames.Count!=0) _hasEditableColors = true;
    }
}
