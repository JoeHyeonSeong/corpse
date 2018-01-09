using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : LoadedObject {
    /// <summary>
    /// do nothing
    /// </summary>
    /// <param name="who"></param>
    public override bool Push(MovableObject who,Position dir)
    {
        return false;
    }

    protected override void Awake()
    {
        base.Awake();
        isObstacle = true;
    }
}
