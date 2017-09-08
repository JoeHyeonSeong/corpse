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
    protected int activatingPoint;
    [SerializeField]
   private int ActivateThreshold;
    Stack<History> myHistory = new Stack<History>();
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
    }

    protected virtual void OnEnable()
    {
        Teleport(new Position((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y)));
    }
    protected virtual void Start()
    {
        ActivateCheck();
    }

    /// <summary>
    /// convert currentStatus to true
    /// </summary>
    protected virtual void Activate()
    {
        currentStatus = ActiveStatus.activating;
    }

    public virtual void AddStack()
    {
        activatingPoint++;
        if (activatingPoint == ActivateThreshold)
        {
            FlipStatus();
        }
    }

    public virtual void SubStack()
    {
        activatingPoint--;
        if (activatingPoint == ActivateThreshold-1)
        {
            FlipStatus();
        }
    }

    protected virtual void FlipStatus()
    {
        if (currentStatus == ActiveStatus.activating)
        {
            Deactivate();
        }
        else if (currentStatus == ActiveStatus.deactivating)
        {
            Activate();
        }
    }
    /// <summary>
    /// convert currentStatus to false
    /// </summary>
    protected virtual void Deactivate()
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
        SetSortingOrder();
    }

    public void ActivateCheck()
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

    protected void SetSortingOrder()
    {
        if (transform.Find("Sprite") != null)
        {
            transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = -currentPos.Y * 10;
        }
    }

    public virtual void SaveHistory()
    {
        myHistory.Push(new History(currentStatus,currentPos));
    }

    public virtual void RollBack()
    {
        if (myHistory.Count == 0)
        {
            Destroy(this.gameObject);
        }
        else if (myHistory.Count > 0)
        {
            History rollbackHistory = myHistory.Pop();
            currentPos = rollbackHistory.Pos;
            transform.position = currentPos.ToVector3();
            currentStatus = rollbackHistory.Status;
            SetSortingOrder();
            ActivateCheck();
        }
        //SaveHistory();
    }
}
