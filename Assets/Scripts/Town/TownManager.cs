using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : SingletonMonoBehaviour<TownManager> {

	public enum Type{
		Sword,
		Shield,
		Item,
		SellItem
	}

	public Equip[] SwordList;
	public Equip[] ShieldList;
	public bool[] SwordIsNothing;
	public bool[] ShieldIsNothing;
	int[] itemPrice;

	// Use this for initialization
	void Start ()
	{
		if (this != Instance)
		{
			Destroy (this);
			return;
		}
		GameMaster.Instance.calcParam ();
		GameMaster.Instance.maxHp ();
		CreateTrade ();
	}

	void CreateTrade()
	{
		SwordList = new Equip[6];
		ShieldList = new Equip[6];
		SwordIsNothing = new bool[6];
		ShieldIsNothing = new bool[6];
		itemPrice = new int[6]{ 90, 80, 60, 70, 50, 100 };
		for (int i = 0; i < 6; i++)
		{
			int r = Random.Range (0, 256);
			int level = GameMaster.Instance.level;
			int id = 0;
			if (r < 3 * level / 15) {
				id = 4;
			} else if (r < 65 * level / 15) {
				id = 3;
			} else if (r < 191 * level / 15) {
				id = 2;
			} else if (r < 383 * level / 15) {
				id = 1;
			}
			SwordList [i] = new Equip ();
			SwordList [i].set (id, Random.Range (0, 16), 15, Random.Range (0, 16), 15);
			SwordIsNothing [i] = false;
		}
		for (int i = 0; i < 6; i++)
		{
			int r = Random.Range (0, 256);
			int level = GameMaster.Instance.level;
			int id = 8;
			if (r < 3 * level / 15) {
				id = 12;
			} else if (r < 65 * level / 15) {
				id = 11;
			} else if (r < 191 * level / 15) {
				id = 10;
			} else if (r < 383 * level / 15) {
				id = 9;
			}
			ShieldList [i] = new Equip ();
			ShieldList [i].set (id, 15, Random.Range (0, 16), 15, Random.Range (0, 16));
			ShieldIsNothing [i] = false;
		}
	}

	public bool isAvailable(Type type, int id)
	{
		switch (type) {
		case TownManager.Type.Sword:
			if (TownManager.Instance.SwordIsNothing [id])
			{
				return false;
			}
			else if (GameMaster.Instance.gold + GameMaster.Instance.equip.Sword.gold / 2 - SwordList [id].gold < 0)
			{
				return false;
			}
			break;
		case TownManager.Type.Shield:
			if (TownManager.Instance.ShieldIsNothing [id])
			{
				return false;
			}
			else if (GameMaster.Instance.gold + GameMaster.Instance.equip.Shield.gold / 2 - ShieldList [id].gold < 0)
			{
				return false;
			}
			break;
		case TownManager.Type.Item:
			if (GameMaster.Instance.itemNum [id] >= 99 || GameMaster.Instance.gold < itemPrice [id])
			{
				return false;
			}
			break;
		case TownManager.Type.SellItem:
			if (GameMaster.Instance.itemNum [id] <= 0)
			{
				return false;
			}
			break;
		default:
			return false;
			break;
		}
		return true;
	}

	public void buySwordButtonClicked(int id)
	{
		Equip e = GameMaster.Instance.equip.Sword;
		int gold = e.gold / 2 - SwordList [id].gold;
		int da = SwordList [id].atk - e.atk;
		int dh = SwordList [id].hit - e.hit;
		DialogManager.Instance.message (
			string.Format ("現在の 装備を 売却して\n" +
				"{0}を 購入します。({1} G)\n" +
				"Atk:{2,3}  -> {3,3}  ({4,4} )\n" +
				"Hit:{5,3}% -> {6,3}% ({7,4}%)",
				SwordList [id].name, gold.ToString("+#;-#;0"),
				e.atk, SwordList [id].atk, da.ToString("+#;-#;0"),
				e.hit, SwordList [id].hit, dh.ToString("+#;-#;0"))
		);
		StartCoroutine (waitForBuySword (id));
	}

	IEnumerator waitForBuySword(int id)
	{
		while(DialogManager.Instance.getAnswer() == DialogManager.Answer.NA)
		{
			yield return 0;
		}
		if (DialogManager.Instance.getAnswer () == DialogManager.Answer.Yes)
		{
			GameMaster.Instance.gold += GameMaster.Instance.equip.Sword.gold / 2 - SwordList [id].gold;
			GameMaster.Instance.equip.Sword.set(SwordList [id]);
			GameMaster.Instance.calcParam ();
			SwordIsNothing [id] = true;
		}
	}

	public void buyShieldButtonClicked(int id)
	{
		Equip e = GameMaster.Instance.equip.Shield;
		int gold = e.gold / 2 - ShieldList [id].gold;
		int da = ShieldList [id].def - e.def;
		int dh = ShieldList [id].eva - e.eva;
		DialogManager.Instance.message (
			string.Format ("現在の 装備を 売却して\n" +
				"{0}を 購入します。({1} G)\n" +
				"Def:{2,3}  -> {3,3}  ({4,4} )\n" +
				"Eva:{5,3}% -> {6,3}% ({7,4}%)",
				ShieldList [id].name, gold.ToString("+#;-#;0"),
				e.def, ShieldList [id].def, da.ToString("+#;-#;0"),
				e.eva, ShieldList [id].eva, dh.ToString("+#;-#;0"))
		);
		StartCoroutine (waitForBuyShield (id));
	}

	IEnumerator waitForBuyShield(int id)
	{
		while(DialogManager.Instance.getAnswer() == DialogManager.Answer.NA)
		{
			yield return 0;
		}
		if (DialogManager.Instance.getAnswer () == DialogManager.Answer.Yes)
		{
			GameMaster.Instance.gold += GameMaster.Instance.equip.Shield.gold / 2 - ShieldList [id].gold;
			GameMaster.Instance.equip.Shield.set(ShieldList [id]);
			GameMaster.Instance.calcParam ();
			ShieldIsNothing [id] = true;
		}
	}

	public void buyItemButtonClicked(int id)
	{
		GameMaster.Instance.itemNum [id]++;
		GameMaster.Instance.gold -= itemPrice [id];
	}

	public void sellItemButtonClicked(int id)
	{
		GameMaster.Instance.itemNum [id]--;
		GameMaster.Instance.gold += itemPrice [id] / 2;
	}
	
	public void GoToDungeonButtonClicked()
	{
		FadeManager.Instance.LoadLevel ("Dungeon", 0.5f);
	}
}
