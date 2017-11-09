using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TouchButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler,IDragHandler
{
    Vector2 pressStartPos;

    const float dragThreshold = 50f;

    private bool alreadyMove;
    private bool showInfo = false;//기다리는동안 false가 안된다면 클릭했던것의 정보 보여줌

    public void OnPointerUp(PointerEventData data)
    {
        if (alreadyMove)
        {
            alreadyMove = false;
        }
        showInfo = false;
    }

    public void OnDrag(PointerEventData data)
    {
        showInfo = false;//움직였으니까 안보여줌
        if (Scheduler.instance.CurrentCycle != Scheduler.GameCycle.InputTime||alreadyMove)
        {
            //deny input
            return;
        }

        Position result;
        Vector2 resultVec = data.position - pressStartPos;
        if (resultVec.magnitude > dragThreshold)
        {
            if (Mathf.Abs(resultVec.x) > Mathf.Abs(resultVec.y))
            {
                if (resultVec.x > 0)
                {
                    result = Direction.Dir4ToPos(Dir4.Right);
                }
                else
                {
                    result = Direction.Dir4ToPos(Dir4.Left);
                }
            }
            else
            {
                if (resultVec.y > 0)
                {
                    result = Direction.Dir4ToPos(Dir4.Up);
                }
                else
                {
                    result = Direction.Dir4ToPos(Dir4.Down);
                }
            }
            Move(result);
            alreadyMove = true;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        pressStartPos = data.pressPosition;
        StartCoroutine(WaitAndShowInfo(0.2f));
    }

    private void Update()
    {
        if (Scheduler.instance.CurrentCycle == Scheduler.GameCycle.InputTime)
        {
            Position pos = null;
            if (Input.GetKeyDown(KeyCode.W)) pos = Direction.Dir4ToPos(Dir4.Up);
            if (Input.GetKeyDown(KeyCode.S)) pos = Direction.Dir4ToPos(Dir4.Down);
            if (Input.GetKeyDown(KeyCode.A)) pos = Direction.Dir4ToPos(Dir4.Left);
            if (Input.GetKeyDown(KeyCode.D)) pos = Direction.Dir4ToPos(Dir4.Right);
            if (pos != null)
            {
                Move(pos);
            }
        }

    }

    protected void Move(Position dir)
    {
        Position initCharPos = (Position)Character.instance.CurrentPos;
        InGameManager.instance.NewPhase();
        List<InGameObject> characterBlockData = MapManager.instance.BlockData(Character.instance.CurrentPos);
        bool ice = false;
        foreach (InGameObject obj in characterBlockData)
        {
            if (obj.GetType() == typeof(Ice))
            {
                ice = true;
            }
        }
        if (ice)
        {
            Character.instance.Slide(Character.instance.CurrentPos + dir, true);
        }
        else
        {
            Character.instance.Move(Character.instance.CurrentPos + dir, true);
        }
        //move success
        if (initCharPos == Character.instance.CurrentPos)
        {
            InGameManager.instance.RollBack();
        }
    }

    IEnumerator WaitAndShowInfo(float waitTime)
    {
        showInfo = true;
        yield return new WaitForSeconds(waitTime);
        //show info
       while(showInfo)
        {
            RaycastHit2D startTouch = GettouchOne(pressStartPos);
            if (startTouch)
            {
                startTouch.transform.parent.GetComponent<InGameObject>().ShowLinks();
            }
            yield return 0;
        }
    }

    RaycastHit2D GettouchOne(Vector3 screenPos)
    {
        Vector3 startPoint = Camera.main.ScreenToWorldPoint(pressStartPos);
        RaycastHit2D hit =
        Physics2D.Raycast(startPoint,
        Vector3.back, 1,
        1 << LayerMask.NameToLayer("Info"));
        return hit;
    }
}

