using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;
    void Awake()
    {
        instance = this;
        PauseEnd();
        GameObject.Find("Fader").GetComponent<Fader>().FadeIn(0.2f);
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
        Restart();
    }

    public void Pause()
    {
        GameObject.Find("Pause").GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void PauseEnd()
    {
        GameObject.Find("Pause").GetComponent<RectTransform>().anchoredPosition = new Vector3(2000,2000,0);
    }

    public void Exit()
    {
        StartCoroutine(ChangeScene(SceneName.main));
    }

    public void Restart()
    {
        HandOverData.ShowStageInfo = false;
        StartCoroutine(ChangeScene(SceneName.inGameScene));
    }

    public void StageClear()
    {
        int currentWorld = HandOverData.WorldIndex;
        if (HandOverData.WorldIndex == -1)
        {
            //mapedit
            StartCoroutine(ChangeScene(SceneName.mapEdit));
        }
        else if (HandOverData.StageIndex==StageList.GetWorldSize(HandOverData.WorldIndex)-1)
        {
            StageList.UnLock(HandOverData.WorldIndex+1,0);
            Exit();
        }
        else
        {
            HandOverData.StageIndex++;
            StageList.UnLock(HandOverData.WorldIndex,HandOverData.StageIndex);
            HandOverData.StageName = StageList.GetStageName(HandOverData.WorldIndex, HandOverData.StageIndex);
            StartCoroutine(ChangeScene(SceneName.inGameScene));
        }
    }

    IEnumerator ChangeScene(string sceneName)
    {
        const float changeTime = 0.3f;
        GameObject.Find("Fader").GetComponent<Fader>().FadeOut(changeTime);
        yield return new WaitForSeconds(changeTime);
        SceneManager.LoadScene(sceneName);
    }
}
