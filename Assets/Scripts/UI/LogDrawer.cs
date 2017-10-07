using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogDrawer : MonoBehaviour {

	private Text targetText;

	// Use this for initialization
	void Start ()
	{
		targetText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		targetText.text = LogManager.Instance.GetLog ();
	}
}
