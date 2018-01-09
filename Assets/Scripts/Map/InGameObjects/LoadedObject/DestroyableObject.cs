using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyableObject : MovableObject {
    protected bool isDestroyed;
    public bool IsDestroyed
    {get { return isDestroyed; }}
    public abstract void Destroy();
}
