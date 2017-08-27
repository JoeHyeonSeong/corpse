using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenPoint :Floor {
    protected static GenPoint activatingGenPoint;
    public static GenPoint ActivatingGenPoint { get { return activatingGenPoint; } }
    public bool IsActivatedGenPoint { get { return this == activatingGenPoint; } }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Step(MovableObject who)
    {
        if (activatingGenPoint != this)
        {
            activatingGenPoint.Deactivate();
            Activate();
        }
    }


    /// <summary>
    /// if this genpoint is activating, deactivate
    /// </summary>
    public override void Deactivate()
    {
        base.Deactivate();
        if(IsActivatedGenPoint)
        {

        }
    }

    public override void Activate()
    {
        base.Activate();
        if (!IsActivatedGenPoint)
        {
            activatingGenPoint = this;
        }
    }
}
