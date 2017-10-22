using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	GameObject player;
	Actor pla;
	bool flickFlag = false;
	bool moveForwardFlag = false;
	bool moveBackwardFlag = false;
	bool moveLeftFlag = false;
	bool moveRightFlag = false;
	bool turnLeftFlag = false;
	bool turnRightFlag = false;
	bool attackFlag = false;
	int count = 0;

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;

    void Start ()
    {
		player = GameObject.FindWithTag ("Player");
		pla = player.GetComponent<Actor>();
    }

	void Flick()
	{
		Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		float dx = mousePos.x - touchStartPos.x;
		float dy = mousePos.y - touchStartPos.y;

		if (Mathf.Abs (dy) < Mathf.Abs (dx))
		{
			if (20 < dx)
			{
				//右向きにフリックされた
				TurnLeft();
			}
			else if (-20 > dx)
			{
				//左向きにフリックされた
				TurnRight();
			}
			else
			{
				MoveForward ();
			}
		}
		else
		{
			if (20 < dy)
			{
				//上向きにフリックされた
				MoveBackward ();
			}
			else
			{
				MoveForward();
			}
		}
	}

	public void FlickStart()
	{
		touchStartPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		flickFlag = true;
	}

	public void FlickEnd()
	{
		if (pla.actphase == Actor.Phase.KEY_WAIT)
		{
			Flick ();
		}
		flickFlag = false;
		count = 0;
	}

	public void TurnLeftButtonDown()
	{
		turnLeftFlag = true;
	}

	public void TurnLeftButtonUp()
	{
		turnLeftFlag = false;
	}

	public void TurnRightButtonDown()
	{
		turnRightFlag = true;
	}

	public void TurnRightButtonUp()
	{
		turnRightFlag = false;
	}

	public void MoveForwardButtonDown()
	{
		moveForwardFlag = true;
	}

	public void MoveForwardButtonUp()
	{
		moveForwardFlag = false;
	}

	public void MoveBackwardButtonDown()
	{
		moveBackwardFlag = true;
	}

	public void MoveBackwardButtonUp()
	{
		moveBackwardFlag = false;
	}

	public void MoveLeftButtonDown()
	{
		moveLeftFlag = true;
	}

	public void MoveLeftButtonUp()
	{
		moveLeftFlag = false;
	}

	public void MoveRightButtonDown()
	{
		moveRightFlag = true;
	}

	public void MoveRightButtonUp()
	{
		moveRightFlag = false;
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
			AudioManager.Instance.playSE (5);
		}
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
		if (flickFlag)
		{
			count += (int)(600 * Time.deltaTime);
		}

		if (pla.actphase == Actor.Phase.KEY_WAIT)
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
			else if (count > 130)
			{
				Flick ();
			}
			else if (Input.GetKey (KeyCode.X) || attackFlag)
			{
				Attack ();
				attackFlag = false;
			}
        }
        player.transform.LookAt(pla.getLookAt());
    }
    
}
