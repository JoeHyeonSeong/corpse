using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {
    private bool isPaused=false;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        if (isPaused)
        {
            InGameManager.instance.PauseEnd();
            isPaused = false;
        }
        else
        {
            InGameManager.instance.Pause();
            isPaused = true;
        }
    }
}
