using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollList : MonoBehaviour
{
    public GameObject contentpref;
    protected List<GameObject> contentList = new List<GameObject>();
    const float maxSpace = 10;
    const float movTime = 0.4f;
    const float ySpace = 0.5f;
    protected void InsertContent(bool AtFront,GameObject content)
    {
        GameObject obj = Instantiate(content, this.transform);
        contentList.Add(obj);
        int index = contentList.Count - 1;
        float contentHeight = contentpref.GetComponent<RectTransform>().rect.height;

        Vector3 coor;
        if (AtFront)
        {
            coor = new Vector3(0, -0.5f * contentHeight, 0);
            for (int i = 0; i < contentList.Count - 1; i++)
            {
                contentList[i].GetComponent<RectTransform>().localPosition += contentHeight * Vector3.down;
            }
        }
        else coor = CalPosition(index);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.GetComponent<RectTransform>().localPosition = coor;
    }

    protected void SetListHeight(int cnt)
    {
        float contentHeight = contentpref.GetComponent<RectTransform>().rect.height;
        RectTransform myRect = GetComponent<RectTransform>();
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, cnt * contentHeight);
    }

    public void ShowAt(int index)
    {
        StartCoroutine(MoveTo(CalPosition(index)));
    }

    protected Vector3 CalPosition(int index)
    {
        float contentHeight = contentpref.GetComponent<RectTransform>().rect.height;
        return new Vector3(0, (-0.5f - index + ySpace) * contentHeight, 0);
    }

    IEnumerator MoveTo(Vector3 position)
    {
        Vector3 speed=Vector3.zero;
        float initDifY = position.y- transform.position.y;
        while (true)
        {
            Vector3 myPos=transform.position;
            float currentDifY = position.y - myPos.y;
            if (initDifY*currentDifY<0)
            {
                //도착
                break;
            }
            transform.position = Vector3.SmoothDamp(transform.position, position, ref speed, movTime);
            yield return null;
        }
    }
}