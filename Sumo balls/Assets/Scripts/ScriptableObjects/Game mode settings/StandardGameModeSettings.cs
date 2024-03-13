using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StandardGameModeSettings : GameModeSettings
{
    public List<EnemiesInStage> EnemiesInStageList => _enemiesInStage;
    public bool FallingBalls => _fallingBalls;
    public bool HolesInArena=>_holesInArena;
    public FallingBallsSettings FallingBallsSettings => _fallingBallsSettings;
    public HoleInArenaSettings HolesInArenaSettings => _holesInArenaSettings;
    [SerializeField] protected bool _fallingBalls;
    [SerializeField] protected bool _holesInArena;
    [SerializeField,HideInInspector] protected FallingBallsSettings _fallingBallsSettings;
    [SerializeField, HideInInspector] protected HoleInArenaSettings _holesInArenaSettings;
    [SerializeField] protected List<EnemiesInStage> _enemiesInStage = new List<EnemiesInStage>();
    private void OnValidate()
    {
        int beltCount = Enum.GetNames(typeof(EnemyBelts.Belt)).Length;
        if (_enemiesInStage.Count < beltCount)
        {
            for (int i = 0; i < beltCount; i++)
            {
                if (i >= _enemiesInStage.Count)
                {
                    EnemiesInStage en;
                    en.belt = (EnemyBelts.Belt)i;
                    en.numberOfEnemies = 0;
                    _enemiesInStage.Add(en);
                }
                else
                {
                    EnemiesInStage en = _enemiesInStage[i];
                    en.belt = (EnemyBelts.Belt)i;
                    _enemiesInStage[i] = en;
                }
            }
        }
    }
}
