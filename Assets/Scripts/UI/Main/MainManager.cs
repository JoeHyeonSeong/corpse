using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private ViewController startCon;
    //private ViewController worldSelectCon;
    private ViewController stageSelectCon;
    private ViewController optionCon;
    private ViewController currentCon;
    private ViewController creditCon;

    private GameObject backButton;

    private bool isSceneChanging;//현재 씬이 넘어가는중

    public enum View { Start, StageSelect, Option,Credits };
    private View currentView;
    public static MainManager instance;


    private void Awake()
    {
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
        //worldSelectCon = GameObject.Find("WorldSelect").GetComponent<ViewController>();
        stageSelectCon = GameObject.Find("StageSelect").GetComponent<ViewController>();
        optionCon = GameObject.Find("Option").GetComponent<ViewController>();
        creditCon= GameObject.Find("Credits").GetComponent<ViewController>();
        backButton = GameObject.Find("BackButton");
    }

    private void Start()
    {
        switch (HandOverData.mainView)
        {
            case View.Start:
                GoToStart();
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
        if (isSceneChanging == false)
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
    }

    /// <summary>
    /// 뒤로가기 기능
    /// </summary>
    public void GoToBack()
    {
        if (isSceneChanging == false)
        {
            switch (currentView)
            {
                case View.Start:
                    Application.Quit();
                    break;
                case View.StageSelect:
                    GoToStart();
                    break;
                case View.Option:
                    GoToStart();
                    break;
                case View.Credits:
                    GoToStart();
                    break;
                default:
                    Debug.Log("메뉴 이동 예외");
                    break;
            }
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
        if (isSceneChanging == false)
        {
            SetCurrentDat(View.StageSelect, stageSelectCon);
            ((StageSelectController)stageSelectCon).Open(worldIndex);
            stageSelectCon.Open();
            backButton.SetActive(true);
        }
    }

    public void GoToStart()
    {
        if (isSceneChanging == false)
        {
            SetCurrentDat(View.Start, startCon);
            startCon.Open();
            backButton.SetActive(false);
        }
    }

    

    private IEnumerator FadeOut()
    {
        const float opaqueTime = 0.3f;
        startCon.transform.parent.Find("Fade").GetComponent<ImageFader>().Opaque(opaqueTime);
        yield return new WaitForSeconds(opaqueTime + 0.1f);
    }

    private IEnumerator FadeIn()
    {
        const float transparentTime = 0.3f;
        startCon.transform.parent.Find("Fade").GetComponent<ImageFader>().Transparent(transparentTime);
        isSceneChanging = false;
        yield return new WaitForSeconds(transparentTime + 0.1f);
    }
    /*
    public void GoToWorldSelect()
    {
        SetCurrentDat(View.WorldSelect, worldSelectCon);
        worldSelectCon.Open();
        backButton.SetActive(true);
    }
    */

    public void GoToOption()
    {
        SetCurrentDat(View.Option, optionCon);
        optionCon.Open();
        backButton.SetActive(true);
    }

    public void GoToCredits()
    {
        SetCurrentDat(View.Credits, creditCon);
        creditCon.Open();
        backButton.SetActive(true);
    }
}
