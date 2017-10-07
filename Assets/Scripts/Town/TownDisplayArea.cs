using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownDisplayArea : MonoBehaviour {
	public TownManager.Type type;
	public int ID;
	Text targettext;
	EqBuff equip;

	// Use this for initialization
	void Start () {
		targettext = GetComponent<Text> ();
		equip = GameMaster.Instance.equip;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (type)
		{
		case TownManager.Type.Sword:
			if (TownManager.Instance.SwordIsNothing [ID])
			{
				targettext.text = "- Sold Out -";
			}
			else
			{
				Equip e = TownManager.Instance.SwordList [ID];
				int gold = e.gold - equip.Sword.gold / 2;
				targettext.text = string.Format ("{0,-12} Atk:{1,3} Hit:{2,3}% {3,7} G", e.name, e.atk, e.hit, gold);
			}
			break;
		case TownManager.Type.Shield:
			if (TownManager.Instance.ShieldIsNothing [ID])
			{
				targettext.text = "- Sold Out -";
			}
			else
			{
				Equip e = TownManager.Instance.ShieldList [ID];
				int gold = e.gold - equip.Shield.gold / 2;
				targettext.text = string.Format ("{0,-12} Def:{1,3} Eva:{2,3}% {3,7} G", e.name, e.def, e.eva, gold);
			}
			break;
		case TownManager.Type.Item:
			switch (ID) {
			case 0:
				targettext.text = string.Format ("Rec Potion:{0,3} ...  90 G", GameMaster.Instance.itemNum [0]);
				break;
			case 1:
				targettext.text = string.Format ("Atk Potion:{0,3} ...  80 G", GameMaster.Instance.itemNum [1]);
				break;
			case 2:
				targettext.text = string.Format ("Def Potion:{0,3} ...  60 G", GameMaster.Instance.itemNum [2]);
				break;
			case 3:
				targettext.text = string.Format ("Hit Potion:{0,3} ...  70 G", GameMaster.Instance.itemNum [3]);
				break;
			case 4:
				targettext.text = string.Format ("Eva Potion:{0,3} ...  50 G", GameMaster.Instance.itemNum [4]);
				break;
			case 5:
				targettext.text = string.Format ("Trap Guard:{0,3} ... 100 G", GameMaster.Instance.itemNum [5]);
				break;
			default:
				break;
			}
			break;
		default:
			break;
		}
	}
}
