using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DungeonMap : MonoBehaviour
{
    private Texture2D map;
    private DungeonManager dm;
    private Actor player;
    private int count;

    private bool[,] visited;
    private bool[,] block;

    void Start()
    {
		map = new Texture2D(128, 128, TextureFormat.ARGB32, false);
//		map = new Texture2D(64, 64, TextureFormat.ARGB32, false);
        map.filterMode = FilterMode.Point;

        GetComponent<RawImage>().texture = map;
        dm = GameObject.FindWithTag("DungeonManager").GetComponent<DungeonManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        count = 0;

        visited = new bool[DungeonManager.WIDTH, DungeonManager.HEIGHT];
        block = new bool[DungeonManager.WIDTH, DungeonManager.HEIGHT];
        for (int x = 0; x < DungeonManager.WIDTH; x++)
        {
            for (int z = 0; z < DungeonManager.HEIGHT; z++)
            {
                visited[x, z] = false;
                block[x, z] = false;
            }
        }
    }

    void Update()
    {
        if (count % 6 == 0)
        {
            visited[player.dest.x, player.dest.z] = true;
            for (int i = 0; i < 4; i++)
            {
                GridPosition p = player.dest.move(i);
                if (dm.getBlock(p) == 1)
                {
                    block[p.x, p.z] = true;
                }
            }

            Color[] cols = new Color[16384];
            for (int i = 0; i < 16384; i++)
            {
                cols[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            }
//			Color[] cols = new Color[4096];
//			for (int i = 0; i < 4096; i++)
//			{
//				cols[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
//			}

            Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            Color green = new Color(0.25f, 1.0f, 0.75f, 0.67f);
            Color blue = new Color(0.75f, 0.25f, 1.0f, 0.67f);
            //Color red = new Color(0.75f, 0.0f, 0.0f, 0.67f);
            Color yellow = new Color(1.0f, 0.875f, 0.25f, 0.67f);

			for (int i = 0; i < 19; i++)
			{
				for (int j = 0; j < 19; j++)
				{
					GridPosition p = new GridPosition (i, j, 0);
					if (getVisited(p.x, p.z))
					{
						if (getBlock(p.move(0)))
						{
							for (int k = 0; k < 7; k++)
							{
								cols[(13 + k + p.x * 6) + 128 * (19 + p.z * 6)] = white;
							}
						}
						if (getBlock(p.move(1)))
						{
							for (int k = 0; k < 7; k++)
							{
								cols[(19 + p.x * 6) + 128 * (13 + k + p.z * 6)] = white;
							}
						}
						if (getBlock(p.move(2)))
						{
							for (int k = 0; k < 7; k++)
							{
								cols[(13 + k + p.x * 6) + 128 * (13 + p.z * 6)] = white;
							}
						}
						if (getBlock(p.move(3)))
						{
							for (int k = 0; k < 7; k++)
							{
								cols[(13 + p.x * 6) + 128 * (13 + k + p.z * 6)] = white;
							}
						}
						for (int x = 0; x < 5; x++)
						{
							for (int y = 0; y < 5; y++)
							{
								if (p == player.pos)
								{
									cols[(14 + x + p.x * 6) + 128 * (14 + y + p.z * 6)] = yellow;
								}	
								else if (dm.getBlock(p)==2)
								{
									cols[(14 + x + p.x * 6) + 128 * (14 + y + p.z * 6)] = blue;
								}
								else
								{
									cols[(14 + x + p.x * 6) + 128 * (14 + y + p.z * 6)] = green;
								}
							}
						}
					}
				}
			}


//            for (int i = -5; i <= 5; i++)
//            {
//                for (int j = -5; j <= 5; j++)
//                {
//                    GridPosition p = player.dest.move(player.dest.direction, i).move((player.dest.direction + 1) % 4, j);
//                    if (getVisited(p.x, p.z))
//                    {
//                        if (getBlock(p.move(player.dest.direction)))
//                        {
//                            for (int k = 0; k < 6; k++)
//                            {
//                                cols[(29 + k + j * 5) + 64 * (34 + i * 5)] = white;
//                            }
//                        }
//                        if (getBlock(p.move((player.dest.direction + 1) % 4)))
//                        {
//                            for (int k = 0; k < 6; k++)
//                            {
//                                cols[(34 + j * 5) + 64 * (29 + k + i * 5)] = white;
//                            }
//                        }
//                        if (getBlock(p.move((player.dest.direction + 2) % 4)))
//                        {
//                            for (int k = 0; k < 6; k++)
//                            {
//                                cols[(29 + k + j * 5) + 64 * (29 + i * 5)] = white;
//                            }
//                        }
//                        if (getBlock(p.move((player.dest.direction + 3) % 4)))
//                        {
//                            for (int k = 0; k < 6; k++)
//                            {
//                                cols[(29 + j * 5) + 64 * (29 + k + i * 5)] = white;
//                            }
//                        }
//                        for (int x = 0; x < 4; x++)
//                        {
//                            for (int y = 0; y < 4; y++)
//                            {
//                                if (dm.getBlock(p)==2)
//                                {
//                                    cols[(30 + x + j * 5) + 64 * (30 + y + i * 5)] = blue;
//                                }
//                                else
//                                {
//                                    cols[(30 + x + j * 5) + 64 * (30 + y + i * 5)] = green;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            for (int x = 0; x < 4; x++)
//            {
//                for (int y = 0; y < 4; y++)
//                {
//                    cols[30 + x + 64 * (30 + y)] = yellow;
//                }
//            }

//            map.SetPixels(0, 0, 64, 64, cols);
			map.SetPixels(0, 0, 128, 128, cols);
            map.Apply();
        }
        count++;
    }

    bool getVisited(int x, int z)
    {
        if (0 <= x && x < DungeonManager.WIDTH && 0 <= z && z < DungeonManager.HEIGHT)
        {
            return visited[x, z];
        }
        return false;
    }

    bool getBlock(int x, int z)
    {
        if (0 <= x && x < DungeonManager.WIDTH && 0 <= z && z < DungeonManager.HEIGHT)
        {
            return block[x, z];
        }
        return false;
    }

    bool getBlock(GridPosition p)
    {
        return getBlock(p.x, p.z);
    }
}
