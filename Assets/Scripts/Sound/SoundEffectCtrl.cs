using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectCtrl : AudioFader
{

    public override void Play()
    {
        if (Option.IsSoundEffectOn())
        {
            base.Play();
        }
    }
}
