using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GrowingHoleInArenaState : HoleInArenaState
{
    public GrowingHoleInArenaState() : base()
    {
    }
    public override void SetUpState(HoleInArenaContext context)
    {
        base.SetUpState(context);
        _context.timeCounter.ResetTimer();
        _context.SetColliders(true);
    }
    public override void Update()
    {
        float newHoleRadius = math.remap(0, _context.timeToGetMaxToSize, 0.01f, _context.holeMaxRadius, _context.timeCounter.CurrentTime);
        _context.SetHoleRadius(newHoleRadius);
        if (_context.timeCounter.CurrentTime > _context.timeToGetMaxToSize) ChangeState(typeof(StationaryHoleInArenaState));
    }
}
