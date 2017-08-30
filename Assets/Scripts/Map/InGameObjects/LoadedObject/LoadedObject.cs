using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoadedObject : InGameObject {
    protected override void Awake()
    {
        base.Awake();
    }
    protected bool isObstacle=true;
    public bool IsObstacle { get { return isObstacle; } }
    abstract public void Push(MovableObject who,Position dir);

    public override void Teleport(Position des)
    {
        base.Teleport(des);
        TouchCheck();
    }

    protected void TouchCheck()
    {
        List<InGameObject> currentBlockData = MapManager.instance.BlockData(currentPos);
        foreach (InGameObject obj in currentBlockData)
        {
            if (obj.GetType().IsSubclassOf(typeof(Laser)) || obj.GetType() == typeof(Laser))
            {
                ((Laser)obj).Touch(this);
            }
        }
    }
}
