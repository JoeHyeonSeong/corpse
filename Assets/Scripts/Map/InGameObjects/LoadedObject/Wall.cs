using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : LoadedObject {
    /// <summary>
    /// do nothing
    /// </summary>
    /// <param name="who"></param>
    public override void Push(MovableObject who,Position dir)
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        isObstacle = true;
    }
}
