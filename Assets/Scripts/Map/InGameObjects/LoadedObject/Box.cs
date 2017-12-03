using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DestroyableObject {
    protected bool destroyCall;
    Sprite onSprite;
    Sprite offSprite;
    protected override void Awake()
    {
        base.Awake();
        onSprite = Resources.Load<Sprite>("Graphic/InGameObject/box_on");
        offSprite = Resources.Load<Sprite>("Graphic/InGameObject/box");
    }
    /// <summary>
    /// destroy this box
    /// </summary>
    /// 
    public override void Destroy()
    {
        //우주로 보내기
        Move(new Position(100,100),false);
    }
    public override void Push(MovableObject who, Position dir)
    {
        if (MapManager.instance.Find(typeof(Hole),currentPos)==null)//hole위에 있지않을때
        {
            base.Push(who, dir);
        }
    }

    public void SetColor(bool on)
    {
        if (on)
        {
            mygraphic.GetComponent<SpriteRenderer>().sprite = onSprite;
        }
        else
        {
            mygraphic.GetComponent<SpriteRenderer>().sprite = offSprite;
        }
        }

    public override void RollBack()
    {
        base.RollBack();
        if (MapManager.instance.Find(typeof(Hole), currentPos) == null)
        {
            SetColor(false);
        }
    }
}
