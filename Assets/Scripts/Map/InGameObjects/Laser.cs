using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : InGameObject
{
    protected Position endPos;
    protected Dir4 dir;
    public Dir4 Dir { get { return dir; } set { dir = value; } } 
    protected const int maxLength = 20;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Resize();
    }

    public void Touch(LoadedObject who)
    {
        if (who.GetType().IsSubclassOf(typeof(DestroyableObject)))
        {
            ((DestroyableObject)who).Destroy();
        }
        else
        {
            Resize();
        }
    }

    /// <summary>
    /// resize laser's length
    /// </summary>
    public void Resize()
    {
        Position tempEndPos=null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(currentPos.ToVector3(), Direction.Dir4ToPos(dir).ToVector3(), maxLength, 1 << LayerMask.NameToLayer("InGameObject"));
        for (int i = 0; i < hits.Length; i++)
        {
            InGameObject temp = hits[i].transform.GetComponent<InGameObject>();
            if (temp.GetType().IsSubclassOf(typeof(LoadedObject))
                &&temp.GetType()!=typeof(LaserWall))
            {
                tempEndPos = temp.CurrentPos;
                break;
            }
        }
        if (tempEndPos == null)
        {
            tempEndPos = currentPos + Direction.Dir4ToPos(dir) * maxLength;
        }
        endPos = tempEndPos;
        Vector2 size;
        Vector2 offset;
        Position laserPos = endPos - currentPos;
        if (Mathf.Abs(laserPos.X) > Mathf.Abs(laserPos.Y))
        {
            size = new Vector2(laserPos.X, 1);
            offset = new Vector2(laserPos.X / 2f, 0);
        }
        else
        {
            size = new Vector2(1, laserPos.Y);
            offset = new Vector2(0, laserPos.Y / 2f);
        }
        GetComponent<BoxCollider2D>().size = new Vector2(Mathf.Abs(size.x),Mathf.Abs(size.y));
        GetComponent<BoxCollider2D>().offset = offset;
        GetComponent<SpriteRenderer>().size = size;
    }
}
