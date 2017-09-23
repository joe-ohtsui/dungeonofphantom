using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SequenceController : SingletonMonoBehaviour<SequenceController>
{
	public GameObject damageUI;
	public GameObject slash;
	private Text damagetext;
	private GameObject player;
	private int count = 0;
    
    void Awake ()
	{
		if (this != Instance)
		{
			Destroy (this);
			return;
		}

		player = GameObject.FindWithTag ("Player");
		damagetext = GameObject.Find ("DamageText").GetComponent<Text> ();
	}
    
    void Update()
    {
		Actor pla = player.GetComponent<Actor> ();
		switch (pla.actphase)
		{
		case Actor.Phase.MOVE_START:
			pla.actphase = Actor.Phase.TURN_END;
			CallEnemiesAI ();
			count = Actor.MAXCOUNT;
			break;
		case Actor.Phase.ATTACK_START:
			Attack (player);
			pla.actphase = Actor.Phase.TURN_END;
			CallEnemiesAI ();
			count = Actor.MAXCOUNT;
			break;
		case Actor.Phase.TURN_END:
			if (count <= 10)
			{
				EnemiesAttack ();
				//aging()
			}
			if (count <= 0)
			{
				TurnEnd ();
			}
			break;
		default:
			break;
		}

		if (count > 0)
		{
			count -= (int)(600 * Time.deltaTime);
//			count--;
		}
    }

    void CallEnemiesAI()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Actor");
        foreach(GameObject g in list)
        {
            g.GetComponent<EnemyAI>().Think();
        }
    }

	void EnemiesAttack()
	{
		GameObject[] list = GameObject.FindGameObjectsWithTag("Actor");
		foreach(GameObject g in list)
		{
			if (g.GetComponent<Actor> ().actphase == Actor.Phase.ATTACK_START)
			{
				Attack (g);
			}
		}
	}

	public void Attack(GameObject obj)
	{
		Actor a = obj.GetComponent<Actor> ();
		GridPosition p = a.pos.move (a.pos.direction);
		Param q = obj.GetComponent<Param> ();
	
		Actor pla = player.GetComponent<Actor> ();
		if (p == pla.dest) {
			Param plp = player.GetComponent <Param> ();
			plDamagePrint (player, damage (q, plp));
		}
	
		GameObject[] list = GameObject.FindGameObjectsWithTag ("Actor");
		foreach (GameObject g in list) {
			Actor ba = g.GetComponent<Actor> ();
			if (ba.pos == p) {
				Param bp = g.GetComponent<Param> ();
				damagePrint (g, damage (q, bp));
				if (bp.hp == 0)
				{
					Destroy (g);
					LogManager.Instance.PutLog (g.transform.name + "を 倒した");
					GameMaster.Instance.ObtainExp (3);
				}
			}
		}
	
		a.actphase = Actor.Phase.TURN_END;
	}
	
    public int damage(Param a, Param b)
	{
		int result = -1;
		if (Random.Range (0, 100) < a.hit - b.eva)
		{
			result = a.atk - b.def;
			if (result < 1)
			{
				result = 1;
			}
			result = result * (Random.Range (0, 16) + Random.Range (0, 16) + 30) / 45;
		}
		if (result > 0)
		{
			b.hp -= result;
			if (b.hp < 0)
			{
				b.hp = 0;
			}
		}
		return result;
	}
	
	void damagePrint(GameObject target, int d)
	{
		Actor a = target.GetComponent<Actor> ();
		GameObject o = (GameObject)Instantiate (damageUI, new Vector3 (a.pos.x, 0.0f, a.pos.z), Quaternion.identity);
		GameObject p = (GameObject)Instantiate (slash, new Vector3 (a.pos.x, 0.0f, a.pos.z), Quaternion.identity);
		o.transform.LookAt (Camera.main.transform.position);
		p.transform.LookAt (Camera.main.transform.position);
		if (d < 0)
		{
			o.transform.GetChild (0).GetComponent<Text> ().text = "MISS";
			LogManager.Instance.PutLog ("攻撃は 外れた");
		}
		else
		{
			o.transform.GetChild (0).GetComponent<Text> ().text = d.ToString ();
			LogManager.Instance.PutLog (target.transform.name + "に " + d.ToString () + "ダメージを 与えた");
			a.damaged ();
		}
		Destroy (o, 0.5f);
		Destroy (p, 0.2f);
	}
	
	void plDamagePrint(GameObject target, int d)
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
			LogManager.Instance.PutLog (d.ToString () + "ダメージを 受けた");
			target.GetComponent<Actor> ().damaged ();
		}
	}

	void TurnEnd()
	{
		GameObject[] list = GameObject.FindGameObjectsWithTag("Actor");
		bool endFlag = true;
		foreach (GameObject g in list)
		{
			Actor a = g.GetComponent<Actor>();
			if (a.actphase != Actor.Phase.TURN_END && a.actphase != Actor.Phase.KEY_WAIT)
			{
				endFlag = false;
			}
		}
		if (endFlag)
		{
			player.GetComponent<Actor>().actphase = Actor.Phase.KEY_WAIT;
			foreach (GameObject g in list)
			{
				g.GetComponent<Actor>().actphase = Actor.Phase.KEY_WAIT;
			}
		}
	}
}
