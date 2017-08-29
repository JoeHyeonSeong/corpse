using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Floor
{
    protected override void Awake()
    {
        base.Awake();
    }


    public override void Step(MovableObject who)
    {
        who.Move(who.CurrentPos + who.MoveDir,false);

    }
}
