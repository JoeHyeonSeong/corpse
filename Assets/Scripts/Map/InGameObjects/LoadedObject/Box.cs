using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DestroyableObject {
    protected bool destroyCall;
    /// <summary>
    /// destroy this box
    /// </summary>
    public override void Destroy()
    {
        //우주로 보내기
        HistoryManager.instance.SaveMove(this, currentPos, false);
        Teleport(new Position(100,100));
        Debug.Log("Destroy");
    }


}
