using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenPoint :Floor {
    protected static GenPoint activatingGenPoint;
    public static GenPoint ActivatingGenPoint { get { return activatingGenPoint; } }

    protected override void Awake()
    {
        base.Awake();
        stepPriority = 0;
    }


    public override void Step(MovableObject who)
    {
        TurnOn();
    }


    protected override void Activate()
    {
        base.Activate();
        TurnOn();
    }

    protected override void Deactivate()
    {
        if (activatingGenPoint != this)
        {
            base.Deactivate();
            transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    protected void TurnOn()
    {
        if (activatingGenPoint != this)
        {
            GenPoint preActivate = activatingGenPoint;
            activatingGenPoint = this;
            if (preActivate != null)
            {
                preActivate.Deactivate();
            }
            transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
