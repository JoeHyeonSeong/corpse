﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipButton : Floor {

    protected override void Awake()
    {
        base.Awake();
        stepPriority = 1;
    }

    [SerializeField]
    protected List<InGameObject> operands;
    public List<InGameObject> Operands { set { operands = value; }}

    public override void Step(MovableObject who)
    {
        ButtonOn();
    }

    public override void Leave(MovableObject who)
    {
        Debug.Log("leave");
        ButtonOff();
    }

    private void ButtonOn()
    {
        foreach (InGameObject operand in operands)
        {
            if (operand != null)
            {
                operand.AddActiveStack();
            }
        }
    }

    private void ButtonOff()
    {
        foreach (InGameObject operand in operands)
        {

            operand.SubActiveStack();
        }
    }

    public override void ShowLinks()
    {
        ShowLinks(operands);
    }
}