using UnityEngine;
using System.Collections;

public class GameMaster : SingletonMonoBehaviour<GameMaster>
{
	private Param param;
	private EqBuff equip;
    public int gold;
	public int[] itemNum;

	// Use this for initialization
	void Awake ()
    {
		if (this != Instance)
		{
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);

		param = GameObject.FindWithTag("Player").GetComponent<Param>();
		equip = GameObject.FindWithTag("Player").GetComponent<EqBuff>();
		itemNum = new int[6]{ 19, 19, 19, 19, 19, 19 };
	}
	
	public float GetExpPercentage()
	{
		int baseExp = nextExp (param.level);
		if (param.level == 15) 
		{
			return 1.0f;
		}
		return 1.0f * (param.exp - baseExp) / (nextExp (param.level + 1) - baseExp);
	}

	public void ObtainExp(int value)
	{
		param.exp += value;
		if (param.exp >= nextExp(param.level + 1) && param.level < 15)
		{
			param.level++;
			param.maxHp = param.level * (param.level + 9) / 2 + 75;
			LogManager.Instance.PutLog (string.Format ("Lv{0}に 上がった", param.level));
		}
	}

	private int nextExp(int level)
	{
		if (level == 1)
		{
			return 0;
		}
		if (level > 15)
		{
			return nextExp (15);
		}
		return level * level * level;
	}

	public void getTreasure()
	{
		int result = 6;
		int s = Random.Range (0, 255);
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
			int g = Random.Range (1, 20) + Random.Range (1, 20) + DungeonManager.Instance.depth * 2 + 20;
			gold += g;
			if (gold > 9999999)
			{
				gold = 9999999;
			}
			LogManager.Instance.PutLog (string.Format(g.ToString() + " Gを 見つけた"));
		}
		//getexp();
	}
}
