using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectButtonList : ScrollList {
    public void ListStages(int worldIndex)
    {
        Clear();

        for (int i = 0; i < StageList.GetWorldSize(worldIndex); i++)
        {
            GameObject button=Instantiate(contentpref,transform);
            int stageIndex = i;
            button.GetComponent<StageSelectButton>().SetInit(worldIndex, stageIndex);
            button.transform.Find("Title").GetComponent<Text>().text = StageLoader.GetData(StageList.GetStageName(worldIndex, stageIndex)).title;
            InsertContent(false, button);
        }
    }

    protected void Clear()
    {
        contentList.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
