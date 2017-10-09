using UnityEngine;
using System.Collections;

public class GridPosition
{
    public int x;
    public int z;
    public int direction;

    private static int[] dx = { 0, 1, 0, -1 };
    private static int[] dz = { 1, 0, -1, 0 };

    public GridPosition(int _x = 0, int _z = 0, int _direction = 0)
    {
        set(_x, _z, _direction);
    }

    public void set(int _x = 0, int _z = 0, int _direction = 0)
    {
        this.x = _x;
        this.z = _z;
        this.direction = _direction;
    }

    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !a.Equals(b);
    }

    bool Equals(GridPosition a)
    {
        return a.x == x && a.z == z;
    }

    public GridPosition toLeft()
    {
        return new GridPosition(this.x, this.z, (this.direction + 3) % 4);
    }

    public GridPosition toRight()
    {
        return new GridPosition(this.x, this.z, (this.direction + 1) % 4);
    }

    public GridPosition toBack()
    {
        return new GridPosition(this.x, this.z, (this.direction + 2) % 4);
    }

    public GridPosition move(int _direction, int steps = 1)
    {
        return new GridPosition(this.x + steps * dx[_direction], this.z + steps * dz[_direction], this.direction);
    }

    public int distance(GridPosition target)
    {
        return (x - target.x) * (x - target.x) + (z - target.z) * (z - target.z);
    }
}
