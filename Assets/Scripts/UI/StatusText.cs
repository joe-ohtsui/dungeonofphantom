using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusText : MonoBehaviour
{
    public enum Entry
    {
        Hp,
        Level,
        Sword,
        Shield,
		Gold
    }

    public Entry entry;
    private Text text;
    private Param param;

    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text>();
        param = GameObject.FindWithTag("Player").GetComponent<Param>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch (entry)
		{
		case Entry.Hp:
			text.text = string.Format ("HP:{0,3}/{1,3}", param.hp, param.maxHp);
			break;
		case Entry.Level:
			text.text = string.Format ("Lv:{0,2}   Exp:", param.level);
			break;
		case Entry.Sword:
			text.text = string.Format ("{0,-12} Atk:{1,3} Hit:{2,3}%", GameMaster.Instance.equip.Sword.name, param.atk, param.hit);
			break;
		case Entry.Shield:
			text.text = string.Format ("{0,-12} Def:{1,3} Eva:{2,3}%", GameMaster.Instance.equip.Shield.name, param.def, param.eva);
			break;
		case Entry.Gold:
			text.text = string.Format ("{0,7}", GameMaster.Instance.gold);
			break;
		default:
			break;
		}
	}
}
