using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
    SpriteFader myLight;
    static public BackGround instance;
    private void Awake()
    {
        myLight = transform.Find("Light").GetComponent<SpriteFader>();
        instance = this;
    }

    public void LightOn()
    {
        myLight.Opaque(1);
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }
}
