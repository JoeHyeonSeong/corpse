using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Option
{
    //0: true
    //1 false
    static public bool IsMusicOn()
    {
        return PlayerPrefs.GetInt(PrefsKey.musicOn) != 1;
    }

    static public bool IsSoundEffectOn()
    {
        return PlayerPrefs.GetInt(PrefsKey.soundEffectOn) != 1;
    }

    static public void SetMusicStatus(bool on)
    {
        int status;
        if (on)
        {
            status = 0;
        }
        else
        {
            status = 1;
        }
        PlayerPrefs.SetInt(PrefsKey.musicOn, status);
    }

    static public void SetSoundEffectStatus(bool on)
    {
        int status;
        if (on)
        {
            status = 0;
        }
        else
        {
            status = 1;
        }
        PlayerPrefs.SetInt(PrefsKey.soundEffectOn, status);
    }
}
