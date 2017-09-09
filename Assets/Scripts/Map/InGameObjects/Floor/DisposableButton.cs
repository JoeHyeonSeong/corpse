using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposableButton : FlipButton {
    Stack<bool> neverUseStack = new Stack<bool>();
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

    public override void SaveHistory()
    {
        base.SaveHistory();
        neverUseStack.Push(neverUsed);
    }

    public override void RollBack()
    {
        base.RollBack();
        if (neverUseStack.Count > 0)
        {
            neverUsed = neverUseStack.Pop();
        }
    }
}
