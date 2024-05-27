using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CosmeticColorSelection : MonoBehaviour
{
    [SerializeField] GameObject _partColorTogglePrefab;
    [SerializeField] GameObject _colorWindow;
    [SerializeField] Transform _partsPanel;
    [SerializeField] CosmeticsShop _cosmeticsShop;
    [SerializeField] ToggleGroup _toggleGroup;
    

    [Header("Color picker")]
    [SerializeField] GameObject _colorPicker;
    [SerializeField] Transform _colorPickerPos;
    [SerializeField] GameObject _colorPickerBlockScreen;
    [SerializeField] Slider _colorPickerUpperSlider;
    [SerializeField] Slider _colorPickerMainSlider;
    [SerializeField] Image _colorPickerTick;


    [Header("Tabs")]
    [SerializeField] TabToggleUI _cosmeticsTabToggle;
    [SerializeField] TabToggleUI _ballColorsTabToggle;

    [Header("Buttons")]
    [SerializeField] Selectable _closeButton;
    [SerializeField] Selectable _resetColorButton;

    private CosmeticSO _cosmeticSO;
    private CosmeticShopCategory.CosmeticCategory _cosmeticCategory;
    private bool _wasPickerCreated = false;
    private NavigationSetter _colorPickerMainSliderNavSetter;
    private int _selectedPartIndex = 0;
    private Selectable _caller;
    CosmeticData _cosmeticData;
    List<CosmeticPartColorToggle> _partColorToggles=new List<CosmeticPartColorToggle>();
    public void OpenColorWindow(CosmeticSO cosmetic,CosmeticShopCategory.CosmeticCategory cosmeticCategory, Selectable caller)
    {
        _caller= caller;
        _cosmeticSO = cosmetic;
        _cosmeticCategory = cosmeticCategory;
        _colorPickerMainSliderNavSetter=_colorPickerMainSlider.GetComponent<NavigationSetter>();
        _colorWindow.gameObject.SetActive(true);
        _colorPicker.transform.SetParent(_colorWindow.transform, false);
        _colorPicker.transform.position=_colorPickerPos.position;
        _colorPickerTick.enabled = false;
        _colorPicker.GetComponent<Button>().enabled = true;
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
        AssignNaviagation();
        _partColorToggles[0].GetComponent<Toggle>().isOn = true;
    }
    private void AssignNaviagation()
    {
        _cosmeticsTabToggle.SetSelectableOnDown(_colorPickerUpperSlider);
        _ballColorsTabToggle.SetNavigationMode(true);
        _ballColorsTabToggle.SetSelectableOnLeft(_cosmeticsTabToggle);
        _ballColorsTabToggle.SetSelectableOnRight(_cosmeticsTabToggle);
        _ballColorsTabToggle.SetSelectableOnDown(_partColorToggles[0].GetComponent<Selectable>());
        _colorPickerMainSliderNavSetter.SetSelectableOnLeft(_partColorToggles[_partColorToggles.Count-1].GetComponent<Selectable>());
        _colorPicker.GetComponent<NavigationSetter>().SetSelectableOnDown(_closeButton);
        for (int i=0;i<_partColorToggles.Count;i++)
        {
            Navigation navigation = new Navigation()
            {
                mode = Navigation.Mode.Explicit,
            };
            if (i==0)navigation.selectOnUp =_ballColorsTabToggle;
            else navigation.selectOnUp = _partColorToggles[i - 1].GetComponent<Toggle>();
            if (i == _partColorToggles.Count - 1) navigation.selectOnDown = _resetColorButton;
            else navigation.selectOnDown = _partColorToggles[i+1].GetComponent<Toggle>();
            navigation.selectOnRight = _colorPickerMainSlider;
            _partColorToggles[i].GetComponent<Toggle>().navigation = navigation;
        }
        EventSystem.current.SetSelectedGameObject(_partColorToggles[0].gameObject);
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
    public void ResetPartColor()
    {
        _cosmeticData.colors[_selectedPartIndex] = _cosmeticSO.Colors[_selectedPartIndex];
        ColorPicker.Done();
        ColorPicker.Create(_cosmeticData.colors[_selectedPartIndex], "", ChangeColor, null, false);
        GameDataManager.Save();
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
        _colorPickerMainSliderNavSetter.ResetNavigation();
        _cosmeticsTabToggle.ResetNavigation();
        _ballColorsTabToggle.ResetNavigation();
        _colorPicker.GetComponent<NavigationSetter>().ResetNavigation();
        _colorPickerTick.enabled = true;
        _colorPicker.GetComponent<Button>().enabled = false;
        EventSystem.current.SetSelectedGameObject(_caller.gameObject);
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
