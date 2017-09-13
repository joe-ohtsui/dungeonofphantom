using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSwitch : MonoBehaviour {

	private Slider slider;
	private RawImage image;
	int flag = 0;

	// Use this for initialization
	void Start ()
	{
		slider = transform.Find("Slider").GetComponent<Slider>();
		image = GameObject.Find ("DungeonMap").GetComponent<RawImage> ();
	}

	public void OnClick()
	{
		flag = (flag + 1) % 2;
		slider.value = flag;
		image.enabled = (flag == 1);
	}
}
