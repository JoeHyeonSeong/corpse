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

        Vector2 buttonLocalPos=Vector2.zero;
        for (int i = 0; i < stageSelectButtons.Length; i++)
        {
            if (StageList.IsUnLocked(0, i))//잠겨있음
            {
                buttonLocalPos = stageSelectButtons[i].GetComponent<RectTransform>().anchoredPosition;
            }
        }
        this.GetComponent<RectTransform>().anchoredPosition = -buttonLocalPos;
    }

}
