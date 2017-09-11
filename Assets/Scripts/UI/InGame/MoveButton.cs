using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MoveButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Vector2 pressStartPos;
    const float dragThreshold = 10f;

    public void OnPointerUp(PointerEventData data)
    {
        if (Scheduler.instance.CurrentCycle != Scheduler.GameCycle.InputTime)
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
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        pressStartPos = data.pressPosition;
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

    }
}

