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
    private bool isMoving;

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
            Move(des,false);
        }
    }

    /// <summary>
    /// move to destination if not walk to teleport is true
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="teleport"></param>
    public void Move(Position destination,bool rollback)
    {

        Position tempDir = destination - currentPos;
        PushCheck(destination);

        if (MapManager.instance.CanGo(destination))
        {
            //can go to destination
            if (!rollback)
            {
                InGameManager.instance.MoveSign(this);
            }
            //leave
            LeaveCheck();

            //change current position
            currentPos = destination;
            moveDir = tempDir;
            transform.position = currentPos.ToVector3();
            transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = -currentPos.Y * 10;
            if (!rollback)
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
    IEnumerator MoveCoroutine()
    {
        Transform mySprite = transform.Find("Sprite");
        mySprite.localPosition = -moveDir.ToVector3();
       for (int i=0;i<moveSpd;i++)
        {
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



    protected void PushCheck(Position des)
    {
        Position tempDir = des - currentPos;
        List<InGameObject> desBlockData = MapManager.instance.BlockData(des);
        //push
        foreach (InGameObject obj in desBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(LoadedObject)))
            {
                ((LoadedObject)obj).Push(this, tempDir);
            }
        }
    }
}
