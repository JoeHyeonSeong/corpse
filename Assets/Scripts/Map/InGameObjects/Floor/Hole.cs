using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : FlipButton{
    protected override void Awake()
    {
        base.Awake();
        stepPriority = 1;
    }


    public override void Step(MovableObject who)
    {
        if(who.GetType()==typeof(Box))
        {
            base.Step(who);
        }
    }


    public override void Leave(MovableObject who)
    {
        if (who.GetType() == typeof(Box))
        {
            base.Leave(who);
        }
    }
}
