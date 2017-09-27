using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextFader : Text {
    public void SetText(string text)
    {
        this.text = text;
    }

    public void FadeIn(float time)
    {
        StartCoroutine(AlphaChange(time, true));
    }

    public void FadeOut(float time)
    {
        StartCoroutine(AlphaChange(time, false));
    }

    private IEnumerator AlphaChange(float time, bool plus)
    {
        float delta = (1 / time) * Time.deltaTime;
        Color originCol = color;
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
            color =new Color(originCol.r, originCol.g, originCol.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}
