using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : DestroyableObject
{
    public static Character instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
    }

    public override void Destroy()
    {
        Debug.Log("주것따");
        //시체 남김
        Instantiate(Resources.Load("Prefab/InGameObject/Corpse"), currentPos.ToVector3(), Quaternion.identity, transform.parent);
        //gen point 에서 다시 살아남
        Teleport(GenPoint.ActivatingGenPoint.CurrentPos);
    }
}
