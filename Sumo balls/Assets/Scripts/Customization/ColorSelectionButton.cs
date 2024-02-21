using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorSelectionButton : MonoBehaviour,IColorPickable
{
    [SerializeField] protected Image _colorImage;
    [SerializeField] protected GameObject _tick;
    protected Unlockable _unlockable;

    public event IColorPickable.ColorPickedEventHandler OnColorPicked;

    private void Awake()
    {
        if(_unlockable == null) _unlockable = GetComponent<Unlockable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color GetColor()
    {
        return _colorImage.color;
    }
    public void SetSelectionTick(bool value)
    {
        _tick.SetActive(value);
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
    public void TryUnlock()
    {
        _unlockable.TryUnlock();
    }
}
