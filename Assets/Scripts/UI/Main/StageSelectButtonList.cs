using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectButtonList : ScrollList {
    public void ListStages(int worldIndex)
    {
        for (int i = 0; i < StageList.GetStageNo(worldIndex); i++)
        {
            GameObject button=Instantiate(contentpref,transform);
            int stageNum = i;
            button.GetComponent<Button>().onClick.AddListener(() => MainManager.instance.GoToStage(stageNum));
            button.transform.Find("Title").GetComponent<Text>().text = StageLoader.GetData(StageList.GetStageName(worldIndex, i)).title;
            InsertContent(false, button);
        }
    }
}
