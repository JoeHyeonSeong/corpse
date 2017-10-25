using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageFader : Fader
{
    private void SetPosition()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void BlackOut()
    {
        SetPosition();
        Color originCol = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(originCol.r,originCol.g,originCol.b,1);
        GetComponent<Image>().raycastTarget = true;
    }

    public override void Transparent(float time)
    {
        SetPosition();
        base.Transparent(time);
    }

    public override void Opaque(float time)
    {
        SetPosition();
        base.Opaque(time);
    }

    protected override float GetNum()
    {
        return GetComponent<Image>().color.a;
    }

    protected override void SetNum(float newNum)
    {
        Color myColor = GetComponent<Image>().color;
        GetComponent<Image>().color =new Color(myColor.r,myColor.g,myColor.b, newNum);
    }

    protected override void TransparentFinish()
    {
        GetComponent<Image>().raycastTarget = false;
    }

    protected override void OpaqueFinish()
    {
        GetComponent<Image>().raycastTarget = true;
    }
}
