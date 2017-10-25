using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioFader : Fader{
    protected AudioSource mySrc;

    private void Awake()
    {
        mySrc = GetComponent<AudioSource>();
    }


    protected override float GetNum()
    {
        return mySrc.volume;
    }

    protected override void SetNum(float newNum)
    {
        mySrc.volume = newNum;
    }

    public virtual void Play()
    {
        mySrc.Play();
    }
}
