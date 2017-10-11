using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStart : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Mouse0))
		{
			GameMaster.Instance.load ();
			FadeManager.Instance.LoadLevel("Town",0.5f);
		}
	}
}
