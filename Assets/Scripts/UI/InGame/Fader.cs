using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fader : MonoBehaviour
{
    public void FadeIn(float time)
    {
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        StartCoroutine(AlphaChange(time, false));
    }

    public void FadeOut(float time)
    {
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        StartCoroutine(AlphaChange(time, true));
    }

    private IEnumerator AlphaChange(float time, bool plus)
    {
        float delta = (1 / time) * Time.deltaTime;
        Color originCol = GetComponent<Image>().color;
        float alpha = originCol.a;
        while ((alpha > 0 && !plus) || (alpha < 1 && plus))
        {
            if (plus)
            {
                alpha += delta;
            }
            else
            {
                alpha -= delta;
            }
            GetComponent<Image>().color =
                new Color(originCol.r, originCol.g, originCol.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}
