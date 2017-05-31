using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Actor player;

    void Start ()
    {
        player = GetComponent<Actor>();
    }

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;

	void Flick()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0))
		{
			touchStartPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		}
		if (Input.GetKeyUp (KeyCode.Mouse0))
		{
			touchEndPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			GetDirection ();
		}
	}

	void GetDirection()
	{
		float dx = touchEndPos.x - touchStartPos.x;
		float dy = touchEndPos.y - touchStartPos.y;

		if (Mathf.Abs (dy) < Mathf.Abs (dx))
		{
			if (30 < dx)
			{
				//右向きにフリックされた
				player.setDest (player.pos.toLeft ());
			}
			else if (-30 > dx)
			{
				//左向きにフリックされた
				player.setDest (player.pos.toRight ());	
			}
		}
		else if (Mathf.Abs (dx) < Mathf.Abs (dy))
		{
			if (30 < dy)
			{
				//上向きにフリックされた
				player.setDest (player.pos.move ((player.pos.direction + 2) % 4));
			}
			else if (-30 > dy)
			{
				//下向きにフリックされた
				player.setDest (player.pos.move (player.pos.direction));
			}
		}
		else
		{
			//タップされた
			player.setDest (player.pos.move (player.pos.direction));
		}
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
			Flick ();
        }
        transform.LookAt(player.getLookAt());
    }
    
}
