﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MoveButton :MonoBehaviour,  IPointerUpHandler, IPointerDownHandler
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

        Vector2 resultVec = data.position - pressStartPos;
        if (resultVec.magnitude > dragThreshold)
        {
            Position additionalPos;
            if (Mathf.Abs(resultVec.x) > Mathf.Abs(resultVec.y))
            {
                if (resultVec.x > 0)
                {
                    additionalPos = Direction.Dir4ToPos(Dir4.Right);
                }
                else
                {
                    additionalPos = Direction.Dir4ToPos(Dir4.Left);
                }
            }
            else
            {
                if (resultVec.y > 0)
                {
                    additionalPos = Direction.Dir4ToPos(Dir4.Up);
                }
                else
                {
                    additionalPos = Direction.Dir4ToPos(Dir4.Down);
                }
            }
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
                Character.instance.Slide(Character.instance.CurrentPos + additionalPos, true);
            }
            else
            {
                Character.instance.Move(Character.instance.CurrentPos + additionalPos, true, true);
            }
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        pressStartPos = data.pressPosition;
    }
}
