using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour {

	private Text targetText;

	// Use this for initialization
	void Start ()
	{
		targetText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		targetText.color = new Color (1.0f, 1.0f, 1.0f, targetText.color.a - 2.0f * Time.deltaTime);
		if (targetText.color.a < 0.0f)
		{
			targetText.enabled = false;
		}
	}
}
