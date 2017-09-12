using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Actor player;
	bool buttonClicked = false;

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

	public void TurnLeftButtonClicked()
	{
		buttonClicked = true;
		if (player.actphase == Actor.Phase.KEY_WAIT)
		{
			TurnLeft ();
		}
	}

	public void TurnRightButtonClicked()
	{
		buttonClicked = true;
		if (player.actphase == Actor.Phase.KEY_WAIT)
		{
			TurnRight ();
		}
	}

	public void MoveForwardButtonClicked()
	{
		buttonClicked = true;
		if (player.actphase == Actor.Phase.KEY_WAIT)
		{
			MoveForward ();
		}
	}

	public void MoveBackwardButtonClicked()
	{
		buttonClicked = true;
		if (player.actphase == Actor.Phase.KEY_WAIT)
		{
			MoveBackward ();
		}
	}

	public void MoveLeftButtonClicked()
	{
		buttonClicked = true;
		if (player.actphase == Actor.Phase.KEY_WAIT)
		{
			MoveLeft ();
		}
	}

	public void MoveRightButtonClicked()
	{
		buttonClicked = true;
		if (player.actphase == Actor.Phase.KEY_WAIT)
		{
			MoveRight ();
		}
	}

	public void AttackButtonClicked()
	{
		buttonClicked = true;
		if (player.actphase == Actor.Phase.KEY_WAIT)
		{
			Attack ();
		}
	}

	void TurnLeft()
	{
		player.setDest(player.pos.toLeft());
	}

	void TurnRight()
	{
		player.setDest(player.pos.toRight());
	}

	void MoveForward()
	{
		player.setDest(player.pos.move(player.pos.direction));
	}

	void MoveBackward()
	{
		player.setDest(player.pos.move((player.pos.direction + 2) % 4));
	}

	void MoveLeft()
	{
		player.setDest(player.pos.move((player.pos.direction + 3) % 4));
	}

	void MoveRight()
	{
		player.setDest(player.pos.move((player.pos.direction + 1) % 4));
	}

	void Attack()
	{
		player.actphase = Actor.Phase.ATTACK_START;
	}
	
	void Update ()
    {
        if (player.actphase == Actor.Phase.KEY_WAIT && !buttonClicked)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
				TurnLeft ();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
				TurnRight ();
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
				MoveForward ();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
				MoveBackward ();
            }
            else if (Input.GetKey(KeyCode.X))
            {
				Attack ();
            }
			Flick ();
        }
		buttonClicked = false;
        transform.LookAt(player.getLookAt());
    }
    
}
