using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : SingletonMonoBehaviour<AchievementManager>
{
	public int[] count;
	public int[] rank;
	public bool[] unread;

	// Use this for initialization
	void Start ()
	{
		if (this != Instance)
		{
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
		init ();
	}

	public void init()
	{
		count = new int[16]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		rank = new int[16]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		unread = new bool[16];
	}

	public int nextCount(int id, int _rank)
	{
		if (id == 10 || id == 11)
		{
			switch (_rank)
			{
			case 0:
				return 1;
			case 1:
				return 1000;
			case 2:
				return 100000;
			default:
				return 9999999;
			}
		}
		else if (id == 12)
		{
			switch (_rank)
			{
			case 0:
				return 2;
			case 1:
				return 9;
			case 2:
				return 17;
			default:
				return 20;
			}
		}
		else
		{
			switch (_rank)
			{
			case 0:
				return 1;
			case 1:
				return 10;
			case 2:
				return 100;
			default:
				return 9999;
			}
		}
		return 1;
	}

	public void addCount(int id, int _value)
	{
		setCount (id, count [id] + _value);
	}

	public void setCount(int id, int _value)
	{
		if (_value > count [id])
		{
			int max = nextCount (id, 3);
			count [id] = _value;
			if (count [id] > max)
			{
				count [id] = max;
			}
			while (count [id] >= nextCount (id, rank [id]) && rank [id] < 4)
			{
				rank [id]++;
				unread [id] = true;
			}
		}
	}
}
