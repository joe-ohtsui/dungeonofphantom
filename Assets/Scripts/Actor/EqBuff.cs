using UnityEngine;
using System.Collections;

public class EqBuff : MonoBehaviour
{
    public Equip Sword;
    public Equip Shield;
    public int atkForce;
    public int defForce;
    public int hitForce;
    public int evaForce;
    
    void Start ()
    {
        Sword = new Equip();
        Shield = new Equip();
        init();
	}

    public void init()
    {
        atkForce = 0;
        defForce = 0;
        hitForce = 0;
        evaForce = 0;
		Sword.set(4, 15, 15, 15, 15);
		Shield.set(12, 15, 15, 15, 15);
//		Sword.set(0, 0, 15, 0, 15);
//		Shield.set(8, 15, 0, 15, 0);
        calcParam();
    }

    public void aging()
    {
        if (atkForce > 0) { atkForce--; }
        if (defForce > 0) { defForce--; }
        if (hitForce > 0) { hitForce--; }
        if (evaForce > 0) { evaForce--; }
        calcParam();
    }

    public void calcParam()
    {
        Param param = GetComponent<Param>();
        param.atk = Sword.atk * (100 + atkForce * 7) / 100;
        param.def = Shield.def * (100 + defForce * 6) / 100;
        param.hit = Sword.hit * (100 + hitForce * 9) / 100;
        param.eva = Shield.eva * (100 + evaForce * 8) / 100;
    }
}
