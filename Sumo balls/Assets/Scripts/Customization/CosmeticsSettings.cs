using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsSettings : MonoBehaviour
{
    public static List<CosmeticSO> SelectedCosmetics => _selectedCosmetics;
    [SerializeField] static List<CosmeticSO> _selectedCosmetics;
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
