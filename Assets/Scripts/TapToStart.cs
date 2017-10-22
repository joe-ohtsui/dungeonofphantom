using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStart : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		AudioManager.Instance.playBGM (0);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnClicked()
	{
		AudioManager.Instance.playSE (1);
		GameMaster.Instance.load ();
		FadeManager.Instance.LoadLevel("Town",0.5f);
	}
}
