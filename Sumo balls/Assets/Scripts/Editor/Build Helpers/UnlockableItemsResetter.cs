using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class UnlockableItemsResetter : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        List<UnlockableItem> unlockedItems = new List<UnlockableItem>();
        unlockedItems = Resources.LoadAll<UnlockableItem>("Unlockables").ToList();
        int unlockingItemsNumber=0;
        int lockingItemsNumber=0;
        foreach (UnlockableItem item in unlockedItems)
        {
            if (item.StartUnlocked && !item.IsUnlocked)
            {
                item.Unlock();
                unlockingItemsNumber++;
            }
            else if (!item.StartUnlocked && item.IsUnlocked)
            {
                item.Lock();
                lockingItemsNumber++;
            }
        }
        Debug.Log($"locked {lockingItemsNumber} items and unlocked {unlockingItemsNumber} items");
    }
}
