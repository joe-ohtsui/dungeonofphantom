﻿using UnityEngine;
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

        actor.pos = new GridPosition(9, 9, 0);
        actor.dest = new GridPosition(actor.pos.x, actor.pos.z, actor.pos.direction);
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
            if (actor.actphase == Actor.Phase.MOVE_START) { actor.actphase = Actor.Phase.MOVE_NOW; }
        }
    }
}
