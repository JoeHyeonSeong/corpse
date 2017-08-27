using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MoveButton :MonoBehaviour,  IPointerUpHandler, IPointerDownHandler
{
    Vector2 pressStartPos;
    const float dragThreshold = 100f;

    public void OnPointerUp(PointerEventData data)
    {
        
        Vector2 resultVec = data.position - pressStartPos;
        if (resultVec.magnitude > dragThreshold)
        {
            if (Mathf.Abs(resultVec.x) > Mathf.Abs(resultVec.y))
            {
                if (resultVec.x > 0)
                {
                    Character.instance.Move(Character.instance.CurrentPos+Direction.Dir4ToPos(Dir4.Right));
                }
                else
                {
                    Character.instance.Move(Character.instance.CurrentPos + Direction.Dir4ToPos(Dir4.Left));
                }
            }
            else
            {
                if (resultVec.y > 0)
                {
                    Character.instance.Move(Character.instance.CurrentPos + Direction.Dir4ToPos(Dir4.Up));
                }
                else
                {
                    Character.instance.Move(Character.instance.CurrentPos + Direction.Dir4ToPos(Dir4.Down));
                }
            }
            
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        pressStartPos = data.pressPosition;
    }
}
