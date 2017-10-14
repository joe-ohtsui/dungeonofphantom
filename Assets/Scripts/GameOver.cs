using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
	private Actor player;
	private Param param;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag("Player").GetComponent<Actor>();
		param = GameObject.FindWithTag("Player").GetComponent<Param>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (param.hp == 0)
		{
			GetComponent<Canvas> ().enabled = true;
			player.actphase = Actor.Phase.DEAD;
		}
	}

	public void OnClicked()
	{
		SaveLoad.Instance.gameover ();
		FadeManager.Instance.LoadLevel("Title",1.5f);
	}
}
