using UnityEngine;
public class Contents  {
    
}

public enum InGameObjectsType
{
    Empty,
    Floor,
    Ice,
    GenPoint,
    Spine,
    Button,
    DisposableButton,
    Destination,
    Box,
    Ball,
    Corpse,
    Wall,
    LaserWall,
    Character
}

public enum Dir4
{
    Right,
    Left,
    Up,
    Down
}

public class Direction
{

    public static Position Dir4ToPos(Dir4 dir)
    {
        switch (dir)
        {
            case Dir4.Right:
                return new Position(1, 0);
            case Dir4.Left:
                return new Position(-1, 0);
            case Dir4.Up:
                return new Position(0, 1);
            case Dir4.Down:
                return new Position(0, -1);
            default:
                Debug.Log("error");
                return new Position(0, 0);
        }
    }

}

public static class SceneName
{
    public const string inGameScene = "InGameScene";
    public const string main = "Main";
}

public static class StageList
{
    //각각의 월드에 스테이지가 몇개인지
    private static int[] StageNo = new int[] { 1, 1, 1 };
    private static string[,] stageList =new string[,] {
        {"Level" },//world0
        {"Level" },//world1
        {"Level" }//world2
    };

    public static string GetStageName(int world, int stage)
    {
        if (StageNo[world] < stage)
        {
            Debug.Log("invalid stage");
            return null;
        }
        return stageList[world, stage];
    }

    public static int GetStageNo(int world)
    {
        return StageNo[world];
    }
}