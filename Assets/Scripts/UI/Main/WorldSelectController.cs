using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class WorldSelectController : ViewController{
    private void Start()
    {
        ShowLastWorld();
    }

    public override void Open()
    {
        base.Open();
        SetWorldButtons();
    }

    private void SetWorldButtons()
    {
        transform.Find("WorldSelectList/Viewport/Content/World1").GetComponent<Button>().onClick.
            AddListener(() => MainManager.instance.GoToStageSelect(0));
        transform.Find("WorldSelectList/Viewport/Content/World2").GetComponent<Button>().onClick.
            AddListener(() => MainManager.instance.GoToStageSelect(1));
        transform.Find("WorldSelectList/Viewport/Content/World3").GetComponent<Button>().onClick.
            AddListener(() => MainManager.instance.GoToStageSelect(2));
    }

    private void ShowLastWorld()
    {
        int currentWorld =(HandOverData.WorldIndex==-1)? PlayerPrefs.GetInt(PrefsKey.lastWorld):HandOverData.WorldIndex;
        StartCoroutine(MoveNext(currentWorld));
    }


    IEnumerator MoveNext(int num)
    {
        for (int i = 0; i < num; i++)
        {
            transform.Find("WorldSelectList").GetComponent<HorizontalScrollSnap>().NextScreen();
            yield return new WaitForSeconds(0.3f);
        }
    }
}

