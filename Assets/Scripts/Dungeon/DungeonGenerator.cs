using UnityEngine;
using System.Collections;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject treasurePrefab;
    public GameObject upstairsPrefab;
    public GameObject downstairsPrefab;
    private DungeonManager dm;
    
    void Start ()
    {
        dm = GameObject.FindWithTag("DungeonManager").GetComponent<DungeonManager>();
        Generate();
    }

    void Generate()
    {
        InitFloor();
        WallExtend();
        DungeonObject();
        CreateObject();
    }

    void InitFloor()
    {
        //フロア初期化
        dm.init();
        for (int x = 0; x < DungeonManager.WIDTH; x++)
        {
            for (int z = 0; z < DungeonManager.HEIGHT; z++)
            {
                dm.setBlock(x, z, 1);    //壁
            }
        }
        for (int x = 1; x < DungeonManager.WIDTH - 1; x++)
        {
            for (int z = 1; z < DungeonManager.HEIGHT - 1; z++)
            {
                dm.setBlock(x, z, 0);    //床
            }
        }
    }

    void WallExtend()
    {
        int m = (DungeonManager.WIDTH / 2 + 1) * (DungeonManager.HEIGHT / 2 + 1);
        bool endflag = false;
        while (!endflag)
        {
            GridPosition p = new GridPosition(-1, -1, -1);
            int max = -1;
            int wallcount = 0;
            //起点をランダム選択
            for (int x = 0; x < DungeonManager.WIDTH; x += 2)
            {
                for (int z = 0; z < DungeonManager.HEIGHT; z += 2)
                {
                    if (dm.getBlock(x, z) == 1)
                    {
                        wallcount++;
                        int d = GetNWallDirection(new GridPosition(x, z), 2);
                        int r = Random.Range(0, 100);
                        if (d != -1 && r > max)
                        {
                            max = r;
                            p.set(x, z, d);
                        }
                    }
                }
            }
            //起点になりうる点が存在しなければ，終了
            if (max == -1)
            {
                endflag = true;
            }
            else
            {
                while (p.direction != -1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        p = p.move(p.direction);
                        dm.setBlock(p.x, p.z, 1);
                    }
                    if (Random.Range(0, m) < wallcount)
                    {
                        p.direction = GetNWallDirection(p, 2);
                    }
                    else
                    {
                        p.direction = -1;
                    }
                }
            }
        }
    }

    void DungeonObject()
    {
        GridPosition p = getDeadEnd();
        dm.setBlock(p, 2);
        p.direction = GetNWallDirection(p);

        Actor player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        player.pos = p;
        player.dest = p;

        dm.setBlock(getDeadEnd(), 4);

        for (int i = 0; i < 7; i++)
        {
            dm.setBlock(getDeadEnd(), 6);
        }
    }

    void CreateObject()
    {
        //オブジェクト配置
        for (int x = 0; x < DungeonManager.WIDTH; x++)
        {
            for (int z = 0; z < DungeonManager.HEIGHT; z++)
            {
                instantiateToChildren(floorPrefab, new Vector3(x, -0.5f, z));
                instantiateToChildren(floorPrefab, new Vector3(x, 0.5f, z));
                switch (dm.getBlock(x,z))
                {
                    case 1:
                        instantiateToChildren(wallPrefab, new Vector3(x, 0, z));
                        break;
                    case 2:
                        instantiateToChildren(upstairsPrefab, new Vector3(x, 0, z));
                        break;
                    case 4:
                        instantiateToChildren(downstairsPrefab, new Vector3(x, 0, z));
                        break;
                    case 6:
                        instantiateToChildren(treasurePrefab, new Vector3(x, 0, z));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void instantiateToChildren(GameObject prefab, Vector3 v)
    {
        GameObject o;
        o = (GameObject)Instantiate(prefab, v, Quaternion.identity);
        o.transform.parent = dm.transform;
    }

    int GetNWallDirection(GridPosition position, int distance = 1)
    {
        int direction = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
            if (dm.getBlock(position.move(direction, distance)) == 0)
            {
                return direction;
            }
            direction = (direction + 1) % 4;
        }
        return -1;
    }

    GridPosition getDeadEnd()
    {
        GridPosition p = new GridPosition(-1, -1, -1);
        int max = -1;
        //起点をランダム選択
        for (int i = 1; i < DungeonManager.WIDTH; i += 2)
        {
            for (int j = 1; j < DungeonManager.HEIGHT; j += 2)
            {
                GridPosition q = new GridPosition(i, j);
                int r = Random.Range(0, 100);
                int count = 0;
                for (int k = 0; k < 4; k++)
                {
                    if (dm.getBlock(q.move(k)) == 1)
                    {
                        count++;
                    }
                }
                if (count == 3 && dm.getBlock(i, j) == 0 && r > max)
                {
                    max = r;
                    p.set(i, j, 0);
                }
            }
        }
        return p;
    }
}
