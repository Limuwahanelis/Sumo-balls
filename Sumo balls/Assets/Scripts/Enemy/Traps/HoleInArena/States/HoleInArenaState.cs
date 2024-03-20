using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public abstract class HoleInArenaState
{
    protected HoleInArenaContext _context;
    public delegate HoleInArenaState GetState(Type state);
    public HoleInArenaState() { }
    public abstract void Update();
    public virtual void SetUpState(HoleInArenaContext context)
    {
        _context = context;
    }
    public void ChangeState(Type stateType)
    {
        HoleInArenaState state = _context.GetStateType(stateType);
        _context.ChangeHoleState(state);
        state.SetUpState(_context);
    }
}
