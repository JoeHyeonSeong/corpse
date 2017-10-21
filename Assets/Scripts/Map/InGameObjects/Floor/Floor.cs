using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Floor : InGameObject, IComparable<Floor>
{
    protected int stepPriority;

    protected override void Awake()
    {
        base.Awake();
        stepPriority = 0;
    }
    /// <summary>
    /// if character touch this floor do this func(do nothing)
    /// </summary>
    public virtual void Step(MovableObject who)
    {

    }

    public virtual void Leave(MovableObject who)
    {

    }

    public int CompareTo(Floor com)
    {
        if (com == null) return 0;
        if (stepPriority > com.stepPriority)
        {
            return -1;
        }
        else if (stepPriority < com.stepPriority)
        {
            return 1;
        }
        else return 0;
    }
}
