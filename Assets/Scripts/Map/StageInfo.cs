using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour {
    [SerializeField]
    private int life;
    [SerializeField]
    private string title;
    private void Awake()
    {
        if (HandOverData.ShowStageInfo)
        {
            StageInfoManager.instance.ShowInfo(title, life);
        }
    }

    private void Start()
    {
        Character.instance.Life = life;   
    }
}
