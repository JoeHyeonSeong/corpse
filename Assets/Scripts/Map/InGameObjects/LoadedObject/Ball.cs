using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball :MovableObject {

    public void Hide()
    {
        Move(new Position(100, 100), false);
    }
}
