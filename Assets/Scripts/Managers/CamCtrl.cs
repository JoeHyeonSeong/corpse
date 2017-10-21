using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour {
    private const float maxCamSize = 10f;
    private const float minCamSize= 7f;
    private const float maxMoveTime = 0.15f;
    private const float minMoveTime = 0.1f;
    private const float zoomMoveTime=0.1f;
    private const float maxInterval = 0.1f;
    private const float sideInterval = 0.6f;
    private const int zPos = -10;
    private readonly Vector3 camPosAdjust = Vector3.zero;
    public static CamCtrl instance;

    private float maxX;
    private float maxY;
    private float minX;
    private float minY;

    private bool isZoomIn;

    private Camera myCam;

    private Vector3 targetPos;
    public Vector3 TargetPos
    {
        set
        {
            targetPos = value;
            targetPos.z = zPos;
        }
    }

    private void Awake()
    {
        instance = this;
        myCam = GetComponent<Camera>();
        myCam.orthographicSize = maxCamSize;
    }

    public void SetThresholdPos(float mapMaxX, float mapMaxY, float mapMinX, float mapMinY)
    {
        float ySize = myCam.orthographicSize-sideInterval;
        float xSize = ySize * myCam.pixelWidth/myCam.pixelHeight-sideInterval;
        maxX = mapMinX + xSize;
        maxY = mapMinY + ySize;
        minX = mapMaxX - xSize;
        minY = mapMaxY - ySize;
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, zPos);
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

    private void CamMovRoutine()
    {
        //cam moving routine
        Vector3 moveVel=Vector3.zero;
        TargetPos = Character.instance.CurrentPos.ToVector3()+camPosAdjust;
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
                expectPos.x = Mathf.Clamp(expectPos.x, minX, maxX);
                expectPos.y = Mathf.Clamp(expectPos.y, minY, maxY);
                transform.position = Vector3.Lerp(transform.position, expectPos, movTime);
            }
        }

    }
}
