using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour
{
    public const int WIDTH = 19;
    public const int HEIGHT = 19;
    private int[,] block;
    Actor player;

    public void init()
    {
        block = new int[WIDTH, HEIGHT];
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
    }

    public void setBlock(int x, int z, int value)
    {
        if (0 <= x && x < WIDTH && 0 <= z && z < HEIGHT)
        {
            block[x, z] = value;
        }
    }

    public void setBlock(GridPosition position, int value)
    {
        setBlock(position.x, position.z, value);
    }

    public int getBlock(int x, int z)
    {
        if (0 <= x && x < WIDTH && 0 <= z && z < HEIGHT)
        {
            return block[x, z];
        }
        return -1;
    }

    public int getBlock(GridPosition position)
    {
        return getBlock(position.x, position.z);
    }

    public bool isCollide(int x, int z)
    {
        GridPosition p = new GridPosition(x, z);
        return isCollide(p);
    }

    public bool isCollide(GridPosition position)
    {
        int b = getBlock(position);
        if (b == -1 || b % 2 == 1) { return true; }
        GameObject[] list = GameObject.FindGameObjectsWithTag("Actor");
        if (player.dest == position) { return true; }
        bool flag = false;
        foreach (GameObject g in list)
        {
            if (g.GetComponent<Actor>().dest == position)
            {
                flag = true;
            }
        }
        return flag;
    }
}
