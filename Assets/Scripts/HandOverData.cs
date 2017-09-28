
public static class HandOverData {
    static private int worldIndex=-1;
    static public int WorldIndex { get { return worldIndex; } set { worldIndex = value; } }

    static private int stageIndex;
    static public int StageIndex{get{return stageIndex;}set{stageIndex = value;}}

    static private bool showStageInfo=true;
    static public bool ShowStageInfo { get { return showStageInfo; } set { showStageInfo = value; } }

    static private string stageName="js1";
    static public string StageName { get { return stageName; } set { stageName = value; } }

    static public bool gameInit = false;
    static public bool GameInit { get { return gameInit; } set { gameInit = value; } }
}