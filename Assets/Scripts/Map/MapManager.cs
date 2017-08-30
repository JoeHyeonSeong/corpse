using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    GameObject currentMapPrefs;
    /// <summary>
    ///objects in one block [pos.x,pos.y] can find with Position
    /// </summary>
    public static MapManager instance;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        GenerateMap();
    }
    /// <summary>
    /// generate new map using prefab
    /// </summary>
    private void GenerateMap()
    {
        HandOverData.Stagenum = 1;
       currentMapPrefs=Resources.Load<GameObject>("Prefab/Map/Stage"+HandOverData.Stagenum.ToString());
        Instantiate(currentMapPrefs);
    }

    /// <summary>
    /// return if object can go to des
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public bool CanGo(Position pos)
    {
        bool existFloor=false;
        bool noObstable=true;
        List<InGameObject> currentBlock = BlockData(pos);

        foreach(InGameObject obj in currentBlock)
        {
            if(obj.GetType().IsSubclassOf(typeof(Floor))||obj.GetType()==typeof(Floor))
            {
                existFloor = true;
            }
            if(obj.GetType().IsSubclassOf(typeof(LoadedObject))&&((LoadedObject)obj).IsObstacle)
            {
                noObstable=false;
            }
        }
        if(existFloor&&noObstable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// return ingame objects in position
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public List<InGameObject> BlockData(Position pos)
    {
        List<InGameObject> currentBlock = new List<InGameObject>();
        RaycastHit2D[] rayHits = Physics2D.RaycastAll(pos.ToVector3() + Vector3.back, Vector3.forward, 1, 1 << LayerMask.NameToLayer("InGameObject"));
        foreach (RaycastHit2D rayHit in rayHits)
        {
            InGameObject obj = rayHit.transform.GetComponent<InGameObject>();
            if (obj != null)
            {
                currentBlock.Add(obj);
            }
        }
        return currentBlock;
    }

    /// <summary>
    /// resize lasers adjacent of pos
    /// </summary>
    /// <param name="pos"></param>
    public void ResizeSideLasers(Position pos)
    {
        /*
        List<InGameObject>[] sideBlockDatas =
            {
            BlockData(pos+Direction.Dir4ToPos(Dir4.Down)),
            BlockData(pos+Direction.Dir4ToPos(Dir4.Up)),
            BlockData(pos+Direction.Dir4ToPos(Dir4.Right)),
            BlockData(pos+Direction.Dir4ToPos(Dir4.Left))
        };
        for (int i = 0; i < 4; i++)
        {
            foreach (InGameObject obj in sideBlockDatas[i])
            {
                if (obj.GetType() == typeof(Laser))
                {
                    ((Laser)obj).Resize();
                }
            }
        }
        */
        List<InGameObject> checkBlockData = BlockData(pos);
        foreach (InGameObject obj in checkBlockData)
        {
            if (obj.GetType() == typeof(Laser))
            {
                ((Laser)obj).Resize();
            }
        }
    }
}
