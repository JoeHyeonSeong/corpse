using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : Floor {

    protected override void Awake()
    {
        base.Awake();
    }


    public override void Step(MovableObject who)
    {
        if(currentStatus>0)
        {
            if(who.GetType()==typeof(DestroyableObject))
            ((DestroyableObject)who).Destroy();
        }
    }

   public override void Activate()
    {

    }

    public override void Deactivate()
    {

    }
}
