using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public struct EnemiesInStage 
{
    public EnemyBelts.Belt belt;
    [Tooltip("Represents minimum number of enemies spawned in a stage. If during gameplay all enemies have been used, new enemies will be spawned randomly. If value is set to " +
        "-1 enemies of this type will be spawned indefinitely")]
    public int numberOfEnemies;

}
