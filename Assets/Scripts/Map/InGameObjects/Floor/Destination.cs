using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : Floor {
    protected override void Awake()
    {
        base.Awake();
        stepPriority = 1;
    }

    public override void Step(MovableObject who)
    {
        if (who.GetType() == typeof(Character))
        {
            InGameManager.instance.StageClear();
        }
    }
}
