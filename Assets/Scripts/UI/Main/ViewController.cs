using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {
    public virtual void Open()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public virtual void Close()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(2000, 2000);
    }
}
