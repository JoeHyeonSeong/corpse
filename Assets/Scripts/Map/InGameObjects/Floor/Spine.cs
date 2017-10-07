using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : Floor {
    static Sprite spineOn;
    static Sprite spineOff;
    protected override void Awake()
    {
        base.Awake();
        stepPriority = 3;
        if (spineOn == null)
        {
            spineOn = Resources.Load<Sprite>("Graphic/InGameObject/spineOn");
            spineOff = Resources.Load<Sprite>("Graphic/InGameObject/spineOff");
        }
    }


    public override void Step(MovableObject who)
    {
        if (currentStatus == ActiveStatus.activating)
        {
            if (who.GetType().IsSubclassOf(typeof(DestroyableObject)))
                ((DestroyableObject)who).Destroy();
        }
    }

    protected override void Activate()
    {
        base.Activate();
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = spineOn;
        if (currentPos == null)
        {
            Teleport(new Position((int)transform.position.x, (int)transform.position.y));
        }
        DestroyableObject desObj = (DestroyableObject)MapManager.instance.Find(typeof(DestroyableObject), currentPos);
        if (desObj != null)
        {
            desObj.Destroy();
        }
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = spineOff;
    }
}
