using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour {
    Stack<List<MoveHistory>> moveStack;
    class MoveHistory
    {
        InGameObject obj;
        Position pos;
    }
    public static HistoryManager instance;
    private void Awake()
    {
        instance = this;
        moveStack = new Stack<List<MoveHistory>>();
    }
}
