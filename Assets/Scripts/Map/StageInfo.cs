using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    [SerializeField]
    private int life;
    public int Life {get { return life; } }
    [SerializeField]
    private string title;
    public static StageInfo instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetInitInfo(int life, string title)
    {
        this.life = life;
        this.title = title;
        SetStageInfoUI();
    }

    private void SetStageInfoUI()
    {
        if (InGameManager.IsInGameScene())
        {
            GameObject.Find("Title").GetComponent<TextFader>().SetText(title);
            GameObject.Find("Chapter").GetComponent<TextFader>().
                SetText((InGameManager.instance.WorldIndex+1).ToString()+" - "+ (InGameManager.instance.StageIndex+1).ToString());
        }
    }
}
