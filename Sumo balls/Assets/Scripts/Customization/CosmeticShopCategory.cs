using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CosmeticShopCategory : MonoBehaviour
{
    public enum CosmeticCategory
    {
        TOP,MIDDLE,LOWER
    }
    [SerializeField] CosmeticCategory category;
    [SerializeField] GameObject _gameObjectWithButtons;
    [SerializeField] List<CosmeticSelectionButton> _cosmeticButtons = new List<CosmeticSelectionButton>();
    public UnityEvent<CosmeticSO, CosmeticCategory> OnItemSelected;
    // Start is called before the first frame update
    void Start()
    {
        _cosmeticButtons = _gameObjectWithButtons.GetComponentsInChildren<CosmeticSelectionButton>().ToList();
        foreach (CosmeticSelectionButton button in _cosmeticButtons)
        {
            button.OnCosmeticPicked+=SelectItem;
        }
    }

    private void SelectItem(CosmeticSO cosmetic)
    {
        OnItemSelected?.Invoke(cosmetic, category);
    }
    private void OnDestroy()
    {
        foreach (CosmeticSelectionButton button in _cosmeticButtons)
        {
            button.OnCosmeticPicked -= SelectItem;
        }
    }
}
