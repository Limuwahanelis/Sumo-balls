using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormantHoleInArenaState : HoleInArenaState
{
    public override void SetUpState(HoleInArenaContext context)
    {
        base.SetUpState(context);
        _context.timeCounter.ResetTimer();
        _context.timeCounter.SetCountTime(true);
        _context.SetColliders(false);
        _context.SetShadow(true);
    }
    public override void Update()
    {
        if (_context.timeCounter.CurrentTime >= _context.timeToBeginGrow) ChangeState(typeof(GrowingHoleInArenaState));
    }
}
