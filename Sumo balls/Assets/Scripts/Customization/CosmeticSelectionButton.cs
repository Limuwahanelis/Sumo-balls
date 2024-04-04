using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticSelectionButton : ShopItemSelectionButton, ICosmeticPickable
{
    public event ICosmeticPickable.CosmeticPickedEventHandler OnCosmeticPicked;

    public override void CheckItem(bool tryUnlock = true)
    {
        if (true)//GameDataManager.IsItemUnlocked(_unlockable.UnlockableItem.Id))
        {
            OnCosmeticPicked?.Invoke(_unlockable.UnlockableItem as CosmeticSO);
        }
        else if (tryUnlock)
        {
            _unlockable.TryUnlock();
        }
    }
}
