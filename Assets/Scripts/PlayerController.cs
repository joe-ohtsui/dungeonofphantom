using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Actor player;

    void Start ()
    {
        player = GetComponent<Actor>();
    }
	
	void Update ()
    {
        if (player.actphase == Actor.Phase.KEY_WAIT)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                player.setDest(player.pos.toLeft());
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                player.setDest(player.pos.toRight());
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                player.setDest(player.pos.move(player.pos.direction));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                player.setDest(player.pos.move((player.pos.direction + 2) % 4));
            }
            else if (Input.GetKey(KeyCode.X))
            {
                player.actphase = Actor.Phase.ATTACK_START;
            }
        }
        transform.LookAt(player.getLookAt());
    }
    
}
