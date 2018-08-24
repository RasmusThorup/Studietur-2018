using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UIPlayButtonController : MonoBehaviour
{
    public float playbackSpeed;

    public long frameJumpTo;
    public long frameJumpFrom;

    public VideoPlayer videoPlayer;

    public VideoClip normal;
    public VideoClip disabled;
    public VideoClip loop;
    public VideoClip stop;

    public enum PlayState { Normal, Disabled, Starting,Looping}
    public PlayState playState;

    private void OnEnable()
    {
        ChangeVideo(normal, playbackSpeed);
        playState = PlayState.Normal;
    }


    public void PlayButtonAnimNormal()
    {
        Debug.Log("Playbutton Normal");

        if (playState != PlayState.Normal)
        {
            ChangeVideo(normal, playbackSpeed);
            playState = PlayState.Normal;
        }

    }

    public void PlayButtonAnimDisable()
    {
        Debug.Log("Playbutton Disable");

        if (playState != PlayState.Disabled)
        {
            ChangeVideo(disabled,playbackSpeed);
            playState = PlayState.Disabled;
        }
    }

    public void PlayButtonAnimPressed()
    {
        Debug.Log("Playbutton Pressed");

        if (playState == PlayState.Normal)
        {
            StartCoroutine(StartPlayAnim());
        } else if (videoPlayer.isPlaying)
        {
            StopAllCoroutines();
            ChangeVideo(stop,1);
            videoPlayer.isLooping = false;
            playState = PlayState.Normal;
        }

    }

    IEnumerator StartPlayAnim(){

        videoPlayer.clip = loop;
        videoPlayer.playbackSpeed = 1;
        videoPlayer.Play();
        playState = PlayState.Looping;

        videoPlayer.isLooping = true;

        while (true)
        {
            yield return new WaitUntil(() => videoPlayer.frame == 140);
            
            videoPlayer.frame = 51;
        }
    }

    void ChangeVideo(VideoClip clip, float speed){
        videoPlayer.clip = clip;
        videoPlayer.playbackSpeed = speed;
        videoPlayer.Play();
    }

}
