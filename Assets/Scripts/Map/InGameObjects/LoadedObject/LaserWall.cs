using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : Wall{
    protected Laser myLaser;
    [SerializeField]
    protected Dir4 dir;
    public Dir4 Dir
    {
        get { return dir; }
        set { dir = value; }
    }
    protected override void Awake()
    {
        base.Awake();
    }


    public override void Activate()
    {
        base.Activate();
        myLaser = Instantiate<Laser>(Resources.Load<Laser>("Prefab/InGameObject/Laser")
            ,transform.position,Quaternion.identity,this.transform.parent);
        myLaser.Dir = this.dir;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (myLaser != null)
        {

            Destroy(myLaser.gameObject);
        }
    }
}
