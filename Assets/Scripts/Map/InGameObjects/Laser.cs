using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : InGameObject
{
    protected Dir4 dir;
    public Dir4 Dir { get { return dir; } set { dir = value; } } 
    protected const int maxLength = 20;

    /// <summary>
    /// true면 resize를 하고 아니라면 작동x
    /// </summary>
    protected bool isResizing=true;
    public bool IsResizing { set { isResizing = value; } }
    /// <summary>
    /// lasers end position
    /// </summary>
    protected Position endPos;


    protected override void Start()
    {
        base.Start();
        GetComponent<BoxCollider2D>().size = new Vector2(1, Mathf.Abs(maxLength));
        GetComponent<BoxCollider2D>().offset = new Vector2(0, Mathf.Abs(maxLength / 2f));
        
    }


    /// <summary>
    /// resize laser's length
    /// </summary>
    public void Resize()
    {
        Position tempEndPos=null;
        if (!isResizing) return;
        if (currentPos == null)
        {
            Start();
        }
        RaycastHit2D[] hits = Physics2D.RaycastAll(currentPos.ToVector3(), Direction.Dir4ToPos(dir).ToVector3(), maxLength, 1 << LayerMask.NameToLayer("InGameObject"));
        for (int i = 0; i < hits.Length; i++)
        {
            InGameObject temp = hits[i].transform.GetComponent<InGameObject>();
            if (temp.GetType().IsSubclassOf(typeof(LoadedObject))
                &&temp.GetType()!=typeof(LaserWall))
            {
                if (temp.GetType().IsSubclassOf(typeof(DestroyableObject)))
                {
                    ((DestroyableObject)temp).Destroy();
                    Resize();
                    return;
                }
                else
                {
                    tempEndPos = temp.CurrentPos;
                }
                break;
            }
        }



        if (tempEndPos == null)
        {
            tempEndPos = currentPos + Direction.Dir4ToPos(dir) * maxLength;
        }
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sortingOrder = (tempEndPos.Y<currentPos.Y)?-tempEndPos.Y*10-1:-currentPos.Y*10-1;

        endPos = tempEndPos;
        Position laserPos = tempEndPos - currentPos;
        int length;
        if (Mathf.Abs(laserPos.X) > Mathf.Abs(laserPos.Y))
        {
            length = laserPos.X;
        }
        else
        {
            length = laserPos.Y;
        }
        transform.Find("Sprite").GetComponent<SpriteRenderer>().size = new Vector2(1, Mathf.Abs(length));
        //rotate
        switch (dir)
        {
            case Dir4.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Dir4.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Dir4.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Dir4.Right:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            default:
                Debug.Log("오류");
                break;
        }
    }
}
