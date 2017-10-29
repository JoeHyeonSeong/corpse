using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartController :ViewController {
    private void Awake()
    {
        InitSetting();   
    }

    private void InitSetting()
    {
        transform.Find("WorldButton").GetComponent<Button>().onClick.
            AddListener(() => MainManager.instance.GoToWorldSelect());
        transform.Find("OptionButton").GetComponent<Button>().onClick.
            AddListener(() => MainManager.instance.GoToOption());
        transform.Find("ResetButton").GetComponent<Button>().onClick.
            AddListener(() => PlayerPrefs.DeleteAll());
    }
}
