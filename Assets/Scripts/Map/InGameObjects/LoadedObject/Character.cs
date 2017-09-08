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
        //시체 방향
        mycorpse.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX =
            transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX;
        //gen point 에서 다시 살아남
        Position lastPos = currentPos;
        Ice underIce = (Ice)MapManager.instance.Find(typeof(Ice), currentPos);
        Teleport(GenPoint.ActivatingGenPoint.CurrentPos);
        if (isMoving && underIce != null)
        {
            StopCoroutine(MoveCoroutine());
            mycorpse.GetComponent<Corpse>().Slide(lastPos + moveDir,true);
        }

        
    }

    public override void Move(Position destination,  bool anim)
    {
        InGameManager.instance.NewPhase();
        base.Move(destination,  anim);
        bool flipX= transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX;
        if (moveDir == new Position(1, 0))
        {
            flipX = true;
        }
        else if (moveDir == new Position(-1, 0))
        {
            flipX = false;
        }
        transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = flipX;
    }
}

