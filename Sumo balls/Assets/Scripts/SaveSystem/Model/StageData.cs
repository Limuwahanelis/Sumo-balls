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

        public StageData(bool completed, int score)
        {
            this.completed = completed;
            this.score = score;
        }
    }
}
