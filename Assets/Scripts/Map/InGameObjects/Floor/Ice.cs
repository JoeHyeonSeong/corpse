using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Floor
{
    protected override void Awake()
    {
        base.Awake();
        stepPriority = 2;
    }


    public override void Step(MovableObject who)
    {
        who.Slide(who.CurrentPos + who.MoveDir,false);
    }
}
