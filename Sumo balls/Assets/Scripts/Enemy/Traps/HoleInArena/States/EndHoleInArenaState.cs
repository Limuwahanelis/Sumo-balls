using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHoleInArenaState : HoleInArenaState
{
    public override void SetUpState(HoleInArenaContext context)
    {
        base.SetUpState(context);
        _context.EndHoleCycle();

    }
    public override void Update()
    {
        
    }
}
