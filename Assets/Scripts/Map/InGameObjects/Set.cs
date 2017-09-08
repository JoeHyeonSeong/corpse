using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : InGameObject {
    protected override void Activate()
    {
        base.Activate();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SignLine>().AddStack();
        }
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SignLine>().SubStack();
        }
    }
}
