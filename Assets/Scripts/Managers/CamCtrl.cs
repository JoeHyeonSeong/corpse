using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour {
    public static CamCtrl instance;

    private float maxInterval = 3;
    private float movTime = 0.4f;
    private const int zPos = -10;

    private void Awake()
    {
        instance = this;
    }


    public void SetPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, zPos);
    }

    private void FixedUpdate()
    {
        CamMovRoutine();
    }


    private void CamMovRoutine()
    {
        //cam moving routine
        Vector3 moveVel=Vector3.zero;
        Vector3 targetPos= Character.instance.CurrentPos.ToVector3();
        targetPos.z =zPos;
        if ((targetPos - transform.position).magnitude > maxInterval)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref moveVel, movTime);
        }
        

    }
}
