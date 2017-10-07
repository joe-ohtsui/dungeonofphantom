using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPop : MonoBehaviour {

	int count;
	int MAXCOUNT = 150;

	// Use this for initialization
	void Start () {
		count = 0;
	}

	public void OnClicked()
	{
		count = MAXCOUNT;
		StartCoroutine (Pop ());
	}

	IEnumerator Pop()
	{
		RectTransform rt = GetComponent<RectTransform> ();
		while (count > 0)
		{
			float s = 1.0f + 0.3f * count / MAXCOUNT;
			rt.localScale = new Vector3 (s, s, 1.0f);
			count -= (int)(600 * Time.deltaTime);
			yield return 0;
		}
		rt.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
	}
}
