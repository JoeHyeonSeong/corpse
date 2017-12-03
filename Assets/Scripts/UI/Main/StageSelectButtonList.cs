using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectButtonList:MonoBehaviour {
    [SerializeField]
    private StageSelectButton[] stageSelectButtons;


    public void ListStages(int worldIndex)
    {
        for (int i = 0; i < stageSelectButtons.Length; i++)
        {
            stageSelectButtons[i].SetInit(0,i);
        }
    }
}
