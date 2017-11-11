using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : DestroyableObject
{
    private bool canMove=true;

    public static Character instance;

    private Queue<Position> moveDirQueue = new Queue<Position>();

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        /*
        if (InGameManager.IsInGameScene())
        {
            life = StageInfo.instance.Life;
            SetLifeText();
        }
        */
    }

    public override void Destroy()
    {
        // life--;
        //SetLifeText();
        canMove = false;
        Revive();
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
        StopCoroutine(MoveCoroutine());
        if (isMoving && underIce != null)//움직이는 중이고 밑에 얼음 있음
        {
            mycorpse.GetComponent<Corpse>().Slide(lastPos + moveDir, true);
        }
    }

    private void Respawn()
    {
        Teleport(GenPoint.ActivatingGenPoint.CurrentPos);
        StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {
        //respawn animation
        mygraphic.GetComponent<Animator>().Play("Respawn");
        Scheduler.instance.MoveReport(this);
        yield return new WaitForSeconds(1);
        Scheduler.instance.StopReport(this);
        canMove = true;
        mygraphic.GetComponent<Animator>().Play("Character_Default");
        MoveOrderDequeue();
    }

    public override void Move(Position destination, bool anim)
    {
        Debug.Log("d");
        base.Move(destination, anim);
        bool flipX = transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX;
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


    public override void SaveHistory()
    {
        base.SaveHistory();
       // lifeStack.Push(life);
    }

    public override void RollBack()
    {
        base.RollBack();
        /*
        if (lifeStack.Count > 0)
        {
            life = lifeStack.Pop();
            SetLifeText();
        }
        */
    }

    /// <summary>
    /// 다음에 destination으로 이동하라고 명령
    /// 캐릭터의 조종을 위한 함수임
    /// 평소엔 Move쓰셈
    /// </summary>
    /// <param name="direction"></param>
    public void MoveOrderEnqueue(Position direction)
    {
        if (isMoving||!canMove)
        {
            moveDirQueue.Enqueue(direction);
        }
        else
        {
            TryMove(direction);
        }
    }

    private void TryMove(Position dir)
    {
        Position initPos = currentPos;
        InGameManager.instance.NewPhase();
        Move(dir+ currentPos, true);
        if (initPos == Character.instance.CurrentPos)
        {
            InGameManager.instance.RollBack();
        }
    }

    protected override void MoveEnd()
    {
        base.MoveEnd();
        MoveOrderDequeue();
    }

    private void MoveOrderDequeue()
    {
        Debug.Log(isMoving + "ismoving");
        if (moveDirQueue.Count > 0&&canMove&&Scheduler.instance.CurrentCycle==Scheduler.GameCycle.InputTime)
        {
            TryMove(moveDirQueue.Dequeue());
        }
    }
}

