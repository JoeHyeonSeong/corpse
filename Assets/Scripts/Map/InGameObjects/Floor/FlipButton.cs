﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipButton : Floor {
    protected override void Awake()
    {
        base.Awake();
    }
    [SerializeField]
    protected List<InGameObject> operands;

    public override void Step(MovableObject who)
    {
        ButtonOn();
    }

    public override void Leave(MovableObject who)
    {
        ButtonOff();
    }

    private void ButtonOn()
    {
        foreach (InGameObject operand in operands)
        {

            operand.AddStack();
        }
    }

    private void ButtonOff()
    {
        foreach (InGameObject operand in operands)
        {

            operand.SubStack();
        }
    }
}