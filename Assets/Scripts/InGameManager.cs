using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;
    void Awake()
    {
        instance = this;
    }

    public void NewPhase()
    {
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
