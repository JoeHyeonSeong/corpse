public class Position
{
    private int x;
    private int y;
    public int X { get { return x; } }
    public int Y { get { return y; } }


    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }



    public static Position operator +(Position pos1, Position pos2)
    {
        return new Position(pos1.x + pos2.x, pos1.y + pos2.y);
    }

    public static Position operator -(Position pos1, Position pos2)
    {
        return pos1 + (-pos2);
    }

    public static Position operator -(Position pos)
    {
        return new Position(-pos.x, -pos.y);
    }

    public static Position operator *(Position pos, int num)
    {
        return new Position(pos.x * num, pos.y * num);
    }
    public override string ToString()
    {
        return "(" + x.ToString() + "," + y.ToString() + ")";
    }

    public UnityEngine.Vector3 ToVector3()
    {
        return new UnityEngine.Vector3(x, y);
    }

}
