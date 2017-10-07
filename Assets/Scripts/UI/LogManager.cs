using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : SingletonMonoBehaviour<LogManager>
{
	private const int MAXCOUNT = 4;
	private string[] textList = new string[MAXCOUNT];

	void Awake()
	{
		if (this != Instance)
		{
			Destroy (this);
			return;
		}
	}

	public void PutLog(string text)
	{
		for (int i = 0; i < MAXCOUNT - 1; i++)
		{
			textList [i] = textList [i + 1];
		}
		textList [MAXCOUNT - 1] = text;
	}

	public string GetLog()
	{
		string text = "";
		for (int i = 0; i < MAXCOUNT; i++)
		{
			text += textList [i] + "\n";
		}
		return text;
	}

}
