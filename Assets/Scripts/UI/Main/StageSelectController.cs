using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : ViewController {
    private int currentWorld;
    StageSelectButtonList buttonList;

    private void Awake()
    {
        buttonList = transform.Find("StageButtonList/Viewport/Content").GetComponent<StageSelectButtonList>();
    }

    public void Open(int worldIndex)
    {
        base.Open();
        buttonList.ListStages(worldIndex);
    }
}
