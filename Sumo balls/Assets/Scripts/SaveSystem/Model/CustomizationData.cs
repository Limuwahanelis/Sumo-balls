using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CustomizationData
{
    public string usedColorUnlockId = "24d6f65d07fc4904e8e12a531837887b"; // default color id for black
    public Color colorPickerColor=Color.black;
    public Color playerColor = Color.black;
    public List<UnlockableItemData> unlockableItemsData = new List<UnlockableItemData>();
    public CustomizationData(List<UnlockableItem> unlockableItems)
    { 
        foreach (UnlockableItem item in unlockableItems)
        {
            unlockableItemsData.Add(new UnlockableItemData(item.Id, item.IsUnlocked));
        }
    }
    public CustomizationData(string colorUnlockId,Color pickerColor, List<UnlockableItem> unlockableItems)
    {
        usedColorUnlockId = colorUnlockId;
        colorPickerColor = pickerColor;
        foreach (UnlockableItem item in unlockableItems)
        {
            unlockableItemsData.Add(new UnlockableItemData(item.Id, item.IsUnlocked));
        }
    }


}
