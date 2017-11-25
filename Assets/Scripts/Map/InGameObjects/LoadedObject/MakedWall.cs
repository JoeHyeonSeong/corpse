using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakedWall:Wall {
    SoundEffectCtrl riseSound;
    WallMakeFloor myFloor;

    public enum Status { rise, sink };
    private Status mystatus=Status.rise;
    public Status RiseStatus { get { return mystatus; } }

    public WallMakeFloor MyFloor { set { myFloor = value; } }
    protected override void Awake()
    {
        base.Awake();
        riseSound = transform.Find("StoneRisingSound").GetComponent<SoundEffectCtrl>();
    }

    public void Rise()
    {
        mystatus = Status.rise;
        riseSound.Play();
        mygraphic.GetComponent<Animator>().Play("Rise");
        GetComponent<BoxCollider2D>().enabled = true;
        MapManager.instance.ResizeSideLasers(currentPos);
    }

    public void Sink()
    {
        mystatus = Status.sink;
        mygraphic.GetComponent<Animator>().Play("Sink");
        GetComponent<BoxCollider2D>().enabled = false;
        MapManager.instance.ResizeSideLasers(currentPos);
    }

    public override void ShowLinks()
    {
        myFloor.ShowLinks();
    }
}
