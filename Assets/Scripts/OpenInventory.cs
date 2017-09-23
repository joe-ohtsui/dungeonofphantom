using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour {

	Canvas c;

	void Awake()
	{
		c = GetComponent<Canvas>();
		c.enabled = false;
	}

	public void OnClick()
	{
		c.enabled = !c.enabled;
	}
}
