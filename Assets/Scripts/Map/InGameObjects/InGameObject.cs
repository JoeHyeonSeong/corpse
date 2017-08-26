using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InGameObject : MonoBehaviour {
    /// <summary>
    /// current position of gameObject
    /// </summary>
    protected Position currentPos;
    /// <summary>
    /// return current position of game Object(read only)
    /// </summary>
    public Position CurrentPos { get { return currentPos; } }
    /// <summary>
    /// if can't activate-> null else if activated->true else->false
    /// </summary>
    protected bool? currentStatus;


    /// <summary>
    /// gain setting from data
    /// </summary>
    /// <param name="data"></param>
    public virtual void InitialSetting(InGameObjectData data)
    {
        if(data.initialStatus!=null)
        {
            if((bool)data.initialStatus)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }
        
        currentPos = new Position(data.pos);
        transform.position =currentPos.ToVector3();
       
    }

    /// <summary>
    /// convert currentStatus to true
    /// </summary>
    public virtual void Activate()
    {
        currentStatus = true;
    }

    /// <summary>
    /// convert currentStatus to false
    /// </summary>
    public virtual void Deactivate()
    {
        currentStatus = false;
    }
}
