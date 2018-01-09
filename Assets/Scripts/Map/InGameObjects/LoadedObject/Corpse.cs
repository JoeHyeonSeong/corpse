using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MovableObject {
    public override bool Push(MovableObject who, Position dir)
    {
        bool result= base.Push(who, dir);
        if(result) transform.Find("MoveSound").GetComponent<SoundEffectCtrl>().Play();
        return result;
    }
}
