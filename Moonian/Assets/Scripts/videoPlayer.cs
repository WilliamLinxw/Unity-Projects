using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoPlayer : MonoBehaviour
{
	/// <summary>
	/// 视频播放器
	/// </summary>
	public VideoPlayer VPlayer;
	/// <summary>
	/// 承载视频的媒介
	/// </summary>
	public RawImage rawImg;
	/// <summary>
	/// 下一个要播放的视频
	/// </summary>
	public videoPlayer nextVPlayer;
	/// <summary>
	/// 是否要循环播放，加载背景需要循环，而且循环播放不关闭
	/// </summary>
	public bool isLoop;
	/// <summary>
	/// 是否是开始CG动画
	/// </summary>
	public bool isStartCG;

	private void Awake()
	{
		rawImg.gameObject.SetActive(false);
		VPlayer.errorReceived += ErrorReceived;
		VPlayer.loopPointReached += LoopPointReached;
		VPlayer.isLooping = isLoop;
		//动画循环，是游戏开始界面，设置下次打开游戏不播放CG
		if (isLoop)
			PlayerPrefs.SetInt("Game", 1);
	}

	private void Start()
	{
		//如果达到某些条件就不播放，比如CG动画只在第一次打开游戏时播放
		if (PlayerPrefs.GetInt("Game", 0) == 1 && isStartCG)
		{
			Stop();
		}
		else
		{
			VPlayer.SetDirectAudioVolume(0, 1);
			VPlayer.Play();
			rawImg.gameObject.SetActive(true);
		}
	}

	private void Update()
	{
		//点击空格键或者ESC键停止播放
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
		{
			if (!isLoop)
				Stop();
		}
	}

	private void Stop()
	{
		if (nextVPlayer)
		{
			nextVPlayer.gameObject.SetActive(true);
			//先显示下一个视频，1秒钟后再消失本视频（时间可以适当调整）
			Invoke("StopPlay", 1);
		}
		else
		{
			VPlayer.Stop();
			Destroy(gameObject);
		}

	}
	private void StopPlay()
	{
		CancelInvoke("StopPlay");
		VPlayer.Stop();
		Destroy(gameObject);
	}
	/// <summary>
	/// 循环播放
	/// </summary>
	private void LoopPointReached(VideoPlayer vp)
	{
		if (!isLoop)
			Stop();
	}
	/// <summary>
	/// 播放错误
	/// </summary>
	private void ErrorReceived(VideoPlayer vp, string err)
	{
		if (!isLoop)
			Stop();
	}

}
