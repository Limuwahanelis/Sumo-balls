using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CosmeticShopCategory;

public class CosmeticsShop : MonoBehaviour
{
    [SerializeField] CosmeticsSettings _cosmeticsSettings;
    [SerializeField] CosmeticSO _selectedTopCosmeticSO;
    [SerializeField] CosmeticSO _selectedMiddleCosmeticSO;
    [SerializeField] CosmeticSO _selectedBottomCosmeticSO;
    [SerializeField] Transform _playerShowcase;

    [SerializeField] List<CosmeticShopCategory> _categories= new List<CosmeticShopCategory>();


    private List<CosmeticSO> _allTopComseticsSO= new List<CosmeticSO>();
    private List<CosmeticSO> _allMiddleComseticsSO = new List<CosmeticSO>();
    private List<CosmeticSO> _allBottomComseticsSO = new List<CosmeticSO>();
    private List<GameObject> _allTopSpawnedComsetics = new List<GameObject>();
    private List<GameObject> _allMiddleSpawnedComsetics = new List<GameObject>();
    private List<GameObject> _allBottomSpawnedComsetics = new List<GameObject>();

    private int _currentTopCosmeticPrefabIndex;
    private int _currentMiddleCosmeticPrefabIndex;
    private int _currentBottomCosmeticPrefabIndex;
    // Start is called before the first frame update
    void Start()
    {
        _allTopComseticsSO.Add(CosmeticsSettings.SelectedCosmetics[0]);
        _allTopSpawnedComsetics.Add(Instantiate(CosmeticsSettings.SelectedCosmetics[0].Prefab, _playerShowcase));
        _allMiddleComseticsSO.Add(CosmeticsSettings.SelectedCosmetics[1]);
        _allMiddleSpawnedComsetics.Add(Instantiate(CosmeticsSettings.SelectedCosmetics[1].Prefab, _playerShowcase));
        _allBottomComseticsSO.Add(CosmeticsSettings.SelectedCosmetics[2]);
        _allBottomSpawnedComsetics.Add(Instantiate(CosmeticsSettings.SelectedCosmetics[2].Prefab, _playerShowcase));
        _currentTopCosmeticPrefabIndex=0;
        _currentMiddleCosmeticPrefabIndex = 0;
        _currentBottomCosmeticPrefabIndex = 0;
        _allTopSpawnedComsetics[_currentTopCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(GameDataManager.GameData.customizationData.cosmeticsData.Find((x) => x.cosmeticId == CosmeticsSettings.SelectedCosmetics[0].Id).colors);
        _allMiddleSpawnedComsetics[_currentMiddleCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(GameDataManager.GameData.customizationData.cosmeticsData.Find((x) => x.cosmeticId == CosmeticsSettings.SelectedCosmetics[1].Id).colors);
        _allBottomSpawnedComsetics[_currentBottomCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(GameDataManager.GameData.customizationData.cosmeticsData.Find((x) => x.cosmeticId == CosmeticsSettings.SelectedCosmetics[2].Id).colors);
    }

    // Used by UnityEvent in shop category script
    public void AddCosmeticToSelectedCosmeticList(CosmeticSO cosmetic, CosmeticCategory category)
    {
        switch (category)
        {
            case CosmeticCategory.TOP:
                {
                    _selectedTopCosmeticSO = cosmetic;
                    _cosmeticsSettings.SwapCosmetic(_selectedTopCosmeticSO, category);
                    if (!_allTopComseticsSO.Contains(cosmetic))
                    {
                        _allTopComseticsSO.Add(cosmetic);
                        _allTopSpawnedComsetics.Add(Instantiate(cosmetic.Prefab, _playerShowcase));
                    }
                    _allTopSpawnedComsetics[_currentTopCosmeticPrefabIndex].SetActive(false);
                    _currentTopCosmeticPrefabIndex = _allTopComseticsSO.IndexOf(cosmetic);
                    _allTopSpawnedComsetics[_currentTopCosmeticPrefabIndex].SetActive(true);
                    _allTopSpawnedComsetics[_currentTopCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(GameDataManager.GameData.customizationData.cosmeticsData.Find((x) => x.cosmeticId == cosmetic.Id).colors);
                    break;
                }
            case CosmeticCategory.MIDDLE:
                {
                    _selectedMiddleCosmeticSO = cosmetic;
                    _cosmeticsSettings.SwapCosmetic(_selectedMiddleCosmeticSO, category);
                    if (!_allMiddleComseticsSO.Contains(cosmetic))
                    {
                        _allMiddleComseticsSO.Add(cosmetic);
                        _allMiddleSpawnedComsetics.Add(Instantiate(cosmetic.Prefab, _playerShowcase));
                    }
                    _allMiddleSpawnedComsetics[_currentMiddleCosmeticPrefabIndex].SetActive(false);
                    _currentMiddleCosmeticPrefabIndex = _allMiddleComseticsSO.IndexOf(cosmetic);
                    _allMiddleSpawnedComsetics[_currentMiddleCosmeticPrefabIndex].SetActive(true);
                    _allMiddleSpawnedComsetics[_currentMiddleCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(GameDataManager.GameData.customizationData.cosmeticsData.Find((x) => x.cosmeticId == cosmetic.Id).colors);
                    break;
                }
            case CosmeticCategory.BOTTOM:
                {
                    _selectedBottomCosmeticSO = cosmetic;
                    _cosmeticsSettings.SwapCosmetic(_selectedBottomCosmeticSO, category);
                    if (!_allBottomComseticsSO.Contains(cosmetic))
                    {
                        _allBottomComseticsSO.Add(cosmetic);
                        _allBottomSpawnedComsetics.Add(Instantiate(cosmetic.Prefab, _playerShowcase));
                    }
                    _allBottomSpawnedComsetics[_currentBottomCosmeticPrefabIndex].SetActive(false);
                    _currentBottomCosmeticPrefabIndex = _allBottomComseticsSO.IndexOf(cosmetic);
                    _allBottomSpawnedComsetics[_currentBottomCosmeticPrefabIndex].SetActive(true);
                    _allBottomSpawnedComsetics[_currentBottomCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(GameDataManager.GameData.customizationData.cosmeticsData.Find((x) => x.cosmeticId == cosmetic.Id).colors);
                    break;
                }
        }
    }

    public void UpdateCosmeticColors(CosmeticSO cosmetic, CosmeticCategory category, List<Color> colors)
    {
        switch (category)
        {
            case CosmeticCategory.TOP:
                {
                    _allTopSpawnedComsetics[_currentTopCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(colors);
                    break;
                }
            case CosmeticCategory.MIDDLE:
                {
                    _allMiddleSpawnedComsetics[_currentMiddleCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(colors);
                    break;
                }
            case CosmeticCategory.BOTTOM:
                {
                    _allBottomSpawnedComsetics[_currentBottomCosmeticPrefabIndex].GetComponent<Cosmetic>().SetColors(colors);
                    break;
                }
        }
    }
}
