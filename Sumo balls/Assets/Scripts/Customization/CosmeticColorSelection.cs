using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CosmeticColorSelection : MonoBehaviour
{
    [SerializeField] GameObject _colorWindow;
    [SerializeField] Transform _partsPanel;
    [SerializeField] GameObject _colorPicker;
    [SerializeField] Transform _colorPickerPos;
    [SerializeField] GameObject _colorPickerBlockScreen;
    [SerializeField] GameObject _partColorTogglePrefab;
    [SerializeField] CosmeticsShop _cosmeticsShop;
    [SerializeField] ToggleGroup _toggleGroup;
    private CosmeticSO _cosmeticSO;
    private CosmeticShopCategory.CosmeticCategory _cosmeticCategory;
    private bool _wasPickerCreated = false;
    private int _selectedPartIndex = 0;
    CosmeticData _cosmeticData;
    List<CosmeticPartColorToggle> _partColorToggles=new List<CosmeticPartColorToggle>();
    public void OpenColorWindow(CosmeticSO cosmetic,CosmeticShopCategory.CosmeticCategory cosmeticCategory)
    {
        _cosmeticSO = cosmetic;
        _cosmeticCategory = cosmeticCategory;
        _colorWindow.gameObject.SetActive(true);
        _colorPicker.transform.SetParent(_colorWindow.transform, false);
        _colorPicker.transform.position=_colorPickerPos.position;
        _cosmeticData = GameDataManager.GameData.customizationData.cosmeticsData.Find((x) => x.cosmeticId == cosmetic.Id);
        List<Color> colors = _cosmeticData.colors;

        for(int i=0;i<colors.Count; i++)
        {
            CosmeticPartColorToggle tag = Instantiate(_partColorTogglePrefab, _partsPanel).GetComponent<CosmeticPartColorToggle>();
            tag.SetUp(i, cosmetic.PartsNames[i]);
            tag.GetComponent<Toggle>().group = _toggleGroup;
            tag.OnPartSelected.AddListener(ChangeCosmeticPart);
            _partColorToggles.Add(tag);
        }
        _partColorToggles[0].GetComponent<Toggle>().isOn = true;
    }
    public void CloseColorWindw()
    {
        _colorWindow.SetActive(false);
    }
    private void ChangeCosmeticPart(int value)
    {
        ColorPicker.Done();
        _selectedPartIndex= value;
        ColorPicker.Create(_cosmeticData.colors[value], "", ChangeColor, null, false);
        ColorPicker.SetInteractable(true);
        _colorPickerBlockScreen.SetActive(false);
        _colorWindow.SetActive(true);
    }
    private void ChangeColor(Color color)
    {
        if (!_wasPickerCreated)
        {
            _wasPickerCreated = true;
            return;
        }
        _cosmeticData.colors[_selectedPartIndex] = color;
        GameDataManager.Save();
        _cosmeticsShop.UpdateCosmeticColors(_cosmeticSO, _cosmeticCategory, _cosmeticData.colors);
    }
    private void OnDisable()
    {
        for (int i = _partColorToggles.Count-1; i >= 0; i--)
        {
            _partColorToggles[i].OnPartSelected.RemoveListener(ChangeCosmeticPart);
            Destroy(_partColorToggles[i].gameObject);
            _partColorToggles.RemoveAt(i);
        }
        ColorPicker.SetInteractable(false);
        _colorPickerBlockScreen.SetActive(true);
        ColorPicker.Done();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
