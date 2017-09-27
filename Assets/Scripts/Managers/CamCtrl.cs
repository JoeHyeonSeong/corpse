using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour {
    public static CamCtrl instance;

    private float maxInterval = 0.1f;
    private float movTime = 0.4f;
    private const int zPos = -10;
    private Vector3 targetPos;
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
        Vector3 charPos= Character.instance.CurrentPos.ToVector3();
        charPos.z =zPos;
        if ((charPos - transform.position).magnitude > maxInterval)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, charPos, ref moveVel, movTime);
        }
        

    }
}
