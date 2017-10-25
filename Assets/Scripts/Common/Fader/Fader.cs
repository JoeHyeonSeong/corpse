using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fader : MonoBehaviour {

    public virtual void Transparent(float time)
    {
        StartCoroutine(AlphaChange(time, false));
    }

    public virtual void Opaque(float time)
    {
        StartCoroutine(AlphaChange(time, true));
    }

    private IEnumerator AlphaChange(float time, bool plus)
    {
        float delta = (1 / time) * Time.deltaTime;
        float alpha = GetNum();
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
            SetNum(alpha);
            yield return new WaitForEndOfFrame();
        }
        if (plus)
        {
            OpaqueFinish();
        }
        else
        {
            TransparentFinish();
        }
    }

    protected abstract float GetNum();

    protected abstract void SetNum(float newColor);

    protected virtual void TransparentFinish(){ }

    protected virtual void OpaqueFinish() { }
}
