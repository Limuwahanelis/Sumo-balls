using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class GameData
    {
        public List<StageData> stagesData;
        public bool isTutorialCompleted;
        public GameData(List<StageData> stagesData)
        {
            this.stagesData = stagesData;
        }
    }
}