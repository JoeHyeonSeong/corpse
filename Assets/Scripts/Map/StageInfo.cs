using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour {
    [SerializeField]
    private int life=10;
    public int Life {  set { life = value; } get { return life; } }
    [SerializeField]
    private string title;
    public string Title { set { title = value; } }
    public static StageInfo instance;
    private void Awake()
    {
        if (HandOverData.ShowStageInfo)
        {
            if (MapManager.instance != null)
            {
                instance = this;
                StageInfoManager.instance.ShowInfo(title, life);
            }
        }
    }
}
