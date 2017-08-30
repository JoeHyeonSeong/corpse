using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;
    private int phase=0;
    public int Phase { get { return phase; }set { phase = value; } }
    void Awake()
    {
        instance = this;
    }

    public void MoveSign(InGameObject obj)
    {
        Scheduler.instance.MoveReport(obj);
        HistoryManager.instance.SaveMove(obj, obj.CurrentPos,false);
    }

    public void StopSign(InGameObject obj)
    {
        Scheduler.instance.StopReport(obj);
    }

    public void NewPhase()
    {
        phase++;
        Debug.Log("phase:" + phase);
        HistoryManager.instance.CommitHistory();
    }

    public void RollBack()
    {
        if (Scheduler.instance.CurrentCycle == Scheduler.GameCycle.InputTime)
        {
            HistoryManager.instance.RollBack();
        }
        
    }
}
