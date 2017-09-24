using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : Floor {

    public override void Step(MovableObject who)
    {
        if (who.GetType() == typeof(Character))
        {
            InGameManager.instance.StageClear();
        }
    }
}
