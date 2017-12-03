using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamCtrl :CamCtrl {

    private float maxX;
    private float maxY;
    private float minX;
    private float minY;

    public static InGameCamCtrl instance;

    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }

    protected override void CamMovRoutine()
    {
        //cam moving routine
        Vector3 moveVel = Vector3.zero;
        TargetPos = Character.instance.CurrentPos.ToVector3() + camPosAdjust;
        float diff = (targetPos - transform.position).magnitude;
        if (diff > maxInterval)
        {
            float expectMovTime = 0.5f / diff;
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
            Vector3 expectPos = Vector3.SmoothDamp(this.transform.position, targetPos, ref moveVel, movTime);
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

    public void SetThresholdPos(float mapMaxX, float mapMaxY, float mapMinX, float mapMinY)
    {
        float ySize = myCam.orthographicSize - sideInterval;
        float xSize = ySize * myCam.pixelWidth / myCam.pixelHeight - sideInterval;
        maxX = mapMinX + xSize;
        maxY = mapMinY + ySize;
        minX = mapMaxX - xSize;
        minY = mapMaxY - ySize;
    }

}
