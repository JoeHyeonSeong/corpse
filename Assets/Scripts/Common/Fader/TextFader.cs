using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextFader : Fader {
    Text myText;

    private void Awake()
    {
        myText = GetComponent<Text>();
    }

    public void SetText(string text)
    {
        myText.text = text;
    }

    protected override float GetNum()
    {
        return myText.color.a;
    }

    protected override void SetNum(float newNum)
    {
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, newNum);
    }
}
