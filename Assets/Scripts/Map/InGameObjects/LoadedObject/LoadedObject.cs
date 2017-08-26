﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoadedObject : InGameObject {
    bool isObstacle=true;
    public bool IsObstacle { get { return isObstacle; } }
    abstract public void Push(MovableObject who,Position dir);
}
