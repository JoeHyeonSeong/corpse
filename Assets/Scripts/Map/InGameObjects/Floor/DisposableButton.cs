using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposableButton : Button {
    bool neverUsed = true;
    public override void Step(MovableObject who)
    {
        if (neverUsed)
        {
            neverUsed = false;
            base.Step(who);
        }
    }
    /// <summary>
    /// do nothing
    /// </summary>
    /// <param name="who"></param>
    public override void Leave(MovableObject who)
    {
    }
}
