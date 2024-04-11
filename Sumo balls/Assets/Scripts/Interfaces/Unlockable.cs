using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unlockable:MonoBehaviour
{

    public UnlockableItem UnlockableItem => _unlock;
    [SerializeField] UnlockableItem _unlock;
    //[SerializeField] IntValue _points;
    public UnityEvent OnUnlockedEvent;
    private void OnEnable()
    {
        //if(_unlock.IsUnlocked)
        if(GameDataManager.IsItemUnlocked(_unlock.Id))
        {
            Debug.Log("unlock");
            OnUnlockedEvent?.Invoke();
        }
    }
    public void SetUnlockable(UnlockableItem item)
    {
        _unlock = item;
    }    
    public bool TryUnlock()
    {
        if(GameDataManager.Points>=_unlock.Cost)
        {
            GameDataManager.IncreasePoints(-_unlock.Cost);
            GameDataManager.UpdateCustomizationData(_unlock.Id, true);
            OnUnlockedEvent?.Invoke();
            return true;
        }
        return false;
    }
}
