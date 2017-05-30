using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusDrawer : MonoBehaviour
{
    public enum Content
    {
        Hp,
        Level,
        Sword,
        Shield
    }

    public Content content;
    private Text text;
    private Param param;
    private EqBuff equip;

    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text>();
        param = GameObject.FindWithTag("Player").GetComponent<Param>();
        equip = GameObject.FindWithTag("Player").GetComponent<EqBuff>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch(content)
        {
            case Content.Hp:
                text.text = string.Format("HP:{0,3}/{1,3}", param.hp, param.maxHp);
                break;
            case Content.Level:
                text.text = string.Format("Lv:{0,2}   Exp:", param.level, param.exp);
                break;
            case Content.Sword:
                text.text = string.Format("{0,-12} Atk:{1,3} Hit:{2,3}%", equip.Sword.name, param.atk, param.hit);
                break;
            case Content.Shield:
                text.text = string.Format("{0,-12} Def:{1,3} Eva:{2,3}%", equip.Shield.name, param.def, param.eva);
                break;
            default:
                break;
        }
	}
}
