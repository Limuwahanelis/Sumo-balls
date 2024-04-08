using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItemSelectionButton : MonoBehaviour
{
    [SerializeField] protected GameObject _selectedTick;
    protected Unlockable _unlockable;

    private void Awake()
    {
        
    }
    public void SetSelectionTick(bool value)
    {
        _selectedTick.SetActive(value);
    }
    /// <summary>
    /// Checks if item is unlocked if not then tries to unlock it if tryUnlock is set to true.
    /// </summary>
    /// <param name="tryUnlock"></param>
    public abstract void CheckItem(bool tryUnlock = true);
    public void TryUnlock()
    {
        _unlockable.TryUnlock();
    }
    private void OnValidate()
    {
        if (_unlockable == null) _unlockable = GetComponent<Unlockable>();
    }
}
