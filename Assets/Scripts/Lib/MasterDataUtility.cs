using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

public class MasterDataUtility
{
	// CSVの中身を入れるリスト
	private static List<string[]> datas = new List<string[]>(32);

	// param result
	private static List<int> paramResult = new List<int>();

	/*------------------------------------------------------------------*/
	// CSV読み込み
	/*------------------------------------------------------------------*/
	public static List<string[]> LoadMasterData (string dataName)
	{
		datas.Clear();

		// Resouces/CSV下のCSV読み込み
		var csvFile = Resources.Load("Data/CSV/" + dataName) as TextAsset;

		if (null == csvFile)
		{
			Debug.Log("Not Found " + dataName);
			return datas;
		}

		StringReader reader = new StringReader(csvFile.text);

		// , で分割しつつ一行ずつ読み込みリストに追加していく
		while (reader.Peek() != -1)		// reader.Peaekが-1になるまで
		{
			string line = reader.ReadLine();	// 一行ずつ読み込み
			datas.Add(line.Split(','));			// , 区切りでリストに追加
		}

		return datas;
	}


	/*------------------------------------------------------------------*/
	// key チェック
	/*------------------------------------------------------------------*/
	public static bool Check (string key, string param)
	{
		var value = param.Split(':');
		if (null != value)
		{
			if (key == value[0])
			{
				return true;
			}
		}

		return false;
	}

	/*------------------------------------------------------------------*/
	// string 取得
	/*------------------------------------------------------------------*/
	public static string GetString (string key, string param)
	{
		var value = param.Split(':');
		if (null != value)
		{
			if (key == value[0])
			{
				return value[1];
			}
		}

		return "";
	}

	/*------------------------------------------------------------------*/
	// int 取得
	/*------------------------------------------------------------------*/
	public static int GetInt (string key, string param)
	{
		var value = param.Split(':');
		if (null != value)
		{
			if (key == value[0])
			{
				return int.Parse(value[1]);
			}
		}

		return 0;
	}

	public static IList<int> GetInts (string key, string param)
	{
		var value = param.Split(':');
		if (null != value)
		{
			if (key == value[0])
			{
				paramResult.Clear();
				var list = value[1].Split('|');
				foreach(var i in list)
				{
					paramResult.Add(int.Parse(i));
				}

				return paramResult.ToArray();
			}
		}

		return null;
	}

	/*------------------------------------------------------------------*/
	// float 取得
	/*------------------------------------------------------------------*/
	public static float GetFloat (string key, string param)
	{
		var value = param.Split(':');
		if (null != value)
		{
			if (key == value[0])
			{
				return float.Parse(value[1]);
			}
		}

		return 0;
	}

	/*------------------------------------------------------------------*/
	// vector2 取得
	/*------------------------------------------------------------------*/
	public static Vector2 GetVector2 (string key, string param)
	{
		Vector2 result = Vector2.zero;

		var value = param.Split(':');
		if (null != value)
		{
			if (key == value[0])
			{
				var p = value[1].Split('|');
				if (null != p)
				{
					result = new Vector2(float.Parse(p[0]), float.Parse(p[1]));	
				}
			}
		}

		return result;
	}

	public static Vector2 GetVector2(string param, char splitChar)
	{
		Vector2 result = Vector2.zero;

		var split = param.Split(splitChar);
		result = new Vector2(float.Parse(split[0]), float.Parse(split[1]));

		return result;
	}

	public static Vector2Int GetVector2Int(string param, char splitChar)
	{
		Vector2Int result = Vector2Int.zero;

		var split = param.Split(splitChar);
		result = new Vector2Int(int.Parse(split[0]), int.Parse(split[1]));

		return result;
	}

	/*------------------------------------------------------------------*/
	// vector3 取得
	/*------------------------------------------------------------------*/
	public static Vector3 GetVector3 (string key, string param)
	{
		Vector3 result = Vector3.zero;

		var value = param.Split(':');
		if (null != value)
		{
			if (key == value[0])
			{
				var p = value[1].Split('|');
				if (null != p)
				{
					result = new Vector3(float.Parse(p[0]), float.Parse(p[1]), float.Parse(p[2]));	
				}
			}
		}

		return result;
	}

	public static Vector2 GetVector3(string param, char splitChar)
	{
		Vector3 result = Vector3.zero;

		var split = param.Split(splitChar);
		result = new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));

		return result;
	}

	/*------------------------------------------------------------------*/
	// color 取得
	/*------------------------------------------------------------------*/
	public static Color GetColor (string key, string param)
	{
		Color result = Color.white;

		var value = param.Split(':');
		if (null != value)
		{
			if (key == value[0])
			{
				var p = value[1].Split('|');
				if (null != p)
				{
					result = new Color(float.Parse(p[0])/255.0f, float.Parse(p[1])/255.0f, float.Parse(p[2])/255.0f, float.Parse(p[3])/255.0f);	
				}
			}
		}

		return result;
	}

	/*------------------------------------------------------------------*/
	// XML読み込み
	/*------------------------------------------------------------------*/
	public static XDocument LoadXml (string dataName)
	{
		// Resouces/XML下のXml読み込み
		var asset = Resources.Load(dataName) as TextAsset;
		if (null == asset)
		{
			return null;
		}

		try
		{
			return XDocument.Parse(asset.text);
		}
		catch (System.Exception e)
		{
			Debug.Log("xml load failled..." + e.Message);
		}

		return null;
	}
}

