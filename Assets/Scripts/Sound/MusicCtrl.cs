using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCtrl : AudioFader {
    public override void Play()
    {
        if (Option.IsMusicOn())
        {
            base.Play();
        }
    }
}
