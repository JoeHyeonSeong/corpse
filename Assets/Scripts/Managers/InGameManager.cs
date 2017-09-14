using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void GameOver()
    {
        Debug.Log("Game Over");
        HandOverData.ShowStageInfo = false;
        SceneManager.LoadScene(SceneName.inGameScene);
    }
}
