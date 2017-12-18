using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectButton :Button {
    private bool isUnLocked;
    private int worldIndex;
    private int stageIndex;

    public void SetInit(int worldIndex,int stageIndex)
    {
        this.worldIndex = worldIndex;
        this.stageIndex = stageIndex;
        isUnLocked = StageList.IsUnLocked(worldIndex,stageIndex);
        onClick.AddListener(()=>OnClick());
        transform.Find("Title").GetComponent<Text>().text = RomanNumber.Roman(stageIndex+1);
        if (isUnLocked)
        {
            transform.Find("LockImage").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("LockImage").gameObject.SetActive(true);
        }
    }

    private void OnClick()
    {
        if (isUnLocked)
        {
            MainManager.instance.GoToStage(worldIndex, stageIndex);
        }
    }
}
