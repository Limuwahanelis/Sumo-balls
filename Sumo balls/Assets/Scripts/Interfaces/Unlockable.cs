using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unlockable:MonoBehaviour
{

    public UnlockableItem UnlockableItem => _unlock;
    [SerializeField] UnlockableItem _unlock;
    [SerializeField] IntValue _points;
    public UnityEvent OnUnlockedEvent;
    private void OnEnable()
    {
        if(_unlock.IsUnlocked)
        {
            Debug.Log("unlock");
            OnUnlockedEvent?.Invoke();
        }
    }

    public bool TryUnlock()
    {
        if(_points.value>=_unlock.Cost)
        {
            _points.value -= _unlock.Cost;
            _unlock.Unlock();
            SaveGameData.UpdateCustomizationData(_unlock.Id, true);
            OnUnlockedEvent?.Invoke();
            return true;
        }
        return false;
    }
}
