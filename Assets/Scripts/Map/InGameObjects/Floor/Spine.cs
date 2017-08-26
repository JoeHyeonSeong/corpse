using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : Floor {
    public override void Touched(MovableObject who)
    {
        if(currentStatus==true)
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
