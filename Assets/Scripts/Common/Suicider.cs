using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicider : MonoBehaviour
{
    [SerializeField]
    private float timer;
    // Use this for initialization
    void Start()
    {
        Invoke("Destroy",timer);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
