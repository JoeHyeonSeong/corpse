using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenPoint :Floor {
    protected static GenPoint activatingGenPoint;
    public static GenPoint ActivatingGenPoint { get { return activatingGenPoint; } }
    public bool IsActivatedGenPoint { get { return this == activatingGenPoint; } }

    public override void Touched(MovableObject who)
    {
        activatingGenPoint.DeactivateGenPoint();
        ActivateGenPoint();
    }


    /// <summary>
    /// if this genpoint is activating, deactivate
    /// </summary>
    protected void DeactivateGenPoint()
    {
        if(IsActivatedGenPoint)
        {

        }
    }

    protected void ActivateGenPoint()
    {
        if(!IsActivatedGenPoint)
        {
            activatingGenPoint = this;
        }
    }
}
