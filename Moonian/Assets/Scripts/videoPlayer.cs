using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoPlayer : MonoBehaviour
{
	/// <summary>
	/// ��Ƶ������
	/// </summary>
	public VideoPlayer VPlayer;
	/// <summary>
	/// ������Ƶ��ý��
	/// </summary>
	public RawImage rawImg;
	/// <summary>
	/// ��һ��Ҫ���ŵ���Ƶ
	/// </summary>
	public videoPlayer nextVPlayer;
	/// <summary>
	/// �Ƿ�Ҫѭ�����ţ����ر�����Ҫѭ��������ѭ�����Ų��ر�
	/// </summary>
	public bool isLoop;
	/// <summary>
	/// �Ƿ��ǿ�ʼCG����
	/// </summary>
	public bool isStartCG;

	private void Awake()
	{
		rawImg.gameObject.SetActive(false);
		VPlayer.errorReceived += ErrorReceived;
		VPlayer.loopPointReached += LoopPointReached;
		VPlayer.isLooping = isLoop;
		//����ѭ��������Ϸ��ʼ���棬�����´δ���Ϸ������CG
		if (isLoop)
			PlayerPrefs.SetInt("Game", 1);
	}

	private void Start()
	{
		//����ﵽĳЩ�����Ͳ����ţ�����CG����ֻ�ڵ�һ�δ���Ϸʱ����
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
		//����ո������ESC��ֹͣ����
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
			//����ʾ��һ����Ƶ��1���Ӻ�����ʧ����Ƶ��ʱ������ʵ�������
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
	/// ѭ������
	/// </summary>
	private void LoopPointReached(VideoPlayer vp)
	{
		if (!isLoop)
			Stop();
	}
	/// <summary>
	/// ���Ŵ���
	/// </summary>
	private void ErrorReceived(VideoPlayer vp, string err)
	{
		if (!isLoop)
			Stop();
	}

}
