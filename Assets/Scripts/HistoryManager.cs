using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    int savingPhase = 0;




    public static HistoryManager instance;

    Stack<History[]> moveStack;
    List<History> currentHistory = new List<History>();

    private void Awake()
    {
        instance = this;
        moveStack = new Stack<History[]>();
    }


    /// <summary>
    /// commit move datas
    /// </summary>
    public void CommitHistory()
    {
        savingPhase++;
        Debug.Log(savingPhase);
        Transform currentMaptf = MapManager.instance.CurrentMap.transform;
        for (int i = 0; i < currentMaptf.childCount; i++)
        {
            currentMaptf.GetChild(i).GetComponent<InGameObject>().SaveHistory();
        }
    }



    public void RollBack()
    {
        if (savingPhase == 0)
            return;

        savingPhase--;
        Debug.Log(savingPhase);
        Transform currentMaptf = MapManager.instance.CurrentMap.transform;
        for (int i = 0; i < currentMaptf.childCount; i++)
        {
            currentMaptf.GetChild(i).GetComponent<InGameObject>().RollBack();
        }
    }
}



public class History
{
    public History(InGameObject.ActiveStatus status, Position pos)
    {
        this.status = status;
        this.pos = pos;
    }
    InGameObject.ActiveStatus status;
    public InGameObject.ActiveStatus Status { get { return status; } }
    Position pos;
    public Position Pos { get { return pos; } }
}