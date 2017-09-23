using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private DungeonManager dm;
    private Actor actor;
    private Actor player;
    
    void Start ()
    {
        dm = GameObject.FindWithTag("DungeonManager").GetComponent<DungeonManager>();
        actor = GetComponent<Actor>();
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();

		initPosition ();
	}

	void initPosition()
	{
		actor.pos = new GridPosition (-1, -1, -1);
		int max = -1;

		//起点をランダム選択
		for (int i = 0; i < DungeonManager.WIDTH; i++)
		{
			for (int j = 0; j < DungeonManager.HEIGHT; j++)
			{
				int r = Random.Range (0, 100);
				if (DungeonManager.Instance.getBlock (i, j) == 0 && r > max)
				{
					max = r;
					actor.pos.set (i, j, 0);
				}
			}
		}
		actor.dest = actor.pos;
	}

    public void Think()
    {
        actor.actphase = Actor.Phase.KEY_WAIT;
        if (actor.pos.distance(player.dest) == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                if (actor.pos.move(i) == player.dest)
                {
                    actor.pos.direction = i;
                    actor.actphase = Actor.Phase.ATTACK_START;
                    break;
                }
            }
        }
        else
        {
            bool flag = false;
            for (int i = 0; i < 4; i++)
            {
                int N = 3;
                if (i == actor.pos.direction) { N = 5; }
                for (int j = 1; j <= N; j++)
                {
                    GridPosition p = actor.pos.move(i, j);
                    if (dm.getBlock(p) % 2 == 1) { break; }
                    if (p == player.pos)
                    {
                        actor.pos.direction = i;
                        actor.setDest(actor.pos.move(actor.pos.direction));
                        flag = true;
                    }
                }
                if (flag) { break; }
            }
            if (!flag)
            {
                actor.pos.direction = (actor.pos.direction + 3) % 4;
                for (int i = 0; i < 4; i++)
                {
                    actor.setDest(actor.pos.move(actor.pos.direction));
                    if (actor.actphase == Actor.Phase.MOVE_START) { break; }
                    actor.pos.direction = (actor.pos.direction + 1) % 4;
                }
            }
        }
    }
}
