using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CosmeticSelectionButton : ShopItemSelectionButton, ICosmeticPickable
{
    public event ICosmeticPickable.CosmeticPickedEventHandler OnCosmeticPicked;
    [SerializeField] RawImage _comseticImage;
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] GameObject _coinContainer;
    public override void CheckItem(bool tryUnlock = true)
    {
        if (GameDataManager.IsItemUnlocked(_unlockable.UnlockableItem.Id))
        {
            OnCosmeticPicked?.Invoke(_unlockable.UnlockableItem as CosmeticSO,this);
        }
        else if (tryUnlock)
        {
            _unlockable.TryUnlock();
        }
    }

    public void SetUp(CosmeticSO cosmetic)
    {
        _comseticImage.texture = cosmetic.CosmeticIcon;
        for(int i=0;i<cosmetic.Cost;i++)
        {
            Instantiate(_coinPrefab, _coinContainer.transform);    
        }
        _unlockable.SetUnlockable(cosmetic);
    }
}
