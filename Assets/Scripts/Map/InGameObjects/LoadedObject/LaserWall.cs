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
    }


    public override void Activate()
    {
        base.Activate();
        if (myLaser == null)
        {
            myLaser = Instantiate<Laser>(Resources.Load<Laser>("Prefab/InGameObject/Laser")
            , transform.position, Quaternion.identity, this.transform.parent);
            myLaser.Dir = this.dir;
        }
        else
        {
            myLaser.gameObject.SetActive(true);
        }
        myLaser.Resize();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (myLaser != null)
        {

            myLaser.gameObject.SetActive(false);
       }
    }
}
