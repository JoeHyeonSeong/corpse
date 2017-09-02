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
        //시체 남김
        GameObject mycorpse =
       (GameObject) Instantiate(Resources.Load("Prefab/InGameObject/Corpse"),
        currentPos.ToVector3(), Quaternion.identity, transform.parent);
        //gen point 에서 다시 살아남
        Position lastPos = currentPos;
        Ice underIce = (Ice)MapManager.instance.Find(typeof(Ice), currentPos);
        Teleport(GenPoint.ActivatingGenPoint.CurrentPos);
        if (isMoving && underIce != null)
        {
            StopCoroutine(MoveCoroutine(true));
            mycorpse.GetComponent<Corpse>().Slide(lastPos + moveDir,true);
        }

        
    }

    public override void Move(Position destination, bool saveHistory, bool anim)
    {
        InGameManager.instance.NewPhase();
        base.Move(destination, saveHistory, anim);
    }
}
