using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    int savingPhase = 0;




    public static HistoryManager instance;


    private void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// commit move datas
    /// </summary>
    public void CommitHistory()
    {
        savingPhase++;
        Transform currentMaptf = MapManager.instance.CurrentMap.transform;
        for (int i = 0; i < currentMaptf.childCount; i++)
        {
            //layer
            Transform layer = currentMaptf.GetChild(i);
            for (int j = 0; j < layer.childCount; j++)
            {
                layer.GetChild(j).GetComponent<InGameObject>().SaveHistory();
            }
        }
    }



    public void RollBack()
    {
        if (savingPhase == 0)
            return;

        savingPhase--;
        Transform currentMaptf = MapManager.instance.CurrentMap.transform;
        for (int i = 0; i < currentMaptf.childCount; i++)
        {
            //layer
            Transform layer = currentMaptf.GetChild(i);
            for (int j = 0; j < layer.childCount; j++)
            {
                layer.GetChild(j).GetComponent<InGameObject>().RollBack();
            }
        }
    }
}



public class History
{
    public History(InGameObject.ActiveStatus status, Position pos,int activatingPoint)
    {
        this.status = status;
        this.pos = pos;
        this.activatingPoint = activatingPoint;
    }
    InGameObject.ActiveStatus status;
    public InGameObject.ActiveStatus Status { get { return status; } }
    Position pos;
    public Position Pos { get { return pos; } }
    int activatingPoint;
    public int ActivatingPoint { get { return activatingPoint; } }
}