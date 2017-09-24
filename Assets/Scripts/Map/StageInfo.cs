using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    [SerializeField]
    private int life;
    public int Life { set { life = value; } get { return life; } }
    [SerializeField]
    private string title;
    public string Title { set { title = value; } }
    public static StageInfo instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (HandOverData.ShowStageInfo&&MapManager.instance!=null)
        {
            StageInfoManager.instance.ShowInfo(title, life);
        }
    }
}
