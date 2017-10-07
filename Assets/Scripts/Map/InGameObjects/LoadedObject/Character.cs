using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : DestroyableObject
{

    private int life;
    private Stack<int> lifeStack = new Stack<int>();

    public static Character instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        if (InGameManager.IsInGameScene())
        {
            life = StageInfo.instance.Life;
            SetLifeText();
        }
    }



    public override void Destroy()
    {
        life--;
        SetLifeText();
        if (life > 0)
        {
            Revive();
        }
        else
        {
            InGameManager.instance.GameOver();
        }
        
    }

    private void Revive()
    {
        //시체 남김
        GameObject mycorpse =
       (GameObject)Instantiate(Resources.Load("Prefab/InGameObject/Corpse"),
        currentPos.ToVector3(), Quaternion.identity, transform.parent);
        //시체 방향
        mycorpse.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX =
            transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX;
        //gen point 에서 다시 살아남
        Position lastPos = currentPos;
        Ice underIce = (Ice)MapManager.instance.Find(typeof(Ice), currentPos);
        Respawn();
        if (isMoving && underIce != null)
        {
            StopCoroutine(MoveCoroutine());
            mycorpse.GetComponent<Corpse>().Slide(lastPos + moveDir, true);
        }
    }

    private void Respawn()
    {
        Teleport(GenPoint.ActivatingGenPoint.CurrentPos);
        StartCoroutine(RespawnAnimation());
    }

    private IEnumerator RespawnAnimation()
    {
        
       // Scheduler.instance.MoveReport(this);
        //respawn animation
        yield return new WaitForSeconds(1);
        //Scheduler.instance.StopReport(this);

    }

    public override void Move(Position destination,  bool anim)
    {
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

    private void SetLifeText()
    {
        GameObject.Find("LifeText").GetComponent<UnityEngine.UI.Text>().text = "×" + life;
    }

    public override void SaveHistory()
    {
        base.SaveHistory();
        lifeStack.Push(life);
    }

    public override void RollBack()
    {
        base.RollBack();
        if (lifeStack.Count > 0)
        {
            life = lifeStack.Pop();
            SetLifeText();
        }
    }
}

