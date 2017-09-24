using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class StageLoader : MonoBehaviour {
    private Dictionary<int, GameObject> layerParents = new Dictionary<int, GameObject>();
    List<List<Vector3>> triggers;
    int currentX, currentY, currentZ;
    InGameObject.ActiveStatus previewStatus;
    int previewThreshold;
    InGameObject currentObj;
    private GameObject tileLevelParent;
    public List<Transform> tiles;
    private List<Vector3> currentTrigger;
    private Transform[,,] gameObjects;
    static public StageLoader instance;
    private void Awake()
    {
        instance = this;
    }

    public static StageData GetData(string name)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        // Reset the level
        TextAsset bFile = Resources.Load<TextAsset>("MapData/" + name);
        Stream file = new MemoryStream(bFile.bytes);
        // Convert the file from a byte array into a string
        string levelData = bFormatter.Deserialize(file) as string;
        // We're done working with the file so we can close it
        file.Close();
        return JsonUtility.FromJson<StageData>(levelData);
    }

    public GameObject LoadLevelUsingPath(string name)
    {
        // Enable the LevelEditor when the fileBrowser is done
        if (name.Length != 0)
        {
            LoadLevelFromStringLayers(GetData(name));
            return tileLevelParent;
        }
        else
        {
            Debug.Log("Invalid path given");
            return null;
        }
    }

    // Method that loads the layers
    private void LoadLevelFromStringLayers(StageData sData)
    {
      int  HEIGHT = sData.height;
      int  WIDTH = sData.width;
      int  LAYERS = sData.layer;
       string title = sData.title;
       int life = sData.life;
        int counter = 0;
        gameObjects = new Transform[WIDTH, HEIGHT, LAYERS];

        triggers = new List<List<Vector3>>();
        tileLevelParent = GameObject.Find("TileLevel");
        if (tileLevelParent == null)
        {
            tileLevelParent = new GameObject("TileLevel");
            StageInfo stageInfo=tileLevelParent.AddComponent<StageInfo>();
            stageInfo.Title = title;
            stageInfo.Life = life;
        }
        for (int i = 0; i < sData.triggerNum.Length; i++)
        {
            List<Vector3> temp = new List<Vector3>();
            for (int j = 0; j < sData.triggerNum[i]; j++)
            {
                temp.Add(sData.trigger[counter]);
                counter++;
            }
            triggers.Add(temp);
        }
        counter = 0;
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int z = 0; z < LAYERS; z++)
                {
                    currentX = x; currentY = y; currentZ = z;

                    previewStatus = sData.state[counter];
                    previewThreshold = sData.threshold[counter];
                  Transform obj= CreateBlock(sData.level[counter], x, y, z);
                    gameObjects[x, y, z] = obj;
                    if (obj != null)
                    {
                        currentObj =obj.GetComponent<InGameObject>();
                        if (currentObj.GetType() == typeof(FlipButton) || currentObj.GetType().IsSubclassOf(typeof(FlipButton)))
                        {
                            AttachOperands();
                        }
                    }
                    counter++;
                }
            }
        }
    }


    private Transform CreateBlock(int value, int xPos, int yPos, int zPos)
    {
        // The transform to create
        Transform toCreate = null;
        // Return on invalid positions
        // Set the value for the internal level representation
        // If the value is not empty, set it to the correct tile
        if (value != -1)
        {
            toCreate = tiles[value];
        }
        if (toCreate != null)
        {
            //Create the object we want to create
            Transform newObject = Instantiate(toCreate, new Vector3(xPos, yPos, toCreate.position.z), Quaternion.identity) as Transform;
            if (newObject.GetComponent<InGameObject>() != null)
            {
                InGameObject inObj = newObject.GetComponent<InGameObject>();
                inObj.SetSortingOrder();
                inObj.ActivateThreshold = previewThreshold;
                inObj.CurrentStatus = previewStatus;
            }
            //Give the new object the same name as our tile prefab
            newObject.name = toCreate.name;
            // Set the object's parent to the layer parent variable so it doesn't clutter our Hierarchy
            newObject.parent = GetLayerParent(zPos).transform;
            // Add the new object to the gameObjects array for correct administration
            return newObject;
        }
        return null;
    }

    private GameObject GetLayerParent(int layer)
    {
        if (!layerParents.ContainsKey(layer))
        {
            GameObject layerParent = new GameObject("Layer " + layer);
            layerParent.transform.parent = tileLevelParent.transform;
            layerParents.Add(layer, layerParent);
        }
        return layerParents[layer];
    }

    private void AttachOperands()
    {
        List<InGameObject> operands = new List<InGameObject>();
        FindCurrentTrigger();
        for (int i = 1; i < currentTrigger.Count; i++)
        {
            operands.Add(gameObjects[(int)currentTrigger[i].x, (int)currentTrigger[i].y, (int)currentTrigger[i].z].GetComponent<InGameObject>());
        }
        currentObj.GetComponent<FlipButton>().Operands = operands;
    }


    private void FindCurrentTrigger()
    {
        foreach (List<Vector3> candidate in triggers)
        {
            if (candidate[0] == new Vector3(currentX, currentY, currentZ))
            {
                currentTrigger = candidate;
                return;
            }
        }
        List<Vector3> newTrigger = new List<Vector3>();
        newTrigger.Add(new Vector3(currentX, currentY, currentZ));
        triggers.Add(newTrigger);
        currentTrigger = newTrigger;
    }

}
