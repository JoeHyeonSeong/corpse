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

            isDestroyed = true;
            //파편 생성
            GameObject debris = (GameObject)Resources.Load<GameObject>("Prefab/InGameObject/BoxDebris");
            Instantiate(debris, currentPos.ToVector3(), Quaternion.identity);
            //우주로 보내기
            Move(new Position(100, 100), false);
    }
    public override bool Push(MovableObject who, Position dir)
    {
        if (MapManager.instance.Find(typeof(Hole),currentPos)==null)//hole위에 있지않을때
        {
            bool result= base.Push(who, dir);
            if(result) transform.Find("MoveSound").GetComponent<SoundEffectCtrl>().Play();
            return result;
        }
        return false;
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
