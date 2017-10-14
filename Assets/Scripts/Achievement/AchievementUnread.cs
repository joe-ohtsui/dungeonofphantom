using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUnread : MonoBehaviour {
	Image targetimage;

	// Use this for initialization
	void Start ()
	{
		targetimage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		targetimage.enabled = false;
		for (int i = 0; i < 16; i++)
		{
			if (AchievementManager.Instance.unread [i])
			{
				targetimage.enabled = true;
			}
		}
	}
}
