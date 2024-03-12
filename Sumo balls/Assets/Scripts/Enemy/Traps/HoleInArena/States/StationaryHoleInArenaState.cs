using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryHoleInArenaState : HoleInArenaState
{
    public override void SetUpState(HoleInArenaContext context)
    {
        base.SetUpState(context);
        _context.timeCounter.ResetTimer();
    }
    public override void Update()
    {
        if (_context.timeCounter.CurrentTime <= _context.timeToStayAtMaxSize) return;
        ChangeState(typeof(ShrinkingHoleInArenaState));
    }
}
