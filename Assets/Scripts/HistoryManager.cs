﻿using System.Collections;
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

    private int savingPhase;
    Stack<MoveHistory[]> moveStack;
    List<MoveHistory> currentHistory = new List<MoveHistory>();

    private void Awake()
    {
        instance = this;
        moveStack = new Stack<MoveHistory[]>();
    }

    public void SaveMove(InGameObject obj, Position pos, bool generated)
    {
        if (savingPhase != InGameManager.instance.Phase)
        {
            savingPhase = InGameManager.instance.Phase;
        }
        currentHistory.Add(new MoveHistory(obj, pos, generated));
    }


    /// <summary>
    /// commit move datas
    /// </summary>
    public void CommitHistory()
    {
        Debug.Log("commit");
        moveStack.Push(currentHistory.ToArray());
        currentHistory = new List<MoveHistory>();
    }

    public void RollBack()
    {
        Debug.Log("roll back");
        savingPhase--;
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



        foreach (MoveHistory his in rollbackData)
        {
            if (his.Generated && his.Obj != null)
            {
                Destroy(his.Obj.gameObject);
            }
            else
            {
                Debug.Log(his.Obj.ToString() + his.Pos);
                ((MovableObject)his.Obj).Move(his.Pos,true);
            }
        }
    }

    private void Start()
    {
        CommitHistory();
    }
}