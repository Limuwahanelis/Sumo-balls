using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class StageData
    {
        public bool completed;
        public int score;
        public string stageID;
        public StageData(bool completed, int score, string stageID)
        {
            this.completed = completed;
            this.score = score;
            this.stageID = stageID;
        }
        public StageData(string stageID)
        {
            completed = false;
            score = 0;
            this.stageID = stageID;
        }
    }
}
