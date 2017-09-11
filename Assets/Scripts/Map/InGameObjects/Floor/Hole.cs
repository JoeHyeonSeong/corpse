using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : DisposableButton{

    public override void Step(MovableObject who)
    {
        if(who.GetType()==typeof(Box))
        {
            base.Step(who);
            ((Box)who).Pushable = false;
        }
    }
}
