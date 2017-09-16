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
		targetText.color = new Color(1.0f, 1.0f, 1.0f, targetText.color.a - 0.05f);
		transform.position += new Vector3 (0.0f, 0.005f, 0.0f);
	}
}
