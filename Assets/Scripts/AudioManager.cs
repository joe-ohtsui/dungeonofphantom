using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager> {

	public AudioClip[] bgm;
	public AudioClip[] se;
	private AudioSource[] sources;
	int bgmid = -1;

	// Use this for initialization
	void Start ()
	{
		if (this != Instance)
		{
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);

		sources = gameObject.GetComponents<AudioSource> ();
	}

	public void playBGM(int id)
	{
		if (id != bgmid)
		{
			sources [0].clip = bgm [id];
			sources [0].loop = true;
			sources [0].Play ();
			bgmid = id;
		}
	}

	public void stopBGM()
	{
		sources [0].Stop ();
	}

	public void playSE(int id)
	{
		sources [1].clip = se [id];
		sources [1].loop = false;
		sources [1].Play ();
	}

}
