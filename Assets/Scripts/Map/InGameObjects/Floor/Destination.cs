using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : Floor{
    protected override void Awake()
    {
        base.Awake();
    }


    public override void Step(MovableObject who)
    {
        if(who.GetType()==typeof(Ball))
        {
            Debug.Log("game clear");
        }
    }
}
