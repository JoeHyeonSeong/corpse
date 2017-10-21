using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;
    private int worldIndex;
    public int WorldIndex{get{return worldIndex;}}
    private int stageIndex;
    public int StageIndex { get { return stageIndex;}}

    private const float restartWaitTime = 0.2f;
    private const float exitWaitTime = 1f;
    private void Awake()
    {
        worldIndex = HandOverData.WorldIndex;
        stageIndex = HandOverData.StageIndex;
        instance = this;
        PauseEnd();
        SetMapEditButton();
    }

    private void Start()
    {
        MapManager.instance.GenerateMap();
        ShowStageInfo();
    }

    public void NewPhase()
    {
        HistoryManager.instance.CommitHistory();
    }

    /// <summary>
    /// 맵 바로 전상황으로 바꿔줌
    /// </summary>
    public void RollBack()
    {
        if (Scheduler.instance.CurrentCycle == Scheduler.GameCycle.InputTime)
        {
        HistoryManager.instance.RollBack();
        }
        
    }

    private void ShowStageInfo()
    {
        GameObject.Find("Fader").GetComponent<ImageFader>().BlackOut();
        float fadeTime;
        if (HandOverData.ShowStageInfo)
        {
            FadeInStageInfo();
            Invoke("FadeOutStageInfo", 1f);
            fadeTime = 2f;
        }
        else
        {
            fadeTime = 0.5f;
        }
        Invoke("FadeIn",fadeTime);
    }

    /// <summary>
    /// 현재 씬이 인게임인지 확인함
    /// </summary>
    /// <returns></returns>
    static public bool IsInGameScene()
    {
        return SceneManager.GetActiveScene().name == SceneName.inGameScene;
    }

    /// <summary>
    /// 게임 오버
    /// </summary>
    public void GameOver()
    {
        Debug.Log("Game Over");
        Restart();
    }

    /// <summary>
    /// 맵 에딧 화면으로 보냄
    /// </summary>
    public void GoToMapEdit()
    {
        SceneManager.LoadScene(SceneName.mapEdit);
    }

    /// <summary>
    /// 게임 정지시킴
    /// </summary>
    public void Pause()
    {
        GameObject pause=GameObject.Find("Pause");
        pause.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        pause.transform.Find("ExitButton").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => Exit(MainManager.View.StageSelect));
        pause.transform.Find("RestartButton").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => Restart());
    }

    /// <summary>
    /// 정지 끝냄
    /// </summary>
    public void PauseEnd()
    {
        GameObject.Find("Pause").GetComponent<RectTransform>().anchoredPosition = new Vector3(2000,2000,0);
    }

    /// <summary>
    /// 메인씬으로 나감
    /// </summary>
    public void Exit(MainManager.View mainView)
    {
        HandOverData.mainView = mainView;
        StartCoroutine(ChangeScene(SceneName.main,exitWaitTime));
    }

    /// <summary>
    /// 스테이지 다시시작
    /// </summary>
    public void Restart()
    {
        HandOverData.ShowStageInfo = false;
        StartCoroutine(ChangeScene(SceneName.inGameScene,restartWaitTime));
    }

    /// <summary>
    /// 스테이지 클리어 했을때의 행동을 함
    /// </summary>
    public void StageClear()
    {
        int currentWorld = HandOverData.WorldIndex;
        BackGround.instance.LightOn();
        if (HandOverData.WorldIndex == -1)
        {
            //mapedit
            StartCoroutine(ChangeScene(SceneName.mapEdit,exitWaitTime));
        }
        else if (HandOverData.StageIndex==StageList.GetWorldSize(HandOverData.WorldIndex)-1)
        {
            StageList.UnLock(HandOverData.WorldIndex+1,0);
            HandOverData.WorldIndex++;
            Exit(MainManager.View.WorldSelect);
        }
        else
        {
            HandOverData.StageIndex++;
            StageList.UnLock(HandOverData.WorldIndex,HandOverData.StageIndex);
            HandOverData.StageName = StageList.GetStageName(HandOverData.WorldIndex, HandOverData.StageIndex);
            StartCoroutine(ChangeScene(SceneName.inGameScene,exitWaitTime));
        }
    }


    /// <summary>
    /// 점점 어두어지다가 씬이 바뀜
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator ChangeScene(string sceneName,float waitTime)
    {
        const float beforeTime = 2f;
        const float changeTime = 0.3f;
        yield return new WaitForSeconds(beforeTime);
        GameObject.Find("Fader").GetComponent<ImageFader>().Opaque(changeTime);
        yield return new WaitForSeconds(changeTime);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// map editting화면으로 돌아가게 하는 버튼을 보여줌
    /// </summary>
    private void SetMapEditButton()
    {
        if (HandOverData.WorldIndex != -1)
        {
            GameObject.Find("TestEndButton").SetActive(false);
        }
    }

    /// <summary>
    /// 화면이 검정->게임화면으로 변하게함
    /// </summary>
    private void FadeIn()
    {
        GameObject.Find("Fader").GetComponent<ImageFader>().Transparent(0.5f);
    }


    private void FadeOutStageInfo()
    {
        const float time = 0.3f;
        GameObject.Find("Title").GetComponent<TextFader>().Transparent(time);
        GameObject.Find("Chapter").GetComponent<TextFader>().Transparent(time);
    }

    private void FadeInStageInfo()
    {
        const float time = 0.2f;
        GameObject.Find("Title").GetComponent<TextFader>().Opaque(time);
        GameObject.Find("Chapter").GetComponent<TextFader>().Opaque(time);
    }
}
