using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakedWall:Wall {
    AudioSource riseSound;
    protected override void Awake()
    {
        base.Awake();
        riseSound = transform.Find("StoneRisingSound").GetComponent<AudioSource>();
    }

    public void Rise()
    {
        riseSound.Play();
        mygraphic.GetComponent<Animator>().Play("Rise");
        GetComponent<BoxCollider2D>().enabled = true;
        MapManager.instance.ResizeSideLasers(currentPos);
    }

    public void Sink()
    {
        mygraphic.GetComponent<Animator>().Play("Sink");
        GetComponent<BoxCollider2D>().enabled = false;
        MapManager.instance.ResizeSideLasers(currentPos);
    }
}
