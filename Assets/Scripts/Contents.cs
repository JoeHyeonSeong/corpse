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