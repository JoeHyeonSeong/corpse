using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class InGameObjectData
{


    public InGameObjectsType type;
    /// <summary>
    ///position of this object
    /// </summary>
    public PositionData pos;
    /// <summary>
    /// if not operator->null 
    /// </summary>
    public bool? initialStatus;
    /// <summary>
    /// codes of objects which this object can trigger
    /// </summary>
    public List<int> OperandsCodes;
}

[System.Serializable]
public class PositionData
{
    public int x;
    public int y;
}
