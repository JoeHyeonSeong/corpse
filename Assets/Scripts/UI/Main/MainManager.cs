using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private ViewController startCon;
    private ViewController worldSelectCon;
    private ViewController stageSelectCon;
    private ViewController optionCon;
    private ViewController currentCon;

    private GameObject backButton;

    public enum View { Start, WorldSelect, StageSelect, Option };
    private View currentView;
    public static MainManager instance;


    private void Awake()
    {
        //Screen.SetResolution(540, 960, true);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        SetInitInfo();
    }

    /// <summary>
    /// controller들 설정해줌
    /// </summary>
    private void SetInitInfo()
    {
        startCon = GameObject.Find("Start").GetComponent<ViewController>();
        worldSelectCon = GameObject.Find("WorldSelect").GetComponent<ViewController>();
        stageSelectCon = GameObject.Find("StageSelect").GetComponent<ViewController>();
        optionCon = GameObject.Find("Option").GetComponent<ViewController>();
        backButton = GameObject.Find("BackButton");
    }

    private void Start()
    {
        switch (HandOverData.mainView)
        {
            case View.Start:
                GoToStart();
                break;
            case View.WorldSelect:
                GoToWorldSelect();
                break;
            case View.StageSelect:
                int worldIndex = (HandOverData.WorldIndex == -1) ? PlayerPrefs.GetInt(PrefsKey.lastWorld) : HandOverData.WorldIndex;
                GoToStageSelect(worldIndex);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToBack();
        }
    }

    /// <summary>
    /// 인게임 스테이지로 이동
    /// </summary>
    /// <param name="stageNum"></param>
    public void GoToStage(int worldIndex, int stageIndex)
    {
        HandOverData.StageName = StageList.GetStageName(worldIndex, stageIndex);
        HandOverData.StageIndex = stageIndex;
        HandOverData.WorldIndex = worldIndex;
        HandOverData.ShowStageInfo = true;
        if (worldIndex > PlayerPrefs.GetInt(PrefsKey.lastWorld))
        {
            PlayerPrefs.SetInt(PrefsKey.lastWorld, worldIndex);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.inGameScene);
    }

    /// <summary>
    /// 뒤로가기 기능
    /// </summary>
    public void GoToBack()
    {
        switch (currentView)
        {
            case View.Start:
                Application.Quit();
                break;
            case View.WorldSelect:
                GoToStart();
                break;
            case View.StageSelect:
                GoToWorldSelect();
                break;
            case View.Option:
                GoToOption();
                break;
            default:
                Debug.Log("메뉴 이동 예외");
                break;
        }
    }

    /// <summary>
    /// 해당 view로 이동
    /// </summary>
    /// <param name="view"></param>
    public void SetCurrentDat(View view, ViewController currentCon)
    {
        //현재 켜져있는거 닫음
        if (this.currentCon)
        {
            this.currentCon.Close();
        }
        //현재 켜져있는거 바꿈
        currentView = view;
        this.currentCon = currentCon;
    }

    public void GoToStageSelect(int worldIndex)
    {
        SetCurrentDat(View.StageSelect, stageSelectCon);
        ((StageSelectController)stageSelectCon).Open(worldIndex);
    }

    public void GoToStart()
    {

        SetCurrentDat(View.Start, startCon);
        startCon.Open();
        backButton.SetActive(false);
    }

    public void GoToWorldSelect()
    {
        SetCurrentDat(View.WorldSelect, worldSelectCon);
        worldSelectCon.Open();
        backButton.SetActive(true);
    }

    public void GoToOption()
    {
        SetCurrentDat(View.Option, optionCon);
        optionCon.Open();
    }
}
