using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : Wall
{
    protected Laser myLaser;

    [SerializeField]
    protected Dir4 dir;
    public Dir4 Dir
    {
        get { return dir; }
    }

    SpriteFader onSign;

    protected override void Awake()
    {
        base.Awake();
        onSign = transform.Find("laserWall_on_sign").GetComponent<SpriteFader>();
    }

    protected override void Start()
    {
        base.Start();
        if (InGameManager.IsInGameScene())
        {
            MakeLaser();
        }
    }

    protected override void Activate()
    {
        base.Activate();
        onSign.Opaque(0.2f);
        MakeLaser();


        myLaser.gameObject.SetActive(true);
        myLaser.Resize();
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        if (myLaser != null)
        {
            onSign.Transparent(0.2f);
            myLaser.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// set myLaser if it is null
    /// </summary>
    protected void MakeLaser()
    {
        if (myLaser == null)
        {
            myLaser = Instantiate<Laser>(Resources.Load<Laser>("Prefab/InGameObject/Laser")
   , transform.position, Quaternion.identity, this.transform.parent);
            myLaser.Dir = this.dir;
            myLaser.gameObject.SetActive(false);
        }
    }
}
