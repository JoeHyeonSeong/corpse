using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour {
    public static CamCtrl instance;
    private const int zVal = -10;

    private void Awake()
    {
        instance = this;
    }


    public void SetPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, zVal);
    }
}
