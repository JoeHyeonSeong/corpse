using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transparencer : MonoBehaviour {

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
        Color originCol = GetOriginalColor();
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
            SetColor(new Color(originCol.r, originCol.g, originCol.b, alpha));
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

    protected abstract Color GetOriginalColor();

    protected abstract void SetColor(Color newColor);

    protected abstract void TransparentFinish();

    protected abstract void OpaqueFinish();
}
