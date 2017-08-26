using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    /// <summary>
    /// over all map data of this game
    /// </summary>
    List<List<InGameObjectData>> mapDatas;
    /// <summary>
    ///objects in one block [pos.x,pos.y] can find with Position
    /// </summary>
    List<InGameObject>[,] blockDatas;
    public InGameObject[] BlockData(Position pos)
    {
        return blockDatas[pos.X, pos.Y].ToArray();
    }
    public static MapManager instance;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// fetch map data from text file
    /// </summary>
    private void FetchMapData()
    {
        string jsonData;
        jsonData = TextReader.ReadFile("MapDatas");
        mapDatas = JsonUtility.FromJson<List<List<InGameObjectData>>>(jsonData);
    }

    /// <summary>
    /// generate new map using map data
    /// </summary>
    private void GenerateMap()
    {
        if(mapDatas==null)
        {
            FetchMapData();
        }

        List<InGameObjectData> currentMapData;
        currentMapData = mapDatas[HandOverData.Stagenum];

        int minX=9999,minY=9999,maxX=-9999,maxY=-9999;
        //find max min x,y
        for(int i=0;i<currentMapData.Count;i++)
        {
            Position currentPos =new Position( currentMapData[i].pos.x,currentMapData[i].pos.y);
            minX = (minX > currentPos.X) ? currentPos.X : minX;
            minY = (minY > currentPos.Y) ? currentPos.Y : minY;
            maxX = (maxX < currentPos.X) ? currentPos.X : maxX;
            maxY = (maxY < currentPos.Y) ? currentPos.Y : maxY;
        }

        blockDatas = new List<InGameObject>[maxX-minX+1,maxY-minY+1];

        //set blockDatas
        for(int i=-0;i<currentMapData.Count;i++)
        {
            Position currentPos = new Position(currentMapData[i].pos.x-minX, currentMapData[i].pos.y-minY);
            blockDatas[currentPos.X, currentPos.Y].Add(CreateInGameObject(currentMapData[i]));
        }
    }

    /// <summary>
    ///create and return IngameObject(temp)
    /// </summary>
    /// <returns></returns>
    public InGameObject CreateInGameObject(InGameObjectData data)
    {
  InGameObject createdObject= Instantiate<InGameObject>(Resources.Load<InGameObject>("Prefab/InGameObject" + data.type.ToString()), this.transform);
        createdObject.InitialSetting(data);
        return createdObject;
    }
    /// <summary>
    /// return if object can go to des
    /// </summary>
    /// <param name="des"></param>
    /// <returns></returns>
    public bool CanGo(Position des)
    {
        bool existFloor=false;
        bool noObstable=true;
        List<InGameObject> currentBlock = blockDatas[des.X, des.Y];
        foreach(InGameObject obj in currentBlock)
        {
            if(obj.GetType()==typeof(Floor))
            {
                existFloor = true;
            }
            if(obj.GetType()==typeof(LoadedObject)&&((LoadedObject)obj).IsObstacle)
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
}
