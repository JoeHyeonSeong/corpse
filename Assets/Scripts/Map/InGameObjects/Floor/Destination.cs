using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : Floor{

    public override void Touched(MovableObject who)
    {
        if(who.GetType()==typeof(Ball))
        {
            //game clear
        }
    }
}
