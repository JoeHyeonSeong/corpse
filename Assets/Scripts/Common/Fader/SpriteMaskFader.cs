using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskFader : Fader {
    public override void Opaque(float time)
    {
        base.Transparent(time);
    }

    public override void Transparent(float time)
    {
        base.Opaque(time);
    }
    protected override float GetNum()
    {
        return GetComponent<SpriteMask>().alphaCutoff;
    }

    protected override void SetNum(float newNum)
    {
        GetComponent<SpriteMask>().alphaCutoff = newNum;
    }
}
