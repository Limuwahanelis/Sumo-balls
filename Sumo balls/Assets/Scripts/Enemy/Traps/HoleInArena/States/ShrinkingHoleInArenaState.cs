using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShrinkingHoleInArenaState : HoleInArenaState
{
    float newHoleRadius;
    public override void SetUpState(HoleInArenaContext context)
    {
        base.SetUpState(context);
        _context.timeCounter.ResetTimer();
        newHoleRadius = _context.holeMaxRadius;
    }
    public override void Update()
    {
         newHoleRadius = _context.holeMaxRadius- math.remap(0, _context.timeToGetMaxToSize, 0, _context.holeMaxRadius+0.01f, _context.timeCounter.CurrentTime);
        _context.SetHoleRadius(newHoleRadius);
        if (_context.timeCounter.CurrentTime > _context.timeToGetMaxToSize)
        {
            _context.SetColliders(false);
            ChangeState(typeof(EndHoleInArenaState));
        }

    }
}
