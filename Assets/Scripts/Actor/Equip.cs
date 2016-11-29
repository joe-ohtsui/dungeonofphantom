﻿using UnityEngine;
using System.Collections;

public class Equip
{
    public string name;
    public int id;
    public int atk;
    public int def;
    public int hit;
    public int eva;
    public int gold;
    public int atkIndividual;
    public int defIndividual;
    public int hitIndividual;
    public int evaIndividual;

    public Equip(int _id = 0, int _atkIndividual = 15, int _defIndividual = 15, int _hitIndividual = 15, int _evaIndividual = 15)
    {
        set(_id, _atkIndividual, _defIndividual, _hitIndividual, _evaIndividual);
    }

    public void set(int _id = 0, int _atkIndividual = 15, int _defIndividual = 15, int _hitIndividual = 15, int _evaIndividual = 15)
    {
        id = _id;
        atkIndividual = _atkIndividual;
        defIndividual = _defIndividual;
        hitIndividual = _hitIndividual;
        evaIndividual = _evaIndividual;
        calc();
    }

    public void set(Equip equip)
    {
        set(equip.id, equip.atkIndividual, equip.defIndividual, equip.hitIndividual, equip.evaIndividual);
    }

    void calc()
    {
        atk = 0;
        def = 0;
        hit = 0;
        eva = 0;
        switch(id)
        {

            case 0:
                name = "ShortSword";
                atk = 50;
                hit = 70;
                gold = 3;
                break;
            case 1:
                name = "LongSword";
                atk = 71;
                hit = 85;
                gold = 7;
                break;
            case 2:
                name = "SilverSword";
                atk = 93;
                hit = 100;
                gold = 15;
                break;
            case 3:
                name = "Claymore";
                atk = 114;
                hit = 115;
                gold = 31;
                break;
            case 4:
                name = "Excalibur";
                atk = 150;
                hit = 140;
                gold = 63;
                break;
            case 8:
                name = "Buckler";
                def = 25;
                eva = 10;
                gold = 2;
                break;
            case 9:
                name = "RoundShield";
                def = 36;
                eva = 23;
                gold = 6;
                break;
            case 10:
                name = "KiteShield";
                def = 46;
                eva = 36;
                gold = 16;
                break;
            case 11:
                name = "TowerShield";
                def = 57;
                eva = 49;
                gold = 34;
                break;
            case 12:
                name = "Aegis";
                def = 75;
                eva = 70;
                gold = 70;
                break;
            default:
                name = "";
                break;
        }
        atk = atk * (atkIndividual + 30) / 38;
        def = def * (defIndividual + 30) / 38;
        hit = hit * (hitIndividual + 30) / 38;
        eva = eva * (evaIndividual + 30) / 38;
        gold = gold * (atkIndividual + 85) / 10
            * (defIndividual + 85) / 10
            * (hitIndividual + 85) / 10
            * (evaIndividual + 85) / 10 / 100;
    }
}
