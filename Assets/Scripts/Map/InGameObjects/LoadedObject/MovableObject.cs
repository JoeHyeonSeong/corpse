﻿using System;
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


    public override bool Push(MovableObject who, Position dir)
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
            return true;
        }
        return false;
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
    public virtual bool Move(Position destination, bool anim)
    {
        Position tempDir = destination - currentPos;
        //push
        PushCheck(destination);

        if (MapManager.instance.CanGo(destination))
        {
            Debug.Log(this.name);
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
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// change 'sprite' position
    /// </summary>
    /// <returns></returns>
    protected IEnumerator MoveCoroutine()
    {
        Scheduler.instance.MoveReport(this);
        mygraphic.localPosition = -moveDir.ToVector3();
        for (int i = 0; i < moveSpd; i++)
        {
            isMoving = true;
            mygraphic.localPosition += 1f / moveSpd * moveDir.ToVector3();
            yield return new WaitForEndOfFrame();
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

    protected virtual void MoveEnd()
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
            Debug.Log("밟은 것들"+fl.name);
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
            if (obj.GetType().IsSubclassOf(typeof(LoadedObject)))//pushable
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
            Debug.Log(pushResult);
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
