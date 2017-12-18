using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectCtrl : AudioFader
{
    [SerializeField]
    private bool playOnAwake = false;

    public override void Play()
    {
        if (Option.IsSoundEffectOn())
        {
            base.Play();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (Option.IsSoundEffectOn()&&playOnAwake)
        {
            base.Play();
        }
    }
}
