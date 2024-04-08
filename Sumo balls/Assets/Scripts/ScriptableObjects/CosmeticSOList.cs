using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cosmetics/CosmeticsList")]
public class CosmeticSOList : ScriptableObject
{
    public List<CosmeticSO> Cosmetics=>_cosmetics;
    [SerializeField] List<CosmeticSO> _cosmetics = new List<CosmeticSO>();
}
