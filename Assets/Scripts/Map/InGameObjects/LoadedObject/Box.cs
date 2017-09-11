using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DestroyableObject {
    protected bool destroyCall;


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
        if (MapManager.instance.Find(typeof(Hole),currentPos)==null)
        {
            base.Push(who, dir);
        }
    }

}
