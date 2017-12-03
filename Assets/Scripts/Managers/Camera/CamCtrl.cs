using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour {
#region
    protected const float maxCamSize = 10f;
    protected const float minCamSize= 5f;
    protected const float maxMoveTime = 0.15f;
    protected const float minMoveTime = 0.1f;
    protected const float zoomMoveTime=0.1f;
    protected const float maxInterval = 0.1f;
    protected const float sideInterval = 0.6f;
    protected const int zPos = -10;
    protected readonly Vector3 camPosAdjust = Vector3.zero;
#endregion// constants



    protected bool isZoomIn;

    protected Camera myCam;

    protected Vector3 targetPos;
    
    public Vector3 TargetPos
    {
        set
        {
            targetPos = value;
            targetPos.z = zPos;
        }
    }
    
    protected virtual void Awake()
    {
        myCam = GetComponent<Camera>();
        myCam.orthographicSize = maxCamSize;
        targetPos = transform.position;
    }


    public void SetPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, zPos);
    }

    public void GoToOriginalSize()
    {
        myCam.orthographicSize = maxCamSize;
    }

    public void Zoom(float time, bool zoomIn)
    {
        if (zoomIn)
        {
            isZoomIn = true;
        }
        else
        {
            isZoomIn = false;
        }
        StartCoroutine(ZoomCoroutine(time, zoomIn));
    }

    private IEnumerator ZoomCoroutine(float time, bool zoomIn)
    {
        float deltaSize = (maxCamSize - minCamSize) / time*Time.deltaTime;
        float accTime=0;
        while (accTime < time)
        {
            if (zoomIn)
            {
                myCam.orthographicSize -= deltaSize;
            }
            else
            {
                myCam.orthographicSize +=deltaSize;
            }
            accTime += Time.deltaTime;
            yield return null;
        }
        if (zoomIn)
        {
            myCam.orthographicSize =minCamSize;
        }
        else
        {
            myCam.orthographicSize=maxCamSize;
        }
    }

    private void FixedUpdate()
    {
        CamMovRoutine();
    }

    protected virtual void CamMovRoutine()
    {
        //cam moving routine
        Vector3 moveVel=Vector3.zero;
        float diff = (targetPos - transform.position).magnitude;
        if (diff > maxInterval)
        {
            float expectMovTime = 0.5f/diff;
            //set movTime
            float movTime;
            if (isZoomIn)
            {
                movTime = zoomMoveTime;
            }
            else
            {
                movTime = Mathf.Clamp(expectMovTime, minMoveTime, maxMoveTime);
            }
            //calculate Position
            Vector3 expectPos = Vector3.SmoothDamp(this.transform.position, targetPos,ref moveVel,  movTime);
            if (isZoomIn)
            {
                transform.position = expectPos;
            }
            else
            {
                movTime = zoomMoveTime;
                transform.position = Vector3.Lerp(transform.position, expectPos, movTime);
            }
        }

    }
}
