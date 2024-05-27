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
    public List<CosmeticData> cosmeticsData = new List<CosmeticData>();
    public string topCosmeticId= "e9076c53d7a4a9d489e3b10363614ddb"; // default top
    public string midddleCosmeticId= "f194953c1695ede4480f4f7ce0c8b0f9"; // default middle
    public string bottomCosmeticId = "ece10e96e1dc5bb4ba8f17b3216725ee"; // default bottom
    public CustomizationData(List<UnlockableItem> unlockableItems,List<CosmeticSO> cosmeticsSO)
    { 
        foreach (UnlockableItem item in unlockableItems)
        {
            unlockableItemsData.Add(new UnlockableItemData(item.Id, item.StartUnlocked));
        }
        foreach(CosmeticSO cosmetic in cosmeticsSO)
        {
            cosmeticsData.Add(new CosmeticData(cosmetic.Id,cosmetic.Colors));
        }
    }


}
