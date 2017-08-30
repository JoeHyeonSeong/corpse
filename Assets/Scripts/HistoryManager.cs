using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{


    class MoveHistory
    {
        public MoveHistory(InGameObject obj, Position pos, bool generated)
        {
            this.obj = obj;
            this.pos = pos;
            this.generated = generated;
        }
        InGameObject obj;
        public InGameObject Obj { get { return obj; } }
        Position pos;
        public Position Pos { get { return pos; } }
        bool generated;
        public bool Generated { get { return generated; } }
    }


    public static HistoryManager instance;

    Stack<MoveHistory[]> moveStack;
    List<MoveHistory> currentHistory = new List<MoveHistory>();

    private void Awake()
    {
        instance = this;
        moveStack = new Stack<MoveHistory[]>();
    }

    public void SaveMove(InGameObject obj, Position pos, bool generated)
    {
        currentHistory.Add(new MoveHistory(obj, pos, generated));
    }


    /// <summary>
    /// commit move datas
    /// </summary>
    public void CommitHistory()
    {
        moveStack.Push(currentHistory.ToArray());
        currentHistory = new List<MoveHistory>();
    }

    public void RollBack()
    {
        //stack must have data


        MoveHistory[] rollbackData;
        do
        {
            if (moveStack.Count <= 1)
            {
                return;
            }
            rollbackData = moveStack.Pop();
        }
        while (rollbackData.Length == 0);
        for (int i = 0; i < rollbackData.Length; i++)
        {
            MoveHistory his = rollbackData[rollbackData.Length - 1 - i];
            if (his.Generated && his.Obj != null)
            {
                Position lastPos = his.Obj.CurrentPos;
                Destroy(his.Obj.gameObject);
                MapManager.instance.ResizeSideLasers(lastPos);
            }
            else
            {
                if (his.Obj.GetType().IsSubclassOf(typeof(MovableObject)))
                {

                    ((MovableObject)his.Obj).Move(his.Pos,false,false);
                }
            }
        }
    }

    private void Start()
    {
        CommitHistory();
    }
}
