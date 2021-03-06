﻿using UnityEngine;
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
			GameMaster.Instance.aging ();
			pla.actphase = Actor.Phase.TURN_END;
			CallEnemiesAI ();
			count = Actor.MAXCOUNT;
			break;
		case Actor.Phase.ATTACK_START:
			Attack (player);
			GameMaster.Instance.aging ();
			pla.actphase = Actor.Phase.TURN_END;
			CallEnemiesAI ();
			count = Actor.MAXCOUNT;
			break;
		case Actor.Phase.TURN_END:
			if (count <= 10)
			{
				EnemiesAttack ();
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
					LogManager.Instance.PutLog (g.transform.name + "を 倒した");
					switch (g.transform.name)
					{
					case "Slime":
						AchievementManager.Instance.addCount (0, 1);
						break;
					case "Rat":
						AchievementManager.Instance.addCount (1, 1);
						break;
					case "Hornet":
						AchievementManager.Instance.addCount (2, 1);
						break;
					case "Zombie":
						AchievementManager.Instance.addCount (3, 1);
						break;
					case "Skeleton":
						AchievementManager.Instance.addCount (4, 1);
						break;
					case "Dragonewt":
						AchievementManager.Instance.addCount (5, 1);
						break;
					case "Taurus":
						AchievementManager.Instance.addCount (6, 1);
						break;
					case "Demon":
						AchievementManager.Instance.addCount (7, 1);
						break;
					case "Phantom":
						AchievementManager.Instance.addCount (8, 1);
						GameMaster.Instance.equip.Sword.set (5, Random.Range (0, 16), 15, Random.Range (0, 16), 15);
						break;
					case "Dragon":
						AchievementManager.Instance.addCount (9, 1);
						break;
					default:
						break;
					}
					GameMaster.Instance.ObtainExp (bp.exp);
					Destroy (g);
					AudioManager.Instance.playSE (3);
				}
			}
		}
	
		a.actphase = Actor.Phase.TURN_END;
	}
	
    public int damage(Param a, Param b)
	{
		int result = GameMaster.Instance.calcDamage (a, b);
		GameMaster.Instance.dealDamage(b, result);
		return result;
	}
	
	void damagePrint(GameObject target, int d)
	{
		Actor a = target.GetComponent<Actor> ();
		GridPosition q = player.GetComponent<Actor> ().pos;
		Vector3 v = new Vector3(a.pos.x * 0.9f + q.x * 0.1f, 0.0f, a.pos.z * 0.9f + q.z * 0.1f);
		GameObject o = (GameObject)Instantiate (damageUI, v, Quaternion.identity);
		GameObject p = (GameObject)Instantiate (slash, v, Quaternion.identity);
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
			LogManager.Instance.PutLog (string.Format ("{0}に {1}ダメージを 与えた", target.name, d));
			a.damaged ();
			AudioManager.Instance.playSE (2);
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
			LogManager.Instance.PutLog (string.Format("{0}ダメージを 受けた", d));
			target.GetComponent<Actor> ().damaged ();
			AudioManager.Instance.playSE (2);
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
