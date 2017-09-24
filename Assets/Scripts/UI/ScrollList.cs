using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollList : MonoBehaviour
{
    [SerializeField]
    protected GameObject contentpref;
    protected List<GameObject> contentList = new List<GameObject>();
    const float maxSpace = 10;
    const float movTime = 0.4f;
    const float ySpace = 100;

    protected void InsertContent(bool AtFront,GameObject content)
    {
        contentList.Add(content);
        int index = contentList.Count - 1;
        float contentHeight = contentpref.GetComponent<RectTransform>().rect.height;

        Vector3 coor;
        if (AtFront)
        {
            coor = new Vector3(0, -0.5f * (contentHeight+ySpace), 0);
            for (int i = 0; i < contentList.Count - 1; i++)
            {
                contentList[i].GetComponent<RectTransform>().anchoredPosition += contentHeight * Vector2.down;
            }
        }
        else coor = CalPosition(index);
        content.transform.localScale = new Vector3(1, 1, 1);
        content.GetComponent<RectTransform>().anchoredPosition = coor;
        SetListHeight();
    }

    protected void SetListHeight()
    {
        float contentHeight = contentpref.GetComponent<RectTransform>().rect.height;
        RectTransform myRect = GetComponent<RectTransform>();
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, contentList.Count * (contentHeight+ySpace));
    }

    public void ShowAt(int index)
    {
        StartCoroutine(MoveTo(CalPosition(index)));
    }

    protected Vector3 CalPosition(int index)
    {
        float contentHeight = contentpref.GetComponent<RectTransform>().rect.height;
        return new Vector3(0, (-0.5f - index) * (contentHeight + ySpace), 0);
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