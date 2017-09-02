﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMakeFloor : Floor {
    MakedWall myWall;
    public override void Activate()
    {
        base.Activate();

        CreateWall();
    }

    protected override void Awake()
    {
        base.Awake();
        myWall = Instantiate(Resources.Load<MakedWall>("Prefab/InGameObject/MakedWall"),
        transform.position, Quaternion.identity, transform.parent);
    }
    public override void Deactivate()
    {
        base.Deactivate();
        if (myWall != null)
        {
            myWall.gameObject.SetActive(false);
            MapManager.instance.ResizeSideLasers(currentPos);
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

            myWall.gameObject.SetActive(true);
        }
    }
}
