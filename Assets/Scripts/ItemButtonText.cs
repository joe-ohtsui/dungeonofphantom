using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonText : MonoBehaviour {
	
	public enum Entry
	{
		RecPotion,
		AtkPotion,
		DefPotion,
		HitPotion,
		EvaPotion,
		TrapGuard
	}

	public Entry entry;
	private Text targettext;

	// Use this for initialization
	void Start ()
	{
		targettext = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (entry)
		{
		case Entry.RecPotion:
			targettext.text = string.Format ("Rec Potion:{0,3}\nHP+80", GameMaster.Instance.itemNum [0]);
			break;
		case Entry.AtkPotion:
			targettext.text = string.Format ("Atk Potion:{0,3}\nAtk+70%", GameMaster.Instance.itemNum [1]);
			break;
		case Entry.DefPotion:
			targettext.text = string.Format ("Def Potion:{0,3}\nDef+60%", GameMaster.Instance.itemNum [2]);
			break;
		case Entry.HitPotion:
			targettext.text = string.Format ("Hit Potion:{0,3}\nHit+90%", GameMaster.Instance.itemNum [3]);
			break;
		case Entry.EvaPotion:
			targettext.text = string.Format ("Eva Potion:{0,3}\nEva+80%", GameMaster.Instance.itemNum [4]);
			break;
		case Entry.TrapGuard:
			targettext.text = string.Format ("Trap Guard:{0,3}\nDetect+Guard", GameMaster.Instance.itemNum [5]);
			break;
		default:
			break;
		}
	}
}
