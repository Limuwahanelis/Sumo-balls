using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItemSelectable
{

    public void SetSelectionTick(bool value);
    /// <summary>
    /// Checks if item is unlocked if not then tries to unlock it if tryUnlock is set to true.
    /// </summary>
    /// <param name="tryUnlock"></param>
    public void CheckItem(bool tryUnlock = true);
    public void TryUnlock();
}
