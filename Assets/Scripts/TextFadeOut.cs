using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour {

	private Text targetText;
	private float g;
	private float b;
	private float a;

	// Use this for initialization
	void Start ()
	{
		targetText = GetComponent<Text> ();
		g = 1.0f;
		b = 1.0f;
		a = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		targetText.color = new Color(1.0f, 1.0f, 1.0f, targetText.color.a - 0.05f);
		transform.position += new Vector3 (0.0f, 0.005f, 0.0f);
		if (targetText.color.a < 0.0f)
		{
			targetText.enabled = false;
		}
	}
}
