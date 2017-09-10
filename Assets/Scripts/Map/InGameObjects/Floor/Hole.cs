using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : DisposableButton{

    public override void Step(MovableObject who)
    {
        if(who.GetType()==typeof(Ball))
        {
            base.Step(who);
            ((Ball)who).Hide();
        }
    }
}
