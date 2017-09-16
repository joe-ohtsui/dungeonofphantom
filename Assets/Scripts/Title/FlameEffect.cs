using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour {

	private int count;

	// Use this for initialization
	void Start () {
		count = 0;
	}

	// Update is called once per frame
	void Update () {
//		float alpha = Mathf.Sin (count * Mathf.PI / 240.0f) / 2.0f + 0.5f;
		count++;
	}
}
