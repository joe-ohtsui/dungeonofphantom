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
		AudioManager.Instance.playSE (0);
		c.enabled = !c.enabled;
	}

	public void EnableCanvas()
	{
		AudioManager.Instance.playSE (0);
		c.enabled = true;
	}

	public void DisableCanvas()
	{
		AudioManager.Instance.playSE (0);
		c.enabled = false;
	}
}
