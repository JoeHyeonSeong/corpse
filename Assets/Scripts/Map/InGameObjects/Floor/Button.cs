using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Floor {
    protected override void Awake()
    {
        base.Awake();
    }
    [SerializeField]
    protected List<InGameObject> operands;

    public override void Step(MovableObject who)
    {
        FlipStatus();
    }

    public override void Leave(MovableObject who)
    {
        FlipStatus();
    }

    private void FlipStatus()
    {
        foreach (InGameObject operand in operands)
        {
            if (operand.CurrentStatus == ActiveStatus.activating)
            {
                operand.Deactivate();
            }
            else if (operand.CurrentStatus == ActiveStatus.deactivating)
            {
                operand.Activate();
            }
        }
    }
}
