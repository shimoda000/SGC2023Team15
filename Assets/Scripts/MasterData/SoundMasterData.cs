
using System.Collections.Generic;
using System.Xml.Linq;

public class SoundMasterData
{
	[System.Serializable]
	public class SoundInfo
	{
		// Id
		public string Id;

		// AssetName
		public string AssetName;

		// Loop
		public bool IsLoop;

		// volume
		public float Volume;

		public SoundInfo()
		{
			Id = "";
		}

		/// <summary>
		/// BGMか？
		/// </summary>
		public bool IsBGM
		{
			get
			{
				return Id.Contains("BGM");
			}
		}
	}

	private static Dictionary<string, SoundInfo> soundInfos = new Dictionary<string, SoundInfo>(32);

	public static Dictionary<string, SoundInfo> SoundInfos
	{
		get {
			return soundInfos;
		}
	}

	/// <summary>
	/// 読み込み
	/// </summary>
	public static void Setup ()
	{
		// リストを空に
		soundInfos.Clear();

		// 読み込み
		var xml = MasterDataUtility.LoadXml("MasterData/Sound");
		if (null == xml)
		{
			return;
		}

		// キャラクターで取得
		XElement sounds = xml.Element("sounds");
		IEnumerable<XElement> sound = sounds.Elements("sound");

		foreach (var data in sound)
		{
			SoundInfo soundInfo = new SoundInfo();

			// Id
			soundInfo.Id = data.Element("id").Value;
			// AssetName
			soundInfo.AssetName = null == data.Element("assetName") ? "" : data.Element("assetName").Value;
			// Loop
			soundInfo.IsLoop = null != data.Element("loop");
			// Volume
			soundInfo.Volume = null == data.Element("volume") ? 1 : float.Parse(data.Element("volume").Value);

			soundInfos.Add(soundInfo.Id, soundInfo);

//				GameUtility.DebugLog(data.Key + ", Hard:" + data.Hard.ToString() + ", Normal:" + data.Normal.ToString());
		}
	}

	/// <summary>
	/// 取得
	/// </summary>
	public static SoundInfo Get (string key)
	{
		SoundInfo data = null;

		if (soundInfos.TryGetValue(key, out data))
		{
			return data;
		}

		return null;
	}
}
