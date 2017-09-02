using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour {
    public enum GameCycle
    {
        InputTime,
        MoveTime
    }
    public static Scheduler instance;

    private GameCycle currentCycle;
    public GameCycle CurrentCycle { get { return currentCycle; } }
    /// <summary>
    /// 아직 움직이는 중인 오브젝트
    /// </summary>
    List<InGameObject> movingObject=new List<InGameObject>();


    private void Awake()
    {
        instance = this;
        currentCycle = GameCycle.InputTime;
    }

    public void MoveReport(InGameObject obj)
    {
        movingObject.Add(obj);
        if (currentCycle == GameCycle.InputTime)
        {
            //InGameManager.instance.NewPhase();
            currentCycle = GameCycle.MoveTime;
        }
    }

    public void StopReport(InGameObject obj)
    {
        movingObject.Remove(obj);
        //end of move time
        if (movingObject.Count == 0)
        {
            currentCycle = GameCycle.InputTime;
        }
    }
}
