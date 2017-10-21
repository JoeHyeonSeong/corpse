using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader :Transparencer{
    private SpriteRenderer mySpriteRend;

    private void Awake()
    {
        mySpriteRend = GetComponent<SpriteRenderer>();    
    }

    protected override Color GetOriginalColor()
    {
        return mySpriteRend.color;
    }

    protected override void SetColor(Color newColor)
    {
        mySpriteRend.color = newColor;
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
