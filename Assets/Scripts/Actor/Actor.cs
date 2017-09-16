using UnityEngine;
using UnityEngine.UI;
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
	public GameObject damageUI;
	public GameObject slash;

	private SpriteRenderer sr;
	private Text damagetext;
	private Image psareaImage;
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
		if (transform.tag == "Actor")
		{
			sr = GetComponent<SpriteRenderer> ();
			damagetext = GameObject.Find ("DamageText").GetComponent<Text> ();
		}
		else if (transform.tag == "Player")
		{
			psareaImage = GameObject.Find ("PlayerStatusArea").GetComponent<Image> ();
		}
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
            if (transform.tag == "Actor")
            {
				if (dmgcount / 3 % 2 == 0)
				{
					sr.enabled = true;
				}
				else
				{
					sr.enabled = false;
				}
            }
			else if (transform.tag == "Player")
			{
				psareaImage.color = new Color (1.0f * dmgcount / MAXCOUNT, 0.0f, 0.0f, 0.25f);
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

        GameObject player = GameObject.FindWithTag("Player");
        Actor pla = player.GetComponent<Actor>();
        if (p == pla.dest)
        {
            Param plp = player.GetComponent <Param>();
			plDamagePrint (pla.damage (param, plp));
        }

        GameObject[] list = GameObject.FindGameObjectsWithTag("Actor");
        foreach (GameObject g in list)
        {
            Actor ba = g.GetComponent<Actor>();
            if (ba.pos == p)
            {
                Param bp = g.GetComponent<Param>();
				damagePrint (ba.damage(param, bp));
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

	void damagePrint(int d)
	{
		GridPosition p = pos.move (pos.direction);
		GameObject o = (GameObject)Instantiate (damageUI, new Vector3 (p.x, 0.0f, p.z), Quaternion.identity);
		GameObject q = (GameObject)Instantiate (slash, new Vector3 (p.x, 0.0f, p.z), Quaternion.identity);
		o.transform.LookAt(Camera.main.transform.position);
		q.transform.LookAt(Camera.main.transform.position);
		if (d < 0)
		{
			o.transform.GetChild(0).GetComponent<Text> ().text = "MISS";
		}
		else
		{
			o.transform.GetChild(0).GetComponent<Text> ().text = d.ToString ();
		}
		Destroy (o, 0.5f);
		Destroy (q, 0.2f);
	}

	void plDamagePrint(int d)
	{
		damagetext.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		damagetext.enabled = true;
		if (d < 0)
		{
			damagetext.text = "MISS";
		}
		else
		{
			damagetext.text = d.ToString ();
		}
	}

    public Vector3 getLookAt()
	{
		GridPosition p = pos.move (pos.direction);
		GridPosition q = dest.move (dest.direction);
		Vector3 v = new Vector3 ();
		v.x = 1.0f * (p.x * (MAXCOUNT - count - 2) + q.x * count) / (MAXCOUNT - 2);
		v.z = 1.0f * (p.z * (MAXCOUNT - count - 2) + q.z * count) / (MAXCOUNT - 2);
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
