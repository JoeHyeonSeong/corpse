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
        List<InGameObject> currentBlock = MapManager.instance.BlockData(des);
        foreach (InGameObject obj in currentBlock)
        {
            //touch
            if (obj.GetType() == typeof(Laser) || obj.GetType().IsSubclassOf(typeof(Laser)))
            {
                ((Laser)obj).Touch(this);
            }
        }
    }
}
