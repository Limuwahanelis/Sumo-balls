using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorSelectionButton :MonoBehaviour, IShopItemSelectable, IColorPickable
{
    [SerializeField] protected GameObject _selectedTick;
    protected Unlockable _unlockable;
    [SerializeField] protected Image _colorImage;

    public event IColorPickable.ColorPickedEventHandler OnColorPicked;

    private void Awake()
    {
        if(_unlockable == null) _unlockable = GetComponent<Unlockable>();
    }
    /// <summary>
    /// Checks if item is unlocked and if it is fires OnColorPicked event. Otherwise tries to unlock it if tryUnlock is set to true.
    /// </summary>
    /// <param name="tryUnlock"></param>
    public void CheckItem(bool tryUnlock=true)
    {
        if(GameDataManager.IsItemUnlocked(_unlockable.UnlockableItem.Id))
        {
            OnColorPicked?.Invoke(_colorImage.color,this);
        }
        else if(tryUnlock)
        {
            _unlockable.TryUnlock();
        }
        
    }

    public void SetSelectionTick(bool value)
    {
        _selectedTick.SetActive(value);
    }

    public void TryUnlock()
    {
        _unlockable.TryUnlock();
    }
}
