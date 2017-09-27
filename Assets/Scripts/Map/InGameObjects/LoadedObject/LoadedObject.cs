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
        if (InGameManager.IsInGameScene())
        {
            MapManager.instance.ResizeSideLasers(currentPos);
        }
    }
}
