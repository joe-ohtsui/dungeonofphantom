using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DungeonMap : MonoBehaviour
{
    private Texture2D map;
    private Actor player;
    private int count;

    void Start()
    {
		map = new Texture2D(128, 128, TextureFormat.ARGB32, false);
        map.filterMode = FilterMode.Point;

        GetComponent<RawImage>().texture = map;
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        count = 0;
    }

    void Update()
    {
        if (count % 6 == 0)
        {
			DungeonManager.Instance.visited[player.dest.x, player.dest.z] = true;

            Color[] cols = new Color[16384];
            for (int i = 0; i < 16384; i++)
            {
                cols[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            }

            Color white = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            Color green = new Color(0.25f, 1.0f, 0.5f, 0.5f);
            Color blue = new Color(0.25f, 0.75f, 1.0f, 0.5f);
            Color red = new Color(0.75f, 0.0f, 0.0f, 0.5f);
            Color yellow = new Color(1.0f, 1.0f, 0.0f, 0.5f);
			Color purple = new Color (0.75f, 0.25f, 1.0f, 0.5f);

			for (int i = 0; i < 19; i++)
			{
				for (int j = 0; j < 19; j++)
				{
					GridPosition p = new GridPosition (i, j, 0);
					if (getVisited(p.x, p.z))
					{
						if (DungeonManager.Instance.getBlock(p.move(0)) == 1)
						{
							for (int k = 0; k < 7; k++)
							{
								cols[(6 + k + p.x * 6) + 128 * (12 + p.z * 6)] = white;
							}
						}
						if (DungeonManager.Instance.getBlock(p.move(1)) == 1)
						{
							for (int k = 0; k < 7; k++)
							{
								cols[(12 + p.x * 6) + 128 * (6 + k + p.z * 6)] = white;
							}
						}
						if (DungeonManager.Instance.getBlock(p.move(2)) == 1)
						{
							for (int k = 0; k < 7; k++)
							{
								cols[(6 + k + p.x * 6) + 128 * (6 + p.z * 6)] = white;
							}
						}
						if (DungeonManager.Instance.getBlock(p.move(3)) == 1)
						{
							for (int k = 0; k < 7; k++)
							{
								cols[(6 + p.x * 6) + 128 * (6 + k + p.z * 6)] = white;
							}
						}
					}
					for (int x = 0; x < 5; x++)
					{
						for (int y = 0; y < 5; y++)
						{
							if (p == player.dest)
							{
								cols [(7 + x + p.x * 6) + 128 * (7 + y + p.z * 6)] = yellow;
							}
							else if (DungeonManager.Instance.getBlock (p) == 2)
							{
								cols [(7 + x + p.x * 6) + 128 * (7 + y + p.z * 6)] = blue;
							}
							else if (p == DungeonManager.Instance.PhantomPosition ())
							{
								cols [(7 + x + p.x * 6) + 128 * (7 + y + p.z * 6)] = red;
							}
							else if (DungeonManager.Instance.getBlock (p) == 8 && getVisited(p.x, p.z))
							{
								cols[(7 + x + p.x * 6) + 128 * (7 + y + p.z * 6)] = purple;
							}
							else if (getVisited(p.x, p.z))
							{
								cols[(7 + x + p.x * 6) + 128 * (7 + y + p.z * 6)] = green;
							}
						}
					}
				}
			}
			map.SetPixels(0, 0, 128, 128, cols);
            map.Apply();
        }
        count++;
    }

    bool getVisited(int x, int z)
    {
        if (0 <= x && x < DungeonManager.WIDTH && 0 <= z && z < DungeonManager.HEIGHT)
        {
			return DungeonManager.Instance.visited[x, z];
        }
        return false;
    }

}
