﻿using UnityEngine;
using System.Collections;

public class GameMaster : SingletonMonoBehaviour<GameMaster>
{
	public int level;
	public int exp;
    public int gold;
	public int[] itemNum;
	public EqBuff equip;

	// Use this for initialization
	void Awake ()
    {
		if (this != Instance)
		{
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);

		itemNum = new int[6]{ 0, 0, 0, 0, 0, 0 };
		equip = new EqBuff ();
		SaveLoad.Instance.load ();

		calcParam ();
	}

	public float GetExpPercentage()
	{
		Param param = GameObject.FindWithTag ("Player").GetComponent<Param> ();
		int baseExp = nextExp (param.level);
		if (level == 15) 
		{
			return 1.0f;
		}
		return 1.0f * (exp - baseExp) / (nextExp (level + 1) - baseExp);
	}

	public void ObtainExp(int value)
	{
		Param param = GameObject.FindWithTag ("Player").GetComponent<Param> ();
		exp += value;
		if (exp > 9999)
		{
			exp = 9999;
		}
		if (exp >= nextExp(level + 1) && level < 15)
		{
			level++;
			calcParam ();
			LogManager.Instance.PutLog (string.Format ("Lv{0}に 上がった", param.level));
		}
	}

	private int nextExp(int l)
	{
		if (l == 1)
		{
			return 0;
		}
		if (l > 15)
		{
			return nextExp (15);
		}
		return 5 * l * l * l - 40 * l * l + 153 * l - 171;
	}

	public void aging()
	{
		if (equip.atkForce > 0) {
			equip.atkForce--;
		}
		if (equip.defForce > 0) {
			equip.defForce--;
		}
		if (equip.hitForce > 0) {
			equip.hitForce--;
		}
		if (equip.evaForce > 0) {
			equip.evaForce--;
		}
		calcParam ();
	}

	public void calcParam()
	{
		Param param = GameObject.FindWithTag ("Player").GetComponent<Param>();
		param.level = level;
		param.maxHp = level * (level + 9) / 2 + 75;
		param.atk = equip.Sword.atk * (100 + equip.atkForce * 7) / 100;
		param.def = equip.Shield.def * (100 + equip.defForce * 6) / 100;
		param.hit = equip.Sword.hit + equip.hitForce * 9;
		param.eva = equip.Shield.eva + equip.evaForce * 8;
	}

	public void maxHp()
	{
		Param param = GameObject.FindWithTag ("Player").GetComponent<Param>();
		param.maxHp = level * (level + 9) / 2 + 75;
		param.hp = param.maxHp;
	}

	public void getTreasure()
	{
		int result = 6;
		int s = Random.Range (0, 256);
		if (s < 12)
		{
			result = 5;
		}
		else if (s < 25)
		{
			result = 0;
		}
		else if (s < 40)
		{
			result = 1;
		}
		else if (s < 57)
		{
			result = 3;
		}
		else if (s < 77)
		{
			result = 2;
		}
		else if (s < 100)
		{
			result = 4;
		}
		if (result != 6)
		{
			if (itemNum [result] < 99)
			{
				itemNum [result]++;
				switch (result)
				{
				case 0:
					LogManager.Instance.PutLog("Rec Potionを 見つけた");
					break;
				case 1:
					LogManager.Instance.PutLog("Atk Potionを 見つけた");
					break;
				case 2:
					LogManager.Instance.PutLog("Def Potionを 見つけた");
					break;
				case 3:
					LogManager.Instance.PutLog("Hit Potionを 見つけた");
					break;
				case 4:
					LogManager.Instance.PutLog("Eva Potionを 見つけた");
					break;
				case 5:
					LogManager.Instance.PutLog("Trap Guardを 見つけた");
					break;
				default:
					break;
				}
			}
			else
			{
				result = 6;
			}
		}
		if (result == 6)
		{
			int g = Random.Range (0, 20) + Random.Range (0, 20) + DungeonManager.Instance.depth * 2 + 26;
			gold += g;
			if (gold > 9999999)
			{
				gold = 9999999;
			}
			LogManager.Instance.PutLog (string.Format(g.ToString() + " Gを 見つけた"));
		}
		GameMaster.Instance.ObtainExp (1);
	}

	public string toJson()
	{
		SaveData data = new SaveData ();
		int i = itemNum [0] + itemNum [1] + itemNum [2];
		int j = itemNum [3] + itemNum [4] + itemNum [5];
		data.i = itemNum[0] + itemNum[1] * 101 + itemNum[2] * 10201 + i * 1030301;
		data.j = itemNum[3] + itemNum[4] * 101 + itemNum[5] * 10201 + j * 1030301;
		data.x = gold + exp;
		data.g = gold - exp;
		data.w = equip.Sword.toInt();
		data.h = equip.Shield.toInt();
		string json = JsonUtility.ToJson (data);
		Debug.Log ("json:" + json);
		return json;
	}

	public void fromJson(string json)
	{
		SaveData data = JsonUtility.FromJson<SaveData> (json);
		itemNum [0] = data.i % 101;
		itemNum [1] = data.i / 101 % 101;
		itemNum [2] = data.i / 10201 % 101;
		if (itemNum [0] + itemNum [1] + itemNum [2] != data.i / 1030301) {
			itemNum [0] = 0;
			itemNum [1] = 0;
			itemNum [2] = 0;
		}
		itemNum [3] = data.j % 101;
		itemNum [4] = data.j / 101 % 101;
		itemNum [5] = data.j / 10201 % 101;
		if (itemNum [3] + itemNum [4] + itemNum [5] != data.j / 1030301) {
			itemNum [3] = 0;
			itemNum [4] = 0;
			itemNum [5] = 0;
		}
		gold = (data.x + data.g) / 2;
		exp = (data.x - data.g) / 2;
		equip.Sword.set (data.w);
		equip.Shield.set (data.h);
		level = 1;
		while (exp >= nextExp(level + 1) && level < 15)
		{
			level++;
			calcParam ();
		}
		Debug.Log ("level:" + level);
		Debug.Log ("exp:" + exp);
		Debug.Log ("gold:" + gold);
	}
}
