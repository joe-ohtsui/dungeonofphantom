using UnityEngine;
using System.Collections;

public class GameMaster : SingletonMonoBehaviour<GameMaster>
{
	private Param param;
	private EqBuff equip;
    public int gold;

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
		while (param.exp >= nextExp(param.level + 1) && param.level < 15)
		{
			param.level++;
			param.maxHp = param.level * (param.level + 9) / 2 + 75;
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
}
