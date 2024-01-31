using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnlockableItemData
{
    public string itemId;
    public bool isUnlocked;

    public UnlockableItemData(string itemId, bool isUnlocked)
    {
        this.itemId = itemId;
        this.isUnlocked = isUnlocked;
    }   
}
