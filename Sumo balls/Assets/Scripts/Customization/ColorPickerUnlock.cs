using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerUnlock : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] List<Unlockable> _colorItems;
    [SerializeField] ColorSelectionButton _pickerButton;
    [SerializeField] UnlockableItem _pickerItem;
    private bool _wasPickerCreated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        _wasPickerCreated = false;
        ColorPicker.Create(GameDataManager.GameData.customizationData.colorPickerColor, "", SetColorImage, null, false);
        //if (_pickerItem.//IsUnlocked) 
        if(GameDataManager.IsItemUnlocked(_pickerItem.Id))
        {
            ColorPicker.SetInteractable(true);
            //_pickerButton.CheckItem(false);
        }
        else
        {
            ColorPicker.SetInteractable(false);
            foreach (Unlockable item in _colorItems)
            {
                item.OnUnlockedEvent.AddListener(CheckIfAllColorsAreUnlocked);
            }
        }
        
        //SetColorImage(GameDataManager.GameData.customizationData.colorPickerColor);
    }
    private void CheckIfAllColorsAreUnlocked()
    {
        bool areAllUnlocked = true;
        foreach (Unlockable item in _colorItems)
        {
            //if (!item.UnlockableItem.IsUnlocked)
            if(!GameDataManager.IsItemUnlocked(item.UnlockableItem.Id))
            {
                areAllUnlocked = false;
                break;
            }
        }
        if (!areAllUnlocked) return;
        _pickerButton.TryUnlock();
    }
    private void SetColorImage(Color color)
    {
        if (!_wasPickerCreated)
        {
            _wasPickerCreated = true;
            return;
        }
        GameDataManager.GameData.customizationData.colorPickerColor=color;
        GameDataManager.Save();
        _image.color = color;
        _pickerButton.CheckItem(false);
    }
}
