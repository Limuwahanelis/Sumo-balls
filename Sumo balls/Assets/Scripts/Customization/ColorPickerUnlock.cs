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
        if (_pickerItem.IsUnlocked) _pickerButton.CheckItem(true);
        else
        {

            foreach (Unlockable item in _colorItems)
            {
                item.OnUnlockedEvent.AddListener(CheckIfAllColorsAreUnlocked);
            }
        }
        ColorPicker.Create(SaveGameData.GameData.customizationData.colorPickerColor, "",SetColorImage,null,false);
        SetColorImage(SaveGameData.GameData.customizationData.colorPickerColor);
    }
    private void CheckIfAllColorsAreUnlocked()
    {
        bool areAllUnlocked = true;
        foreach (Unlockable item in _colorItems)
        {
            if (!item.UnlockableItem.IsUnlocked)
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
        SaveGameData.GameData.customizationData.colorPickerColor=color;
        SaveGameData.Save();
        _image.color = color;
        _pickerButton.CheckItem(false);
    }
}
