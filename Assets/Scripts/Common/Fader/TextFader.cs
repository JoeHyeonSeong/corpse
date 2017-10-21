using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextFader : Transparencer {
    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }

    public void SetText(string text)
    {
        myText.text = text;
    }

    public override void Transparent(float time)
    {
        base.Transparent(time);
    }

    public override void Opaque(float time)
    {
        base.Opaque(time);
    }

    protected override Color GetOriginalColor()
    {
        return myText.color;
    }

    protected override void SetColor(Color newColor)
    {
        myText.color = newColor;
    }

    protected override void OpaqueFinish()
    {
        //do nothing
    }

    protected override void TransparentFinish()
    {
        //do nothing
    }
}
