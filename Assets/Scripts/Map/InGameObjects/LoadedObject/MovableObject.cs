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

    public override void Push(MovableObject who, Position dir)
    {
        Position des = currentPos + dir;
        if (MapManager.instance.CanGo(des))
        {
            List<InGameObject> currentBlockData=MapManager.instance.BlockData(des);
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
                Move(des, true, true);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        moveDir = new Position(0, 0);
    }
    /// <summary>
    /// move to destination if not walk to teleport is true
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="teleport"></param>
    public void Move(Position destination, bool saveHistory, bool anim)
    {
        Position tempDir = destination - currentPos;
        PushCheck(destination);

        if (MapManager.instance.CanGo(destination))
        {
            //can go to destination
            if (saveHistory)
            {
                InGameManager.instance.MoveSign(this);
            }
            //leave
            LeaveCheck();
            //change current position
            Position lastPos = currentPos;
            currentPos = destination;
            moveDir = tempDir;
            transform.position = currentPos.ToVector3();
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
        Transform mySprite = transform.Find("Sprite");
        mySprite.localPosition = -moveDir.ToVector3();
        for (int i = 0; i < moveSpd; i++)
        {
            isMoving = true;
            mySprite.localPosition += 1f / moveSpd * moveDir.ToVector3();
            yield return new WaitForEndOfFrame();
        }
        MoveEnd();
    }

    public override void Teleport(Position des)
    {
        base.Teleport(des);
        List<InGameObject> currentBlock = MapManager.instance.BlockData(des);
        StepCheck();
    }

    private void MoveEnd()
    {
        StepCheck();
        //touch
        TouchCheck();
        isMoving = false;
        InGameManager.instance.StopSign(this);
    }

    protected void StepCheck()
    {
        List<InGameObject> currentBlockData = MapManager.instance.BlockData(currentPos);
        //step
        foreach (InGameObject obj in currentBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(Floor)) || obj.GetType() == typeof(Floor))
            {
                ((Floor)obj).Step(this);
            }
        }
    }

    protected void LeaveCheck()
    {
        //leave
        List<InGameObject> originDesBlockData = MapManager.instance.BlockData(currentPos);
        foreach (InGameObject obj in originDesBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(Floor)) || obj.GetType() == typeof(Floor))
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
        Position tempDir = at - currentPos;
        List<InGameObject> desBlockData = MapManager.instance.BlockData(at);
        bool push = false;
        //push
        foreach (InGameObject obj in desBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(LoadedObject)))
            {
                ((LoadedObject)obj).Push(this, tempDir);
                push = true;
            }
        }
        return push;
    }

    public void Slide(Position destination,bool haveToMove)
    {
        if (isMoving||haveToMove)
        {
            bool pushResult = PushCheck(destination);
            if (pushResult == false)
            {
                Move(destination, true, true);
            }
        }
    }
}
