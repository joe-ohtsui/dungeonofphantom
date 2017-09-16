using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Actor player;
	bool buttonClicked = false;
	bool moveForwardFlag = false;
	bool moveBackwardFlag = false;
	bool moveLeftFlag = false;
	bool moveRightFlag = false;
	bool turnLeftFlag = false;
	bool turnRightFlag = false;
	bool attackFlag = false;

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
				TurnLeft();
			}
			else if (-30 > dx)
			{
				//左向きにフリックされた
				TurnRight();
			}
		}
		else if (Mathf.Abs (dx) < Mathf.Abs (dy))
		{
			if (30 < dy)
			{
				//上向きにフリックされた
				MoveBackward ();
			}
			else if (-30 > dy)
			{
				//下向きにフリックされた
				MoveForward();
			}
		}
		else
		{
			//タップされた
			MoveForward();
		}
	}

	public void AnyOtherButtonClicked()
	{
		buttonClicked = true;
	}

	public void TurnLeftButtonDown()
	{
		turnLeftFlag = true;
	}

	public void TurnLeftButtonUp()
	{
		turnLeftFlag = false;
		buttonClicked = true;
	}

	public void TurnRightButtonDown()
	{
		turnRightFlag = true;
	}

	public void TurnRightButtonUp()
	{
		turnRightFlag = false;
		buttonClicked = true;
	}

	public void MoveForwardButtonDown()
	{
		moveForwardFlag = true;
	}

	public void MoveForwardButtonUp()
	{
		moveForwardFlag = false;
		buttonClicked = true;
	}

	public void MoveBackwardButtonDown()
	{
		moveBackwardFlag = true;
	}

	public void MoveBackwardButtonUp()
	{
		moveBackwardFlag = false;
		buttonClicked = true;
	}

	public void MoveLeftButtonDown()
	{
		moveLeftFlag = true;
	}

	public void MoveLeftButtonUp()
	{
		moveLeftFlag = false;
		buttonClicked = true;
	}

	public void MoveRightButtonDown()
	{
		moveRightFlag = true;
	}

	public void MoveRightButtonUp()
	{
		moveRightFlag = false;
		buttonClicked = true;
	}

	public void AttackButtonClicked()
	{
		attackFlag = true;
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
			if (Input.GetKey (KeyCode.LeftArrow) || turnLeftFlag)
			{
				TurnLeft ();
			}
			else if (Input.GetKey (KeyCode.RightArrow) || turnRightFlag)
			{
				TurnRight ();
			}
			else if (Input.GetKey (KeyCode.UpArrow) || moveForwardFlag)
			{
				MoveForward ();
			}
			else if (Input.GetKey (KeyCode.DownArrow) || moveBackwardFlag)
			{
				MoveBackward ();
			}
			else if (Input.GetKey (KeyCode.X) || attackFlag)
			{
				Attack ();
				attackFlag = false;
			}
			else
			{
				Flick ();
			}
        }
		buttonClicked = false;
        transform.LookAt(player.getLookAt());
    }
    
}
