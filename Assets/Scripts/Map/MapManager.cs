using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public Position SpacePos { get { return new Position(100, 100); } }

    GameObject currentMapPrefs;
    private GameObject currentMap;
    public GameObject CurrentMap { get { return currentMap; } }
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

    public GameObject makingMapPref;

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
        // currentMapPrefs=Resources.Load<GameObject>("Prefab/Map/Stage"+HandOverData.Stagenum.ToString());
        currentMapPrefs = makingMapPref;
        //currentMap=Instantiate(currentMapPrefs);
       currentMap = StageLoader.instance.LoadLevelUsingPath(HandOverData.Stage);
        //set cam pos

        float minX = 999;
        float maxX = -999;
        float minY = 999;
        float maxY = -999;
        for (int i = 0; i < currentMap.transform.childCount; i++)
        {
            //layer
            Transform layer = currentMap.transform.GetChild(i);
            for (int j = 0; j <layer.childCount; j++)
            {
                Vector2 childPos = layer.GetChild(j).position;
                if (childPos.x > maxX) maxX = childPos.x;
                if (childPos.x < minX) minX = childPos.x;
                if (childPos.y > maxY) maxY = childPos.y;
                if (childPos.y < minY) minY = childPos.y;
            }
        }
        CamCtrl.instance.SetPosition(new Vector2((minX + maxX) / 2, (minY + maxY) / 2));
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
        if(pos==SpacePos||(existFloor&&noObstable))
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
        List<InGameObject> checkBlockData = BlockData(pos);
        foreach (InGameObject obj in checkBlockData)
        {
            if (obj.GetType() == typeof(Laser))
            {
                ((Laser)obj).Resize();
            }
        }
    }

    /// <summary>
    ///type이 t이거나 t의 subclass인것중 가장 먼저 발견한 것을 반환한다.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public InGameObject Find(System.Type t, Position pos)
    {
        List<InGameObject> checkBlock = BlockData(pos);
        foreach (InGameObject obj in checkBlock)
        {
            if (obj.GetType() == t || obj.GetType().IsSubclassOf(t))
            {
                return obj;
            }
        }
        return null;
    }
}
