
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
	/// <summary> seチャンネル数 </summary>
	private const int seChannelNum = 16;

	/// <summary> AudioSource </summary>
	private AudioSource bgm = null;
	private Dictionary<int, AudioSource> se = new Dictionary<int, AudioSource>(seChannelNum);

	/// <summary>
	/// CAPACITY
	/// </summary>
	private const int SOUND_CLIP_CAPACITY = 100;

	/// <summary> SEクリップ </summary>
	private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>(SOUND_CLIP_CAPACITY);

	/// <summary> 現在再生中のBGM名 </summary>
	private string currentBGMName = "";

	public string CurrentBGMName => currentBGMName;


	/// <summary>
	/// マスターボリューム
	/// </summary>
	private float bgmMasterVolume = 1.0f;

	private float seMasterVolume = 1.0f;

	public void SetBGMMasterVolume(float volume)
	{
		bgmMasterVolume = volume;
		if (null != bgm)
		{
			bgm.volume *= bgmMasterVolume;
		}
	}

	public void SetSEMasterVolume(float volume)
	{
		seMasterVolume = volume;
	}


	/// <summary>
	/// Initialize
	/// </summary>
	public void Initialize ()
	{
		// サウンドroot作成
		var root = new GameObject("Sound");

		// AudioSource作成
		if (null != root)
		{
			bgm = root.gameObject.AddComponent<AudioSource>();
			bgm.playOnAwake = false;
			bgm.loop = true;
			for (int i=0; i<seChannelNum; i++)
			{
				var source = root.gameObject.AddComponent<AudioSource>();
				source.playOnAwake = false;
				source.loop = false;
				se.Add(i, source);
			}
		}

		// オーディオリスト初期化
		clips.Clear();

		// AudioClipの取得
		foreach(var data in SoundMasterData.SoundInfos)
		{
			var clip = Resources.Load(data.Value.AssetName) as AudioClip;
			if (null != clip)
			{
				clips.Add(data.Key, clip);
			}
		}
	}

	/// <summary>
	/// 空きのSE AudioSource取得
	/// </summary>
	private int getEmptySource ()
	{
		foreach (var source in se)
		{
			if (!source.Value.isPlaying)
			{
				return source.Key;
			}
		}

		return -1;
	}

	/// <summary>
	/// 全SE停止
	/// </summary>
	public void StopAllSource ()
	{
		foreach(var source in se)
		{
			if (source.Value.isPlaying)
			{
				source.Value.Stop();
			}
		}
	}

	/// <summary>
	/// SE停止
	/// </summary>
	public void StopSource(int index)
	{
		if (index < 0 || index > se.Count - 1)
		{
			return;
		}

		if (se[index].isPlaying)
		{
			se[index].Stop();
		}
	}

	/// <summary>
	/// SE再生
	/// </summary>
	public int Play (string key)
	{
#if DEBUG_NO_SOUND
		return -1;
#endif
		// 対応するキーがない
		if (!clips.ContainsKey(key))
		{
			return -1;
		}

		// 空きソース取得
		var sourceIndex = getEmptySource();
		if (-1 != sourceIndex)
		{
			var source = se[sourceIndex];
			// 再生
			source.clip = clips[key];
			var info = SoundMasterData.Get(key);
			if (null != info)
			{
				source.volume = info.Volume;
			} else {
				source.volume = 1;
			}
			source.volume *= seMasterVolume;
			source.loop = info.IsLoop;
			source.Play();
			return sourceIndex;
		}

		return -1;
	}

	/// <summary>
	/// BGM再生
	/// </summary>
	public void PlayBGM(string key, float time = 0)
	{
#if DEBUG_NO_SOUND
		return;
#endif
		// 対応するキーがない
		if (!clips.ContainsKey(key))
		{
			return;
		}

		// 再生
		bgm.clip = clips[key];
		bgm.time = time;
		var info = SoundMasterData.Get(key);
		if (null != info)
		{
			bgm.volume = info.Volume;
		}
		else
		{
			bgm.volume = 1;
		}
		bgm.volume *= bgmMasterVolume;
		bgm.Play();

		currentBGMName = key;
	}

	/// <summary>
	/// BGMボリュームセット
	/// </summary>
	public float GetBGMVolume ()
	{
		if (bgm.isPlaying)
		{
			return bgm.volume;
		}

		return 1;
	}

	/// <summary>
	/// BGMボリュームセット
	/// </summary>
	public void SetBGMVolume(float volume)
	{
		if (bgm.isPlaying)
		{
			bgm.volume = volume;
		}
	}

	/// <summary>
	/// BGM停止
	/// </summary>
	public void StopBGM()
	{
		bgm.Stop();
	}

	/// <summary>
	/// Release
	/// </summary>
	public void Release()
	{
	}
}

