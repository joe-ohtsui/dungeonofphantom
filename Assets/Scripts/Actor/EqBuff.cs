using UnityEngine;
using System.Collections;

public class EqBuff
{
    public Equip Sword;
    public Equip Shield;
    public int atkForce;
    public int defForce;
    public int hitForce;
    public int evaForce;
    
	public EqBuff ()
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
		Sword.set(0, 0, 15, 0, 15);
		Shield.set(8, 15, 0, 15, 0);
    }
}
