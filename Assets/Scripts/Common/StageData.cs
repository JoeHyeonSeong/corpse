using UnityEngine;

[System.Serializable]
public class StageData
{
    public int[] level;
    public int[] threshold;
    public InGameObject.ActiveStatus[] state;
    public Vector3[] trigger;
    public int[] triggerNum;
    public int height;
    public int width;
    public int layer;
    public int life;
    public string title;
}