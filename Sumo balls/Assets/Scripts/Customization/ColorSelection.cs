using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    [SerializeField] List<ColorSelectionButton> _colorButtons = new List<ColorSelectionButton>();
    [SerializeField] Material _playerMat;
    private ColorSelectionButton _currentlySelectedColorButton;
    private void OnEnable()
    {
        _currentlySelectedColorButton = _colorButtons.Find(x => x.GetComponent<Unlockable>().UnlockableItem.Id == GameDataManager.GameData.customizationData.usedColorUnlockId);
        _currentlySelectedColorButton.SetSelectionTick(true);
        for (int i = 0; i < _colorButtons.Count; i++)
        {
            (_colorButtons[i] as IColorPickable).OnColorPicked += SelectColor;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MarkSavedButton()
    {

    }
    private void SelectColor(Color color, IColorPickable caller)
    {
        _currentlySelectedColorButton.SetSelectionTick(false);
        _playerMat.color = color;
        _currentlySelectedColorButton = _colorButtons.Find(x => (x as IColorPickable) == caller);
        GameDataManager.GameData.customizationData.usedColorUnlockId = _currentlySelectedColorButton.GetComponent<Unlockable>().UnlockableItem.Id;
        GameDataManager.GameData.customizationData.playerColor = color;
        GameDataManager.Save();
        _colorButtons.Find(x => (x as IColorPickable) == caller).SetSelectionTick(true);

    }
    private void OnDisable()
    {
        ColorPicker.Done();
        for (int i = 0; i < _colorButtons.Count; i++)
        {
            _colorButtons[i].OnColorPicked -= SelectColor;
        }
    }
}
