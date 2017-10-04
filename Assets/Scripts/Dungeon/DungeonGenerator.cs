using UnityEngine;
using System.Collections;

public class DungeonGenerator : SingletonMonoBehaviour<DungeonGenerator>
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject treasurePrefab;
    public GameObject upstairsPrefab;
    public GameObject downstairsPrefab;

	public GameObject slimePrefab;
	public GameObject ratPrefab;
	public GameObject hornetPrefab;
	public GameObject zombiePrefab;
	public GameObject skeletonPrefab;
    
    void Start ()
	{
		if (this != Instance)
		{
			Destroy (this);
			return;
		}

        Generate();
    }

    public void Generate()
	{
		InitFloor ();
		WallExtend ();
		DungeonObject ();
		ClearObject ();
		CreateObject ();
		CreateMonster ();
	}

    void InitFloor()
    {
        //フロア初期化
		DungeonManager.Instance.init();
        for (int x = 0; x < DungeonManager.WIDTH; x++)
        {
            for (int z = 0; z < DungeonManager.HEIGHT; z++)
            {
				DungeonManager.Instance.setBlock(x, z, 1);    //壁
            }
        }
		for (int x = offset() + 1; x < DungeonManager.WIDTH - offset() - 1; x++)
        {
			for (int z = offset() + 1; z < DungeonManager.HEIGHT - offset() - 1; z++)
            {
				DungeonManager.Instance.setBlock(x, z, 0);    //床
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
			for (int x = offset(); x < DungeonManager.WIDTH - offset(); x += 2)
            {
				for (int z = offset(); z < DungeonManager.HEIGHT - offset(); z += 2)
                {
					if (DungeonManager.Instance.getBlock(x, z) == 1)
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
						DungeonManager.Instance.setBlock(p.x, p.z, 1);
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
		DungeonManager.Instance.setBlock(p, 2);
        p.direction = GetNWallDirection(p);

        Actor player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        player.pos = p;
        player.dest = p;

		DungeonManager.Instance.setBlock(getDeadEnd(), 4);

		for (int i = 0; i < 7 - offset(); i++)
        {
			DungeonManager.Instance.setBlock(getDeadEnd(), 6);
        }
    }

	void ClearObject()
	{
		//オブジェクト削除
		foreach (Transform n in DungeonManager.Instance.transform)
		{
			if (n.tag != "DungeonGenerator")
			{
				GameObject.Destroy (n.gameObject);
			}
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
				switch (DungeonManager.Instance.getBlock(x,z))
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

	void CreateMonster()
	{
		int N = 3 * DungeonManager.Instance.depth / 2 - 1;
		for (int n = 0; n < N; n++)
		{
			int r = Random.Range (0, 256);
			int d = 43 * DungeonManager.Instance.depth;
			if (r < d - 689) {
				//demon
			} else if (r < d - 603) {
				//taurus
			} else if (r < d - 517) {
				//lizardman
			} else if (r < d - 431) {
				instantiateToChildren (skeletonPrefab, new Vector3 (9, 0, 9));
			} else if (r < d - 345) {
				instantiateToChildren (zombiePrefab, new Vector3 (9, 0, 9));
			} else if (r < d - 259) {
				instantiateToChildren (hornetPrefab, new Vector3 (9, 0, 9));
			} else if (r < d - 173) {
				instantiateToChildren (ratPrefab, new Vector3 (9, 0, 9));
			} else {
				instantiateToChildren (slimePrefab, new Vector3 (9, 0, 9));
			}
		}
	}

    void instantiateToChildren(GameObject prefab, Vector3 v)
    {
        GameObject o;
		o = Instantiate(prefab, v, Quaternion.identity) as GameObject;
		o.transform.parent = DungeonManager.Instance.transform;
		o.name = prefab.name;
    }

    int GetNWallDirection(GridPosition position, int distance = 1)
    {
        int direction = Random.Range(0, 4);
        for (int i = 0; i < 4; i++)
        {
			if (DungeonManager.Instance.getBlock(position.move(direction, distance)) == 0)
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
		for (int i = offset() + 1; i < DungeonManager.WIDTH - offset() - 1; i += 2)
        {
			for (int j = offset() + 1; j < DungeonManager.HEIGHT - offset() - 1; j += 2)
            {
                GridPosition q = new GridPosition(i, j);
                int r = Random.Range(0, 100);
                int count = 0;
                for (int k = 0; k < 4; k++)
                {
					if (DungeonManager.Instance.getBlock(q.move(k)) == 1)
                    {
                        count++;
                    }
                }
				if (count == 3 && DungeonManager.Instance.getBlock(i, j) == 0 && r > max)
                {
                    max = r;
                    p.set(i, j, 0);
                }
            }
        }
        return p;
	}

	int offset()
	{
		return 4 - (DungeonManager.Instance.depth - 1) / 4;
	}
}
