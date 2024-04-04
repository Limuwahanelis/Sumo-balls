using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsPreview : MonoBehaviour
{
    [SerializeField] GameObject _playerBody;
    private List<Cosmetic> _spawnedCosmetics;
    private List<CosmeticSO> _usedCosmetics;
    private int _lastChangedCosmeticIndex;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void RefreshCosmetics()
    {
        foreach (CosmeticSO cosmetic in CosmeticsSettings.SelectedCosmetics)
        {
            if (_usedCosmetics.Contains(cosmetic))
            {
                int index = _usedCosmetics.IndexOf(cosmetic);
                _spawnedCosmetics[_lastChangedCosmeticIndex].gameObject.SetActive(false);
                _spawnedCosmetics[index].gameObject.SetActive(true);
                _lastChangedCosmeticIndex = index;
                break;
            }
            Cosmetic cos = Instantiate(cosmetic.Prefab, _playerBody.transform).GetComponent<Cosmetic>();
            cos.SpawnCosmeticLocally();
            _usedCosmetics.Add(cosmetic);
            _spawnedCosmetics.Add(cos);
            _lastChangedCosmeticIndex = _spawnedCosmetics.Count-1;
        }
    }
}
