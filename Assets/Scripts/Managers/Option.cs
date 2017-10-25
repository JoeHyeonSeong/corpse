using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Option
{
    static public bool IsMusicOn()
    {
        return PlayerPrefs.GetInt(PrefsKey.musicOn) != 0;
    }

    static public bool IsSoundEffectOn()
    {
        return PlayerPrefs.GetInt(PrefsKey.soundEffectOn) != 0;
    }

    static public void SetMusicStatus(bool on)
    {
        int status;
        if (on)
        {
            status = 1;
        }
        else
        {
            status = 0;
        }
        PlayerPrefs.SetInt(PrefsKey.musicOn, status);
    }

    static public void SetSoundEffectStatus(bool on)
    {
        int status;
        if (on)
        {
            status = 1;
        }
        else
        {
            status = 0;
        }
        PlayerPrefs.SetInt(PrefsKey.soundEffectOn, status);
    }
}
