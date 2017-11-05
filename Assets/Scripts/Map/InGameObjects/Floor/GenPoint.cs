using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenPoint :Floor {
    protected static GenPoint activatingGenPoint;
    private static GenPoint deactiv;
    public static GenPoint ActivatingGenPoint { get { return activatingGenPoint; } }

    Stack<GenPoint> activatedGenPoints = new Stack<GenPoint>();
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
            TurnOff();
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

    protected void TurnOff()
    {
        if (deactiv != this)
        {
            GenPoint preDeactiv = deactiv;
            deactiv = this;
            if (preDeactiv != null)
            {
                preDeactiv.Activate();
            }
            transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
    public override void RollBack()
    {
        if (activatedGenPoints.Count > 0)
        {
            GenPoint acGenPoint = activatedGenPoints.Pop();
            if (this == acGenPoint)
            {
                Activate();
            }
        }
        base.RollBack();
        
    }

    public override void SaveHistory()
    {
        base.SaveHistory();
        activatedGenPoints.Push(activatingGenPoint);
    }
    
}
