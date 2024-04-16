using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CosmeticSelectionButton :MonoBehaviour, IShopItemSelectable, ICosmeticPickable
{
    public event ICosmeticPickable.CosmeticPickedEventHandler OnCosmeticPicked;
    public UnityEvent<CosmeticSO> OnEditColorPressed;
    [SerializeField] RawImage _comseticImage;
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] GameObject _coinContainer;
    [SerializeField] Button _editColorButton;
    [SerializeField,HideInInspector] private bool _hasEditableColors;
    [SerializeField] protected GameObject _selectedTick;
    protected Unlockable _unlockable;
    public void CheckItem(bool tryUnlock = true)
    {
        if (GameDataManager.IsItemUnlocked(_unlockable.UnlockableItem.Id))
        {
            OnCosmeticPicked?.Invoke(_unlockable.UnlockableItem as CosmeticSO,this);
            if(_hasEditableColors) _editColorButton.gameObject.SetActive(true);
        }
        else if (tryUnlock)
        {
            _unlockable.TryUnlock();
        }
    }
    public void EditColorPressed()
    {
        OnEditColorPressed?.Invoke(_unlockable.UnlockableItem as CosmeticSO);
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

    public void SetSelectionTick(bool value)
    {
        _selectedTick.SetActive(value);
    }

    public void TryUnlock()
    {
        _unlockable.TryUnlock();
    }
}
