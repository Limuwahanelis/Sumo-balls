using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CosmeticShopCategory;

public class CosmeticsShop : MonoBehaviour
{
    [SerializeField] CosmeticsSettings _cosmeticsSettings;
    [SerializeField] CosmeticSO _selectedTopCosmetic;
    [SerializeField] CosmeticSO _selectedMiddleCosmetic;
    [SerializeField] CosmeticSO _selectedBottomCosmetic;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Used by UnityEvent in shop category script
    public void AddCosmeticToSelectedCosmeticList(CosmeticSO cosmetic, CosmeticCategory category)
    {
        switch (category)
        {
            case CosmeticCategory.TOP:
                {
                    _cosmeticsSettings.RemoveCosmeticFromList(_selectedTopCosmetic);
                    _selectedTopCosmetic = cosmetic;
                    _cosmeticsSettings.AddCosmeticToList(_selectedTopCosmetic);
                    break;
                }
            case CosmeticCategory.MIDDLE:
                {
                    _cosmeticsSettings.RemoveCosmeticFromList(_selectedMiddleCosmetic);
                    _selectedMiddleCosmetic = cosmetic;
                    _cosmeticsSettings.AddCosmeticToList(_selectedMiddleCosmetic);
                    break;
                }
            case CosmeticCategory.LOWER:
                {
                    _cosmeticsSettings.RemoveCosmeticFromList(_selectedBottomCosmetic);
                    _selectedBottomCosmetic = cosmetic;
                    _cosmeticsSettings.AddCosmeticToList(_selectedBottomCosmetic);
                    break;
                }
        }
    }
}
