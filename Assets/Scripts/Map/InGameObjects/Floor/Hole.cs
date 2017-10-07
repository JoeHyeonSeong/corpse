using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : FlipButton{

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
