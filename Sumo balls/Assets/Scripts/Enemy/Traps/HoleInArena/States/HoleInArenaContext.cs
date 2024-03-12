using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using static HoleInArenaState;

public class HoleInArenaContext
{
    public TimeCounter timeCounter;
    public Action<HoleInArenaState> ChangeHoleState;
    public Action<float> SetHoleRadius;
    public Action<bool> SetColliders;
    public Action EndHoleCycle;
    public GetState GetStateType;
    public float holeMaxRadius;
    public float timeToGetMaxToSize;
    public float timeToStayAtMaxSize;
    public float timeToBeginGrow;
}
