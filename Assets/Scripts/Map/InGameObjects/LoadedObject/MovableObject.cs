using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : LoadedObject
{

    /// <summary>
    /// move speed
    /// </summary>
    protected const int moveSpd = 10;
    /// <summary>
    /// if move is not end-> true
    /// </summary>
    protected bool isMoving;

    protected Position moveDir;
    /// <summary>
    /// is nomalized, direction of last move
    /// </summary>
    public Position MoveDir { get { return moveDir; } }

    /// <summary>
    /// last position before move
    /// </summary>
    /// <param name="who"></param>
    /// <param name="dir"></param>
    protected Position lastPos;


    public override void Push(MovableObject who, Position dir)
    {
        Position des = currentPos + dir;
        if (MapManager.instance.CanGo(des))
        {
            List<InGameObject> currentBlockData = MapManager.instance.BlockData(des);
            bool iceCheck = false;
            foreach (InGameObject obj in currentBlockData)
            {
                if (obj.GetType() == typeof(Ice))
                {
                    iceCheck = true;
                }
            }
            if (iceCheck)
            {
                Slide(des, true);
            }
            else
            {
                Move(des, true);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        moveDir = new Position(0, 0);
    }

    /// <summary>
    /// move to destination if can
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="isSliding"></param>
    /// <param name="anim"></param>
    public virtual void Move(Position destination, bool anim)
    {
        Position tempDir = destination - currentPos;
        //push
        PushCheck(destination);



        if (MapManager.instance.CanGo(destination))
        {
            //can go to destination
            lastPos = currentPos;
            //change current position
            currentPos = destination;
            moveDir = tempDir;
            transform.position = currentPos.ToVector3();
            //leave
            LeaveCheck();
            //resize laser
            MapManager.instance.ResizeSideLasers(lastPos);
            transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = -currentPos.Y * 10;
            if (anim)
            {
                StartCoroutine(MoveCoroutine());
            }
            else
            {
                MoveEnd();
            }
        }

    }

    /// <summary>
    /// change 'sprite' position
    /// </summary>
    /// <returns></returns>
    protected IEnumerator MoveCoroutine()
    {
        Scheduler.instance.MoveReport(this);
        Transform mySprite = transform.Find("Sprite");
        mySprite.localPosition = -moveDir.ToVector3();
        for (int i = 0; i < moveSpd; i++)
        {
            isMoving = true;
            mySprite.localPosition += 1f / moveSpd * moveDir.ToVector3();
            yield return new WaitForSeconds(0.015f);
        }
        MoveEnd();
    }

    public override void Teleport(Position des)
    {
        lastPos = currentPos;
        base.Teleport(des);
        LeaveCheck();
        StepCheck();
    }

    private void MoveEnd()
    {
        //touch
        MapManager.instance.ResizeSideLasers(currentPos);
        //step
        StepCheck();
        isMoving = false;
        Scheduler.instance.StopReport(this);
    }

    protected void StepCheck()
    {
        Position stepPos = currentPos;
        List<InGameObject> currentBlockData = MapManager.instance.BlockData(stepPos);
        List<Floor> currentFloor = new List<Floor>();
        //floor만 고르기
        foreach (InGameObject obj in currentBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(Floor)))
            {
                currentFloor.Add((Floor)obj);
            }
        }
        //priority로 sorting
        currentFloor.Sort();
        //step
        foreach (Floor fl in currentFloor)
        {
            if (stepPos != currentPos)
            {
                break;
            }
            fl.Step(this);
        }
    }

    protected void LeaveCheck()
    {
        //leave
        if (lastPos == null)
        {
            return;
        }
        List<InGameObject> originDesBlockData = MapManager.instance.BlockData(lastPos);
        foreach (InGameObject obj in originDesBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(Floor)))
            {
                ((Floor)obj).Leave(this);
            }
        }
    }


    /// <summary>
    /// push and return whether push at least one
    /// </summary>
    /// <param name="at"></param>
    /// <returns></returns>
    protected bool PushCheck(Position at)
    {
        Position tempDir = at - currentPos;//내가 움직일 방향
        List<InGameObject> desBlockData = MapManager.instance.BlockData(at);
        bool push = false;
        //push
        foreach (InGameObject obj in desBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(LoadedObject))//pushable
               &&!(MapManager.instance.Find(typeof(Ice),currentPos)!=null&&
               MapManager.instance.Find(typeof(DefaultFloor),at) != null && isMoving))
            {
                    ((LoadedObject)obj).Push(this, tempDir);
                    push = true;
            }
        }
        return push;
    }

    public void Slide(Position destination, bool haveToMove)
    {
        if (isMoving || haveToMove)
        {
            bool pushResult = PushCheck(destination);
            if (pushResult == false)
            {
                Move(destination, true);
            }
            else if(MapManager.instance.Find(typeof(Ice),destination)==null)
            {
                Move(destination, true);
            }
        }
    }
}
