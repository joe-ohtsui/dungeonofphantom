using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
    public enum Phase
    {
        KEY_WAIT,
        MOVE_START,
        MOVE_NOW,
        ATTACK_START,
        ATTACK_NOW,
        TURN_END
    }

    public GridPosition pos;
    public GridPosition dest;
    public Phase actphase;

    private DungeonManager dm;
    private int count;
    private int dmgcount;
    public const int MAXCOUNT = 14;

    void Start()
    {
        count = MAXCOUNT;
        dmgcount = 0;
        dm = GameObject.FindGameObjectsWithTag("DungeonManager")[0].GetComponent<DungeonManager>();
        actphase = Phase.KEY_WAIT;
    }

    void Update()
    {
        switch (actphase)
        {
            case Phase.MOVE_NOW:
                if (count >= MAXCOUNT - 2)
                {
                    actphase = Phase.TURN_END;
                    pos.x = dest.x;
                    pos.z = dest.z;
                    pos.direction = dest.direction;
                }
                break;
            case Phase.ATTACK_START:
                if (tag == "Actor")
                {
                    count = 0;
                    actphase = Phase.ATTACK_NOW;
                }
                break;
            case Phase.ATTACK_NOW:
                if (count > MAXCOUNT * 2 / 3) { Attack(); }
                break;
            default:
                break;
        }

        if (count < MAXCOUNT) { count++; }
        if (dmgcount > 0)
        {
            dmgcount--;
            if (tag == "Actor")
            {
                if (dmgcount / 4 % 2 == 0)
                {
                    GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }

        Vector3 u = transform.position;
        u.x = 1.0f * (pos.x * (MAXCOUNT - count - 2) + dest.x * count) / (MAXCOUNT - 2);
        u.z = 1.0f * (pos.z * (MAXCOUNT - count - 2) + dest.z * count) / (MAXCOUNT - 2);
        transform.position = u;
    }

    public void setDest(GridPosition _dest)
    {
        if (actphase == Phase.KEY_WAIT)
        {
            if (_dest == pos) { actphase = Phase.MOVE_NOW; }
            else if (!dm.isCollide(_dest)) { actphase = Phase.MOVE_START; }
            if (actphase != Phase.KEY_WAIT)
            {
                dest = _dest;
                count = 0;
            }
        }
    }

    public void Attack()
    {
        GridPosition p = pos.move(pos.direction);
        Param param = GetComponent<Param>();
        int damage = -2;

        GameObject player = GameObject.FindWithTag("Player");
        Actor pla = player.GetComponent<Actor>();
        if (p == pla.dest)
        {
            Param plp = player.GetComponent <Param>();
            damage = pla.damage(param, plp);
        }

        GameObject[] list = GameObject.FindGameObjectsWithTag("Actor");
        foreach (GameObject g in list)
        {
            Actor ba = g.GetComponent<Actor>();
            if (ba.dest == p)
            {
                Param bp = g.GetComponent<Param>();
                damage = ba.damage(param, bp);
                if (bp.hp == 0) { Destroy(g); }
            }
        }

        actphase = Phase.TURN_END;
    }

    public int damage(Param a, Param b)
    {
        int result = -1;
        if (Random.Range(0, 100) < a.hit - b.eva)
        {
            result = a.atk - b.def;
            if (result < 1) { result = 1; }
            result = result * (Random.Range(0, 16) + Random.Range(0, 16) + 30) / 45;
        }
        if (result > 0)
        {
            dmgcount = MAXCOUNT;
            b.hp -= result;
            if (b.hp < 0) { b.hp = 0; }
        }
        return result;
    }

    public Vector3 getLookAt()
    {
        GridPosition p = pos.move(pos.direction);
        GridPosition q = dest.move(dest.direction);
        Vector3 v = new Vector3();
        v.x = 1.0f * (p.x * (MAXCOUNT - count) + q.x * count) / MAXCOUNT;
        v.z = 1.0f * (p.z * (MAXCOUNT - count) + q.z * count) / MAXCOUNT;
        if (MAXCOUNT / 3 < count && count < MAXCOUNT * 2 / 3)
        {
            v.y = 0.01f;
        }
        if (dmgcount > 0)
        {
            v.x += 0.08f * (dmgcount / 4 % 2) - 0.04f;
            v.z += 0.08f * (dmgcount / 4 % 2) - 0.04f;
        }
        return v;
    }

}
