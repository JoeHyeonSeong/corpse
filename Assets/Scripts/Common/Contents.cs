using UnityEngine;
public class Contents
{

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
    public const string mapEdit = "MapEditor";
}


public static class PrefsKey
{
    public const string lastWorld = "LastWorld";
    public const string lockInfo = "LockInfo";
    public const string musicOn = "MusicOn";
    public const string soundEffectOn = "SoundEffectOn";
}