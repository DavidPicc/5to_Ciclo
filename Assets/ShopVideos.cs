using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShopVideos : MonoBehaviour
{
    VideoPlayer _videoPlayer;
    public RenderTexture VideoRenderTexture;
    public RawImage VideoImage;
    public GameObject VideoObject;

    public VideoClip LaserVideo, BombVideo, FireVideo, SawVideo, ShieldVideo;

    bool _isChanging;
    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    public void PlayVideo(VideoClip clip)
    {
        if(!_isChanging)
        {
            VideoObject.SetActive(true);
            VideoImage.texture = VideoRenderTexture;
            _videoPlayer.targetTexture = VideoRenderTexture;
            _videoPlayer.clip = clip;

            Debug.Log(clip);
            _isChanging = true;
        }
    }

    public void StopVideo()
    {
        VideoObject.SetActive(false);
        _videoPlayer.clip = null;
        _isChanging = false;
    }
}
