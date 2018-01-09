using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : ViewController {
    private Toggle musicToggle;
    private Toggle soundEffectToggle;
    private Button creditsButton;
    private void Awake()
    {
        InitSetting();
    }

    private void InitSetting()
    {
        creditsButton = transform.Find("CreditsButton").GetComponent<Button>();
        creditsButton.onClick.AddListener(() => MainManager.instance.GoToCredits());

        musicToggle = transform.Find("MusicToggle").GetComponent<Toggle>();
        musicToggle.onValueChanged.AddListener(Option.SetMusicStatus);
        musicToggle.isOn = Option.IsMusicOn();

        soundEffectToggle = transform.Find("SoundEffectToggle").GetComponent<Toggle>();
        soundEffectToggle.onValueChanged.AddListener(Option.SetSoundEffectStatus);
        soundEffectToggle.isOn = Option.IsSoundEffectOn();
    }
}
