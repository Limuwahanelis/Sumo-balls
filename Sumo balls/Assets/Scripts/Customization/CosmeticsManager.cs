using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsManager : MonoBehaviour
{
    [SerializeField] GameObject _playerBody;
    // Start is called before the first frame update
    void Start()
    {
        foreach (CosmeticSO cosmetic in CosmeticsSettings.SelectedCosmetics)
        {
            Cosmetic cos = Instantiate(cosmetic.Prefab,_playerBody.transform).GetComponent<Cosmetic>();
            cos.SpawnCosmeticLocally();
            cos.SetColors(cosmetic.Colors);
        }
    }
}
