using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DestroyableObject {
    private bool pushable;
    public bool Pushable { get { return pushable; } set { pushable = value; } }
    protected bool destroyCall;

    protected override void Awake()
    {
        base.Awake();
        pushable = true;
    }
    /// <summary>
    /// destroy this box
    /// </summary>
    /// 
    public override void Destroy()
    {
        //우주로 보내기
        Move(new Position(100,100),false);
        Debug.Log("Destroy");
    }
    public override void Push(MovableObject who, Position dir)
    {
        if (pushable)
        {
            base.Push(who, dir);
        }
    }

}
