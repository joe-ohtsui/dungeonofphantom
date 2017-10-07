using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : SingletonMonoBehaviour<DialogManager> {

	public GameObject dialogBox;
	GameObject dbInstance;
	Answer answer;

	public enum Answer{
		Yes,
		No,
		NA
	}

	// Use this for initialization
	void Start ()
	{
		if (this != Instance)
		{
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void message(string text)
	{
		GameObject o = Instantiate (dialogBox, this.transform) as GameObject;
		o.GetComponentInChildren<Text> ().text = text;
		answer = Answer.NA;
	}

	public void YesButtonClicked()
	{
		DialogManager.Instance.answer = Answer.Yes;
		foreach (Transform n in DialogManager.Instance.transform)
		{
			GameObject.Destroy (n.gameObject);
		}
	}

	public void NoButtonClicked()
	{
		DialogManager.Instance.answer = Answer.No;
		foreach (Transform n in DialogManager.Instance.transform)
		{
			GameObject.Destroy (n.gameObject);
		}
	}

	public Answer getAnswer()
	{
		return answer;
	}
}
