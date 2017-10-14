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
		save2 (GameMaster.Instance.toJson());
	}

	public void load()
	{
		GameMaster.Instance.fromJson (load2());
	}

	public void gameover()
	{
		string json = load2();
		SaveData data = JsonUtility.FromJson<SaveData> (json);
		data.a [15]++;
		if (data.a[15] > 9999)
		{
			data.a[15] = 9999;
		}
		save2 (JsonUtility.ToJson (data));
	}

	void save2(string json)
	{
		string folderpath = Application.temporaryCachePath + "/Database/";
		string filepath = folderpath + "save.json";
		string crypted = Crypt.Encrypt (json);
		if (!Directory.Exists (folderpath))
		{
			Directory.CreateDirectory(folderpath);
		}

		FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
		BinaryWriter writer = new BinaryWriter(fileStream);
		writer.Write(crypted);
		writer.Close();
	}

	string load2()
	{
		string folderpath = Application.temporaryCachePath + "/Database/";
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
				reader.Close ();
			}
		}

		return json;
	}
}
