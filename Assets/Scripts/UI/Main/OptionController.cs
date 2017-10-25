using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : ViewController {
    private Toggle musicToggle;
    private Toggle soundEffectToggle;

    private void Awake()
    {
        InitSetting();
    }

    private void InitSetting()
    {
        musicToggle = transform.Find("MusicToggle").GetComponent<Toggle>();
        musicToggle.onValueChanged.AddListener(Option.SetMusicStatus);
        musicToggle.isOn = Option.IsMusicOn();

        soundEffectToggle = transform.Find("SoundEffectToggle").GetComponent<Toggle>();
        soundEffectToggle.onValueChanged.AddListener(Option.SetSoundEffectStatus);
        soundEffectToggle.isOn = Option.IsSoundEffectOn();
    }
}
