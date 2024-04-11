using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsSettings : MonoBehaviour
{
    //  1 item is top
    //  2 item is middle
    //  3 item is bottom
    public static List<CosmeticSO> SelectedCosmetics => _selectedCosmetics;
    [SerializeField] static List<CosmeticSO> _selectedCosmetics= new List<CosmeticSO>();
    public void SwapCosmetic(CosmeticSO newCosmetic, CosmeticShopCategory.CosmeticCategory cosmeticCategory)
    {
        _selectedCosmetics[((int)cosmeticCategory)] = newCosmetic;
    }
    public void InitateList(CosmeticSO upperCosmetic, CosmeticSO middleCosmetic, CosmeticSO bottomCosmetic)
    {
        _selectedCosmetics.Add(upperCosmetic);
        _selectedCosmetics.Add(middleCosmetic);
        _selectedCosmetics.Add(bottomCosmetic);
    }
    public void AddCosmeticToList(CosmeticSO cosmetic)
    {
        if (_selectedCosmetics.Contains(cosmetic))
        {
            Debug.Log($"Cosmetic {cosmetic} is already in the list");
            return;
        }
        _selectedCosmetics.Add(cosmetic);
    }
    public void RemoveCosmeticFromList(CosmeticSO cosmetic)
    {
        _selectedCosmetics.Remove(cosmetic);
    }
}
