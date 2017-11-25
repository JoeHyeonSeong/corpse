using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposableButton : FlipButton {
    Stack<bool> neverUseStack = new Stack<bool>();
    bool neverUsed = true;
    Sprite OnImage;
    Sprite OffImage;

    protected override void Awake()
    {
        base.Awake();
        stepPriority = 1;
        OnImage = Resources.Load<Sprite>("Graphic/InGameObject/doorButton_horizontal_on");
        OffImage = Resources.Load<Sprite>("Graphic/InGameObject/doorButton_horizontal2");

    }


    public override void Step(MovableObject who)
    {
        if (neverUsed)
        {
            mygraphic.GetComponent<SpriteRenderer>().sprite = OffImage;
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
        if (neverUseStack.Count > 0)
        {
            bool currentNeverused = neverUsed;
            neverUsed = neverUseStack.Pop();
            if (currentNeverused == false && neverUsed == true)
            {
                mygraphic.GetComponent<SpriteRenderer>().sprite = OnImage;
            }
        }
        base.RollBack();
    }
}
