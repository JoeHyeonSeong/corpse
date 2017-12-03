using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamCtrl : CamCtrl {
    public static MainCamCtrl instance;

    override protected void Awake()
    {
        base.Awake();
        instance = this;
    }
}
