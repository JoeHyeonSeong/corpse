using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultFloor : Floor {

    protected override void Start()
    {
        base.Start();
        SetSprite();
    }
    private static Sprite [] sprites;

    private void SetSprite()
    {
        if (sprites == null)
        {
            sprites = new Sprite[3];
            sprites[0] = Resources.Load<Sprite>("Graphic/InGameObject/Floor/floor");
            sprites[1] = Resources.Load<Sprite>("Graphic/InGameObject/Floor/floor2");
            sprites[2] = Resources.Load<Sprite>("Graphic/InGameObject/Floor/floor3");
        }
        System.Random random = new System.Random(currentPos.X + currentPos.Y);
        mygraphic.GetComponent<SpriteRenderer>().sprite = sprites[random.Next(0,3)];
    }
}
