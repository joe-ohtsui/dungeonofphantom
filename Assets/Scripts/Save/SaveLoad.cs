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
		string folderpath = Application.temporaryCachePath + "/Database/";
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
	}

	public void load()
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
				GameMaster.Instance.fromJson (json);
				reader.Close ();
			}
		}
	}

	public void gameover()
	{
		string folderpath = Application.temporaryCachePath + "/Database/";
		string filepath = folderpath + "save.json";

		if (File.Exists (filepath))
		{
			FileStream rfileStream = new FileStream (filepath, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader (rfileStream);
			if (reader != null)
			{
				string str = reader.ReadString ();
				string decrypted = Crypt.Decrypt (str);
				reader.Close ();

				SaveData data = JsonUtility.FromJson<SaveData> (decrypted);
				data.a [15]++;
				if (data.a[15] > 9999)
				{
					data.a [15] = 9999;
				}
				string json = JsonUtility.ToJson (data);

				string crypted = Crypt.Encrypt (json);
				if (!Directory.Exists (folderpath))
				{
					Directory.CreateDirectory(folderpath);
				}

				FileStream wfileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
				BinaryWriter writer = new BinaryWriter(wfileStream);
				writer.Write(crypted);
				writer.Close();
			}
		}
	}

}
