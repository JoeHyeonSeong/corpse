using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InGameObject : MonoBehaviour
{

    public enum ActiveStatus
    {
        activating,
        deactivating,
        non_activatable
    }

    protected int activatingPoint = 0;

    [SerializeField]
    private int activateThreshold;
    public int ActivateThreshold { set { activateThreshold = value; } }
    Stack<History> myHistory = new Stack<History>();

    protected Position currentPos;

    protected List<InGameObject> linkedButtons = new List<InGameObject>();

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
    { get { return currentStatus; } set { currentStatus = value; } }

    protected Transform mygraphic;

    protected virtual void Awake()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        mygraphic = transform.Find("Sprite");
    }

    protected virtual void OnEnable()
    {
        if (InGameManager.IsInGameScene())
        {
            Teleport(new Position((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.y)));
        }
    }

    protected virtual void Start()
    {
        if (InGameManager.IsInGameScene())
        {
            ActivateCheck();
        }
    }

    /// <summary>
    /// convert currentStatus to true
    /// </summary>
    protected virtual void Activate()
    {
        currentStatus = ActiveStatus.activating;
    }

    public virtual void AddActiveStack()
    {
        activatingPoint++;
        if (activatingPoint == activateThreshold)
        {
            FlipActiveStatus();
        }
        else if (activatingPoint > activateThreshold)
        {
            activatingPoint = activateThreshold;
        }
    }

    public virtual void SubActiveStack()
    {
        bool wasThreshold = false;
        if (activatingPoint == activateThreshold)
        {
            wasThreshold = true;
        }
        if (activatingPoint > 0)
        {
            activatingPoint--;
        }
        if (wasThreshold)
        {
            FlipActiveStatus();
        }
    }

    protected virtual void FlipActiveStatus()
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

    public virtual void SetSortingOrder()
    {
        if (InGameManager.IsInGameScene())
        {
            if (transform.Find("Sprite") != null)
            {
                transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = -currentPos.Y * 10;
            }
        }
        else
        {
            transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y * 10;
        }
    }

    public virtual void SaveHistory()
    {
        myHistory.Push(new History(currentStatus, currentPos, activatingPoint));
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
            activatingPoint = rollbackHistory.ActivatingPoint;
            SetSortingOrder();
            ActivateCheck();
        }
    }

    public void AddLinkedButton(FlipButton button)
    {
        linkedButtons.Add(button);
    }


    public virtual void ShowLinks()
    {
        ShowLinks(linkedButtons);
    }

    protected void ShowLinks(List<InGameObject> objList)
    {
        if (!isFlickering)
        {
            StartCoroutine(Flicker());
            foreach (InGameObject obj in objList)
            {

                StartCoroutine(obj.Flicker());
            }
        }
    }

    private bool isFlickering = false;
    public virtual IEnumerator Flicker(float deltaTime = 0.3f, int num = 1)
    {
        isFlickering = true;
        for (int i = 0; i < num; i++)
        {
            mygraphic.GetComponent<SpriteFader>().Transparent(deltaTime);
            yield return new WaitForSeconds(deltaTime);
            mygraphic.GetComponent<SpriteFader>().Opaque(deltaTime);
            yield return new WaitForSeconds(deltaTime);
        }
        isFlickering = false;
    }
}
