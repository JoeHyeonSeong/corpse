using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageInfoManager : MonoBehaviour {
    public static StageInfoManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void ShowInfo(string title, int life)
    {
        transform.Find("Title").GetComponent<Text>().text = title;
        transform.Find("Life").GetComponent<Text>().text = life.ToString();
        StartCoroutine(Diminisher(3));
    }

    IEnumerator Diminisher(float time)
    {
        float minusDelta = (1 / time)*Time.deltaTime;
        float alpha = 1;
        Color TitleColor = transform.Find("Title").GetComponent<Text>().color;
        Color LifeColor = transform.Find("Life").GetComponent<Text>().color;
        while (alpha > 0)
        {
            alpha -= minusDelta;
            transform.Find("Life").GetComponent<Text>().color =
                new Color(LifeColor.r, LifeColor.g, LifeColor.b, alpha);
            transform.Find("Title").GetComponent<Text>().color =
                new Color(TitleColor.r, TitleColor.g, TitleColor.b, alpha);
            yield return null;
        }
    }
}
