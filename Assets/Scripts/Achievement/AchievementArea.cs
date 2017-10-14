using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementArea : MonoBehaviour {

	Canvas targetcanvas;

	// Use this for initialization
	void Start ()
	{
		targetcanvas = GetComponent<Canvas> ();
	}
	
	public void OnClick()
	{
		targetcanvas.enabled = !targetcanvas.enabled;
	}
}
