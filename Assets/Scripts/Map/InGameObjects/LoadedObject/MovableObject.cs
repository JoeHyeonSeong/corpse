using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : LoadedObject
{

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
            Move(des);
        }
    }

    /// <summary>
    /// move to destination if not walk to teleport is true
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="teleport"></param>
    public void Move(Position destination)
    {
        Position tempDir = destination - currentPos;
        List<InGameObject> desBlockData = MapManager.instance.BlockData(destination);
        //push
        foreach (InGameObject obj in desBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(LoadedObject)))
            {
                ((LoadedObject)obj).Push(this, tempDir);
            }
        }

        if (MapManager.instance.CanGo(destination))
        {
            //can go to destination
            //leave
            List<InGameObject> originDesBlockData = MapManager.instance.BlockData(currentPos);
            foreach (InGameObject obj in originDesBlockData)
            {
                if (obj.GetType().IsSubclassOf(typeof(Floor)) || obj.GetType() == typeof(Floor))
                {
                    ((Floor)obj).Leave(this);
                }
            }

            //change current position
            currentPos = destination;
            moveDir = tempDir;
            //temp
            transform.position = currentPos.ToVector3();
            //step
            foreach (InGameObject obj in desBlockData)
            {
                if (obj.GetType().IsSubclassOf(typeof(Floor)) || obj.GetType() == typeof(Floor))
                {
                    ((Floor)obj).Step(this);
                }
            }
            //touch
            foreach (InGameObject obj in desBlockData)
            {
                if (obj.GetType().IsSubclassOf(typeof(Laser)) || obj.GetType() == typeof(Laser))
                {
                    ((Laser)obj).Touch(this);
                }
            }
        }

    }

    public override void Teleport(Position des)
    {
        base.Teleport(des);
        List<InGameObject> currentBlock = MapManager.instance.BlockData(des);
        foreach (InGameObject obj in currentBlock)
        {
            //step
            if (obj.GetType() == typeof(Floor) || obj.GetType().IsSubclassOf(typeof(Floor)))
            {
                ((Floor)obj).Step(this);
            }
        }
    }

    private void MoveEnd()
    {

    }
}
