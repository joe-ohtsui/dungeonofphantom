using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Actor : MonoBehaviour
{
    public enum Phase
    {
        KEY_WAIT,
		MOVE_START,
        MOVE_ROTATE,
        ATTACK_START,
        TURN_END
    }

    public GridPosition pos;
    public GridPosition dest;
    public Phase actphase;

	private SpriteRenderer sr;
	private Image psareaImage;
    private int count;
    private int dmgcount;
    public const int MAXCOUNT = 160;

    void Start()
    {
        count = MAXCOUNT;
        dmgcount = 0;
        actphase = Phase.KEY_WAIT;
		if (transform.tag == "Actor")
		{
			sr = GetComponent<SpriteRenderer> ();
		}
		else if (transform.tag == "Player")
		{
			psareaImage = GameObject.Find ("PlayerStatusArea").GetComponent<Image> ();
		}
    }

    void Update()
    {
        if (count < MAXCOUNT)
		{
			count += (int)(600 * Time.deltaTime);
			if (count >= MAXCOUNT - 10)
			{
				actphase = Phase.TURN_END;
				pos.x = dest.x;
				pos.z = dest.z;
				pos.direction = dest.direction;
			}
		}
        if (dmgcount > 0)
        {
			dmgcount -= (int)(600 * Time.deltaTime);
            if (tag == "Actor")
			{
				if (dmgcount / 30 % 2 == 0)
				{
					sr.enabled = true;
				}
				else
				{
					sr.enabled = false;
				}
            }
			else if (tag == "Player")
			{
				psareaImage.color = new Color (1.0f * dmgcount / MAXCOUNT, 0.0f, 0.0f, 0.25f);
			}
        }

        Vector3 u = transform.position;
		u.x = 1.0f * (pos.x * (MAXCOUNT - count - 10) + dest.x * count) / (MAXCOUNT - 10);
		u.z = 1.0f * (pos.z * (MAXCOUNT - count - 10) + dest.z * count) / (MAXCOUNT - 10);
        transform.position = u;
    }

    public void setDest(GridPosition _dest)
    {
        if (actphase == Phase.KEY_WAIT)
        {
            if (_dest == pos)
			{
				actphase = Phase.MOVE_ROTATE;
			}
			else if (!DungeonManager.Instance.isCollide(_dest))
			{
				actphase = Phase.MOVE_START;
				if (tag == "Player")
				{
					DungeonManager.Instance.dungeonEvent (_dest);
				}
			}
            if (actphase != Phase.KEY_WAIT)
            {
                dest = _dest;
				count = 0;
            }
        }
    }

	public void damaged()
	{
		dmgcount = MAXCOUNT;
	}

    public Vector3 getLookAt()
	{
		GridPosition p = pos.move (pos.direction);
		GridPosition q = dest.move (dest.direction);
		Vector3 v = new Vector3 ();
		v.x = 1.0f * (p.x * (MAXCOUNT - count - 10) + q.x * count) / (MAXCOUNT - 10);
		v.z = 1.0f * (p.z * (MAXCOUNT - count - 10) + q.z * count) / (MAXCOUNT - 10);
		if (MAXCOUNT / 3 < count && count < MAXCOUNT * 2 / 3)
		{
			v.y = 0.01f;
		}
		if (dmgcount > 0)
		{
			v.x += 0.08f * (dmgcount / 40 % 2) - 0.04f;
			v.z += 0.08f * (dmgcount / 40 % 2) - 0.04f;
		}
		return v;
	}
}
