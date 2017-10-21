using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageFader : Transparencer
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

    protected override Color GetOriginalColor()
    {
        return GetComponent<Image>().color;
    }

    protected override void SetColor(Color newColor)
    {
        GetComponent<Image>().color = newColor;
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
