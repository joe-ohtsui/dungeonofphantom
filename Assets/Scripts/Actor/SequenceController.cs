using UnityEngine;
using System.Collections;

public class SequenceController : MonoBehaviour
{
    private Actor player;
    
    void Start ()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
    }
    
    void Update()
    {
        switch (player.actphase)
        {
            case Actor.Phase.MOVE_START:
                CallEnemiesAI();
                player.actphase = Actor.Phase.MOVE_NOW;
                break;
            case Actor.Phase.ATTACK_START:
                CallEnemiesAI();
                player.actphase = Actor.Phase.ATTACK_NOW;
                break;
            case Actor.Phase.TURN_END:
                TurnEnd();
                break;
            default:
                break;
        }
    }

    void CallEnemiesAI()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Actor");
        foreach(GameObject g in list)
        {
            g.GetComponent<EnemyAI>().Think();
        }
    }

    void TurnEnd()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Actor");
        bool endFlag = true;
        foreach (GameObject g in list)
        {
            Actor a = g.GetComponent<Actor>();
            if (a.actphase != Actor.Phase.TURN_END && a.actphase != Actor.Phase.KEY_WAIT)
            {
                endFlag = false;
            }
        }
        if (endFlag)
        {
            player.actphase = Actor.Phase.KEY_WAIT;
            foreach (GameObject g in list)
            {
                g.GetComponent<Actor>().actphase = Actor.Phase.KEY_WAIT;
            }
        }
    }
}
