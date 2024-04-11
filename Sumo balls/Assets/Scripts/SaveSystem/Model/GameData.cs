using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class GameData
    {
        public List<StageData> stagesData;
        public bool isControlsTutorialCompleted;
        public bool isCombatTutorialCompleted;
        public CustomizationData customizationData;
        public GameData(List<StageData> stagesData,List<UnlockableItem> unlockableColors,List<CosmeticSO> cosmeticSOs)
        {
            this.stagesData = stagesData;
            customizationData = new CustomizationData(unlockableColors, cosmeticSOs);
        }
    }
}
