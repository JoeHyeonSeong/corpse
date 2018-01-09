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


                movTime = Mathf.Clamp(expectMovTime, minMoveTime, maxMoveTime);

            //calculate Position
            Vector3 expectPos = Vector3.SmoothDamp(this.transform.position, targetPos, ref moveVel, movTime);


                movTime = zoomMoveTime;
                expectPos.x = Mathf.Clamp(expectPos.x, minX, maxX);
                expectPos.y = Mathf.Clamp(expectPos.y, minY, maxY);
                transform.position = Vector3.Lerp(transform.position, expectPos, movTime);
        }

    }

    public void SetThresholdPos(float mapMaxX, float mapMaxY, float mapMinX, float mapMinY)
    {

        float xOrtho = ((mapMaxX - mapMinX+1+sideInterval*2) * myCam.pixelHeight / myCam.pixelWidth)/2;
        float yOrtho = (mapMaxY - mapMinY+1+sideInterval*2)/2;
        myCam.orthographicSize = Mathf.Max(xOrtho, yOrtho);

        float ySize = myCam.orthographicSize;
        float xSize = ySize * myCam.pixelWidth / myCam.pixelHeight;
        maxX = mapMinX+xSize-sideInterval;
        maxY = mapMinY+ySize-sideInterval;
        minX = mapMaxX-xSize+sideInterval;
        minY = mapMaxY-ySize+sideInterval;
    }

}
