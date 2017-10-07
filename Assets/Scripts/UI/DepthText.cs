using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DepthText : MonoBehaviour {

	private Text targetText;

	// Use this for initialization
	void Start ()
	{
		targetText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		targetText.text = DungeonManager.Instance.depth + "F Map";
	}
}
