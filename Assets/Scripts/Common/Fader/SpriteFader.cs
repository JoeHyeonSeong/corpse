using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader :Fader{
    private SpriteRenderer mySpriteRend;

    private void Awake()
    {
        mySpriteRend = GetComponent<SpriteRenderer>();    
    }

    protected override float GetNum()
    {
        return mySpriteRend.color.a;
    }

    public override void Opaque(float time)
    {
        base.Opaque(time);
    }

    protected override void SetNum(float newNum)
    {
        Color color = mySpriteRend.color;
        mySpriteRend.color = new Color(color.r,color.g,color.b, newNum);
    }

    protected override void TransparentFinish()
    {
        //do nothing
    }
    protected override void OpaqueFinish()
    {
        //do nothing
    }
}
