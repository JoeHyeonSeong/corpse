using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : InGameObject {
    public override void Activate()
    {
        base.Activate();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SignLine>().Activate();
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SignLine>().Deactivate();
        }
    }
}
