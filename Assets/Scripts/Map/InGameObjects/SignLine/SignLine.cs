using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SignLine :InGameObject{

  protected Sprite sprite_on;
  protected Sprite sprite_off;

    protected override void Activate()
    {
        base.Activate();
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprite_on;
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = sprite_off;
    }
}
