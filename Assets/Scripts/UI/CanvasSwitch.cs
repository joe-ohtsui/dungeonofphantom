using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwitch : MonoBehaviour {

	Canvas c;

	void Awake()
	{
		c = GetComponent<Canvas>();
	}

	public void Switch()
	{
		c.enabled = !c.enabled;
	}

	public void EnableCanvas()
	{
		c.enabled = true;
	}

	public void DisableCanvas()
	{
		c.enabled = false;
	}
}
