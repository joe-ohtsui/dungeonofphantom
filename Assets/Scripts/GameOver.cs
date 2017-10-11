using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
	private Param param;

	// Use this for initialization
	void Start ()
	{
		param = GameObject.FindWithTag("Player").GetComponent<Param>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (param.hp == 0)
		{
			GetComponent<Canvas> ().enabled = true;
		}
	}

	public void OnClicked()
	{
		FadeManager.Instance.LoadLevel("Title",1.5f);
	}
}
