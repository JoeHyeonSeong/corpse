using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMakeFloor : Floor {
    MakedWall myWall;
    protected override void Activate()
    {
        base.Activate();

        CreateWall();
    }

    protected override void Awake()
    {
        base.Awake();
        stepPriority = 1;
        if (InGameManager.IsInGameScene())
        {
            myWall = Instantiate(Resources.Load<MakedWall>("Prefab/InGameObject/MakedWall"),
            transform.position, Quaternion.identity, transform.parent);
        }
    }
    protected override void Deactivate()
    {
        base.Deactivate();
        if (myWall != null)
        {
            myWall.Sink();
            if (InGameManager.IsInGameScene())
            {
                MapManager.instance.ResizeSideLasers(currentPos);
            }
        }

    }


    public override void Leave(MovableObject who)
    {
        base.Leave(who);
        if (currentStatus == ActiveStatus.activating)
        {
            CreateWall();
        }
    }

    /// <summary>
    /// create wall if it is possible
    /// </summary>
    protected void CreateWall()
    {
        bool somethingIsOnMe = false;
        List<InGameObject> currentBlock = MapManager.instance.BlockData(currentPos);
        //check if loaded object is on me
        foreach (InGameObject obj in currentBlock)
        {
            if (obj.GetType().IsSubclassOf(typeof(LoadedObject)))
            {
                somethingIsOnMe = true;
                break;
            }
        }
        if (somethingIsOnMe == false)
        {
            myWall.Rise();
        }
    }
}
