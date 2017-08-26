using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject :LoadedObject{

    /// <summary>
    /// if move is not end-> true
    /// </summary>
    private bool isMoving;

    protected Position moveDir;
    /// <summary>
    /// is nomalized, direction of last move
    /// </summary>
    public Position MoveDir { get { return moveDir; } }

    public override void Push(MovableObject who,Position dir)
    {
        
    }

    /// <summary>
    /// move to destination if not walk to teleport is true
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="teleport"></param>
    public void Move(Position destination, bool teleport)
    {
        Position tempDir = currentPos - destination;
        InGameObject[] desBlockData = MapManager.instance.BlockData(destination);
        //push
        foreach(InGameObject obj in desBlockData)
        {
            if(obj.GetType()==typeof(LoadedObject))
            {
                ((LoadedObject)obj).Push(this,tempDir);
            }
        }
        
        if(MapManager.instance.CanGo(destination)==false)
        {
            return;
        }
        //can go to destination
        currentPos = destination;
        if (teleport)
        {
            moveDir = null;
        }
        else
        {
            moveDir = tempDir;
        }
    }

    private void MoveEnd()
    {
        
    }
}
