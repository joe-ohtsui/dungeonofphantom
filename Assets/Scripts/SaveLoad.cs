using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad : SingletonMonoBehaviour<SaveLoad> {
	// Use this for initialization
	void Start ()
	{
		if (this != Instance)
		{
			Destroy (this);
			return;
		}
	}


	public void save()
	{
		string folderpath = Application.dataPath + "/Database/";
		string filepath = folderpath + "save.json";
		string json = GameMaster.Instance.toJson ();
		string crypted = Crypt.Encrypt (json);
		if (!Directory.Exists (folderpath))
		{
			Directory.CreateDirectory(folderpath);
		}

		FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
		BinaryWriter writer = new BinaryWriter(fileStream);
		writer.Write(crypted);
		writer.Close();
		Debug.Log ("セーブしました");
	}

	public void load()
	{
		string folderpath = Application.dataPath + "/Database/";
		string filepath = folderpath + "save.json";
		string json = "";

		if (File.Exists (filepath))
		{
			FileStream fileStream = new FileStream (filepath, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader (fileStream);
			if (reader != null)
			{
				string str = reader.ReadString ();
				string decrypted = Crypt.Decrypt (str);
				json = decrypted;
				GameMaster.Instance.fromJson (json);
				reader.Close ();

				Debug.Log ("ロードしました");
			}
		}
	}
}
