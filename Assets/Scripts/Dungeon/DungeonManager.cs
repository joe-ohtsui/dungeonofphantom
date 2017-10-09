using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : SingletonMonoBehaviour<DungeonManager>
{
    public const int WIDTH = 19;
    public const int HEIGHT = 19;
	public int depth;
	public bool[,] visited;
    private int[,] block;
	private int phantomstep;
    Actor player;
	Actor phantom;

    public void init()
    {
		if (this != Instance)
		{
			Destroy (this);
			return;
		}

		visited = new bool[WIDTH, HEIGHT];
		block = new int[WIDTH, HEIGHT];
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
		phantom = null;
		phantomstep = 0;
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

	public GridPosition getRandomPosition()
	{
		GridPosition p = new GridPosition(-1, -1, -1);
		int max = -1;
		//起点をランダム選択
		for (int i = 0; i < WIDTH; i++)
		{
			for (int j = 0; j < HEIGHT; j++)
			{
				int r = Random.Range(0, 100);
				if (getBlock(i, j) == 0 && r > max)
				{
					max = r;
					p.set(i, j, 0);
				}
			}
		}
		return p;
	}

	public GridPosition getDeadEnd()
	{
		GridPosition p = new GridPosition(-1, -1, -1);
		int max = -1;
		//起点をランダム選択
		for (int i = 0; i < WIDTH; i++)
		{
			for (int j = 0; j < HEIGHT; j++)
			{
				GridPosition q = new GridPosition(i, j);
				int r = Random.Range(0, 100);
				int count = 0;
				for (int k = 0; k < 4; k++)
				{
					if (getBlock(q.move(k)) == 1)
					{
						count++;
					}
				}
				if (count == 3 && getBlock(i, j) == 0 && r > max)
				{
					max = r;
					p.set(i, j, 0);
				}
			}
		}
		return p;
	}

	GridPosition randomWarpPosition(GridPosition position)
	{
		GridPosition p = new GridPosition(-1, -1, -1);
		int max = -1;
		//起点をランダム選択
		for (int i = 0; i < WIDTH; i++)
		{
			for (int j = 0; j < HEIGHT; j++)
			{
				int r = Random.Range(0, 100);
				if (getBlock(i, j) == 8 && r > max)
				{
					GridPosition q = new GridPosition (i, j, 0);
					if (q != position)
					{
						p.set (i, j, Random.Range (0, 4));
						max = r;
					}
				}
			}
		}
		return p;
	}

	public GridPosition PhantomPosition()
	{
		if (phantom != null)
		{
			return phantom.dest;
		}
		return new GridPosition (-1, -1, 0);
	}

	public void dungeonEvent(GridPosition position)
	{
		switch (getBlock (position))
		{
		case 2:
			returnToTown ();
			break;
		case 4:
			StartCoroutine (nextFloor ());
			break;
		case 6:
			GameMaster.Instance.getTreasure ();
			destroyTreasureBox (position);
			break;
		case 8:
			StartCoroutine (warppoint (position));
			break;
		case 12:
			trap (position);
			break;
		default:
			break;
		}
		phantomstep++;
		int M = ((depth - 1) / 4) * 100 + 200;
		if (phantomstep % M == 0 && phantom == null)
		{
			phantom = DungeonGenerator.Instance.spawnPhantom ().GetComponent<Actor> ();
			LogManager.Instance.PutLog ("殺気を 感じる");
		}
	}

	void returnToTown()
	{
		FadeManager.Instance.LoadLevel ("Town", 0.5f);
	}

	private IEnumerator nextFloor()
	{
		FadeManager.Instance.StartFadeOut (0.5f);
		yield return new WaitForSeconds (0.5f);
		depth++;
		phantomstep = 0;
		for (int x = 0; x < WIDTH; x++)
		{
			for (int z = 0; z < WIDTH; z++)
			{
				visited [x, z] = false;
			}
		}
		DungeonGenerator.Instance.Generate ();
		LogManager.Instance.PutLog (string.Format ("{0}Fに たどり着いた", depth));
		GameMaster.Instance.ObtainExp (2 + depth / 2);
		FadeManager.Instance.StartFadeIn (0.5f);
		yield return new WaitForSeconds (0.5f);
	}

	void destroyTreasureBox(GridPosition position)
	{
		setBlock(position, 0);
		foreach (Transform n in transform)
		{
			if (n.name == "Treasure" && n.position.x == position.x && n.position.z == position.z)
			{
				GameObject.Destroy (n.gameObject, 0.25f);
				break;
			}
		}
	}

	IEnumerator warppoint(GridPosition position)
	{
		FadeManager.Instance.StartFadeOut (0.5f);
		yield return new WaitForSeconds (0.5f);

		player.dest = randomWarpPosition (position);
		player.pos = player.dest;

		FadeManager.Instance.StartFadeIn (0.5f);
		yield return new WaitForSeconds (0.5f);
	}

	void trap(GridPosition position)
	{
		int damage = (6 * depth - 7) * (Random.Range (0, 16) + Random.Range (0, 16) + 30) / 45;
		GameMaster.Instance.trap (damage);
		setBlock (position, 0);
	}
}
