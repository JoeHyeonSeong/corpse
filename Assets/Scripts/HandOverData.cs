
public static class HandOverData {
    static private int stageNum;
    static public int Stagenum
    {get{return stageNum;}set{stageNum = value;}}

    static private bool showStageInfo=true;
    static public bool ShowStageInfo { get { return showStageInfo; } set { showStageInfo = value; } }
    static private string stage="Level";
    static public string Stage { get { return stage; } set { stage = value; } }
}