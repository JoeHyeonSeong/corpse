﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignLineCorner : SignLine {
    static Sprite sprite_on_src;
    static Sprite sprite_off_src;
    protected override void Awake()
    {
        base.Awake();
        if (sprite_on == null)
        {
            sprite_on_src = Resources.Load<Sprite>("Graphic/InGameObject/SignLine_corner_on");
            sprite_off_src= Resources.Load<Sprite>("Graphic/InGameObject/SignLine_corner_off");
        }
        sprite_on = sprite_on_src;
        sprite_off = sprite_off_src;
    }
}
