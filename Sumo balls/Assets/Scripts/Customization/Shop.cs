using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] List<Unlockable> _unlockables = new List<Unlockable>();
    [SerializeField] IntReference _points;
    private void OnEnable()
    {
        foreach (Unlockable item in _unlockables)
        {
            if(!item.UnlockableItem.IsUnlocked)
            {
                item.OnUnlockedEvent.AddListener(() => BuyItem(item.UnlockableItem));
            }
            
        }
    }

    private void BuyItem(UnlockableItem item)
    {
        _points.value -= item.Cost;
    }
}
