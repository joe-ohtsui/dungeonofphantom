using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonCheck : MonoBehaviour {

	public int ID;
	public TownManager.Type type;
	private Button button;

	// Use this for initialization
	void Start ()
	{
		button = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		button.enabled = TownManager.Instance.isAvailable (type, ID);
	}
}
