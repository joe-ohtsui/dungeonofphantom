using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	GameObject player;
	Actor pla;
	bool buttonClicked = false;
	bool moveForwardFlag = false;
	bool moveBackwardFlag = false;
	bool moveLeftFlag = false;
	bool moveRightFlag = false;
	bool turnLeftFlag = false;
	bool turnRightFlag = false;
	bool attackFlag = false;
	int count = 0;

    void Start ()
    {
		player = GameObject.FindWithTag ("Player");
		pla = player.GetComponent<Actor>();
    }

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;

	void Flick()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0))
		{
			touchStartPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		}
		if (Input.GetKeyUp (KeyCode.Mouse0) || count > 130)
		{
			touchEndPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			GetDirection ();
		}
		if (Input.GetKey (KeyCode.Mouse0))
		{
			count += (int)(600 * Time.deltaTime);
		}
		else
		{
			count = 0;
		}
	}

	void GetDirection()
	{
		float dx = touchEndPos.x - touchStartPos.x;
		float dy = touchEndPos.y - touchStartPos.y;

		if (Mathf.Abs (dy) < Mathf.Abs (dx))
		{
			if (40 < dx)
			{
				//右向きにフリックされた
				turnLeftFlag = true;
			}
			else if (-40 > dx)
			{
				//左向きにフリックされた
				turnRightFlag = true;
			}
			else
			{
				moveForwardFlag = true;
			}
		}
		else
		{
			if (40 < dy)
			{
				//上向きにフリックされた
				moveBackwardFlag = true;
			}
			else
			{
				moveForwardFlag = true;
			}
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

	public void UseItemButtonClicked(int itemID)
	{
		Param p = player.GetComponent<Param> ();
		EqBuff equip = GameMaster.Instance.equip;
		if (GameMaster.Instance.itemNum [itemID] > 0)
		{
			switch (itemID)
			{
			case 0:
				p.hp += 80;
				if (p.hp > p.maxHp)
				{
					p.hp = p.maxHp;
				}
				LogManager.Instance.PutLog("Rec Potionを 使った");
				break;
			case 1:
				equip.atkForce = 11;
				LogManager.Instance.PutLog("Atk Potionを 使った");
				break;
			case 2:
				equip.defForce = 11;
				LogManager.Instance.PutLog("Def Potionを 使った");
				break;
			case 3:
				equip.hitForce = 11;
				LogManager.Instance.PutLog("Hit Potionを 使った");
				break;
			case 4:
				equip.evaForce = 11;
				LogManager.Instance.PutLog("Eva Potionを 使った");
				break;
			default:
				break;
			}
			GameMaster.Instance.itemNum [itemID]--;
			GameMaster.Instance.calcParam ();
			pla.actphase = Actor.Phase.MOVE_START;
		}
		buttonClicked = true;
	}

	void TurnLeft()
	{
		pla.setDest(pla.pos.toLeft());
	}

	void TurnRight()
	{
		pla.setDest(pla.pos.toRight());
	}

	void MoveForward()
	{
		pla.setDest(pla.pos.move(pla.pos.direction));
	}

	void MoveBackward()
	{
		pla.setDest(pla.pos.move((pla.pos.direction + 2) % 4));
	}

	void MoveLeft()
	{
		pla.setDest(pla.pos.move((pla.pos.direction + 3) % 4));
	}

	void MoveRight()
	{
		pla.setDest(pla.pos.move((pla.pos.direction + 1) % 4));
	}

	void Attack()
	{
		pla.actphase = Actor.Phase.ATTACK_START;
	}
	
	void Update ()
    {
		Flick ();

        if (pla.actphase == Actor.Phase.KEY_WAIT && !buttonClicked)
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
			else if (moveLeftFlag)
			{
				MoveLeft ();
			}
			else if (moveRightFlag)
			{
				MoveRight ();
			}
			else if (Input.GetKey (KeyCode.X) || attackFlag)
			{
				Attack ();
				attackFlag = false;
			}
//			else
//			{
//				Flick ();
//			}
        }
		buttonClicked = false;
        player.transform.LookAt(pla.getLookAt());
    }
    
}
