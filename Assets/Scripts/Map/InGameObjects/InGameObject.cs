using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InGameObject : MonoBehaviour {

    public enum ActiveStatus
    {
        activating,
        deactivating,
        non_activatable
    }
    /// <summary>
    /// current position of gameObject
    /// </summary>
    protected Position currentPos;
    /// <summary>
    /// return current position of game Object(read only)
    /// </summary>
    public Position CurrentPos { get { return currentPos; } }
    [SerializeField]
    /// <summary>
    /// if can't activate-> non_activatable else if activated->activating else->deactivating
    /// </summary>
    protected ActiveStatus currentStatus;
    public ActiveStatus CurrentStatus
    { get { return currentStatus; } }
    protected virtual void Awake()
    {

        Teleport(new Position((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y)));
        GetComponent<SpriteRenderer>().sortingOrder = -currentPos.Y*10;
    }

    protected virtual void Start()
    {
        if (currentStatus == ActiveStatus.activating)
        {
            Activate();
        }
        else if (currentStatus == ActiveStatus.deactivating)
        {
            Deactivate();
        }
    }

    /// <summary>
    /// convert currentStatus to true
    /// </summary>
    public virtual void Activate()
    {
        currentStatus = ActiveStatus.activating;
    }

    /// <summary>
    /// convert currentStatus to false
    /// </summary>
    public virtual void Deactivate()
    {
        currentStatus = ActiveStatus.deactivating;
    }

    /// <summary>
    /// change position to des
    /// </summary>
    /// <param name="des"></param>
    public virtual void Teleport(Position des)
    {
        currentPos = des;
        transform.position = currentPos.ToVector3();
    }
}
