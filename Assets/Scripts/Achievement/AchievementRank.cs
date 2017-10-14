using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementRank : MonoBehaviour {
	public int id;
	Image targetpanel;
	Image targeticon;
	Image unread;

	// Use this for initialization
	void Start ()
	{
		targetpanel = GetComponent<Image> ();
		targeticon = transform.GetChild(0).GetComponent<Image> ();
		unread = transform.GetChild (1).GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (AchievementManager.Instance.rank [id]) 
		{
		case 0:
			targetpanel.color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			targeticon.enabled = false;
			break;
		case 1:
			targetpanel.color = new Color (0.75f, 0.25f, 0.0f, 0.5f);
			targeticon.enabled = true;
			break;
		case 2:
			targetpanel.color = new Color (0.75f, 0.75f, 0.75f, 0.5f);
			targeticon.enabled = true;
			break;
		case 3:
			targetpanel.color = new Color (1.0f, 1.0f, 0.25f, 0.5f);
			targeticon.enabled = true;
			break;
		case 4:
			targetpanel.color = new Color (0.5f, 0.75f, 1.0f, 0.5f);
			targeticon.enabled = true;
			break;
		default:
			break;
		}

		unread.enabled = AchievementManager.Instance.unread [id];

	}
}
