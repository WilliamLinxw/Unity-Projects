using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public GameObject blackpaper;
    public bool backToStartScene;
    private float flag = 0;
    void Start()
    {
        StartCoroutine(PlayVideo());
        blackpaper.SetActive(true);
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            blackpaper.SetActive(false);
        }
        flag += 1;
        if (flag >= 100 && !videoPlayer.isPlaying)
        {
            GlobalControl.Instance.videoPlayed = true;
            Cursor.visible = true;
            rawImage.gameObject.SetActive(false);
            videoPlayer.gameObject.SetActive(false);
            audioSource.gameObject.SetActive(false);
            blackpaper.SetActive(false);
            this.enabled = false;
            if (backToStartScene)
            {
                GlobalControl.Instance.BackToStartScene();
            }
        }
    }

}
