using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UIPlayButtonController : MonoBehaviour
{
    public float playbackSpeed;

    public VideoPlayer videoPlayer;

    public VideoClip normal;
    public VideoClip disabled;
    public VideoClip loop;
    public VideoClip stop;

    public enum PlayState { Normal, Disabled, StopLoop, Looping }
    public PlayState playState;

    public List<PlayState> playStates = new List<PlayState>();

    Coroutine chageStates;

    private void OnEnable()
    {
        ChangeVideo(normal, playbackSpeed);
        playState = PlayState.Normal;
       
    }


    public void PlayButtonAnimNormal()
    {
        if (playState != PlayState.Normal && playState != PlayState.Looping)
        {
            //ChangeVideo(normal, playbackSpeed);

            playState = PlayState.Normal;

            playStates.Add(PlayState.Normal);

            if (playStates.Count == 1)
                StartCoroutine(ChangePlaystate());
        }

    }

    public void PlayButtonAnimDisable()
    {
        if (playState != PlayState.Disabled)
        {
            //ChangeVideo(disabled, playbackSpeed);
            playState = PlayState.Disabled;

            playStates.Add(PlayState.Disabled);
               
            if (playStates.Count == 1)
                StartCoroutine(ChangePlaystate());
        }
    }

    public void PlayButtonAnimPressed()
    {
        if (playState != PlayState.Looping)
        {
            //StopAllCoroutines();
            StartCoroutine(StartPlayAnim());

            //playStates.Add(PlayState.Looping);
            //playState = PlayState.Looping;

            //if (playStates.Count == 1)
                //StartCoroutine(ChangePlaystate());

        } else if (videoPlayer.isPlaying)
        {
            //playStates.Add(PlayState.StopLoop);

            StopAllCoroutines();
            ChangeVideo(stop,1.4f);
            videoPlayer.isLooping = false;
            videoPlayer.waitForFirstFrame = true;
            playState = PlayState.Normal;

            //playStates.Clear();

        }



    }

    IEnumerator StartPlayAnim(){

        videoPlayer.clip = loop;
        videoPlayer.playbackSpeed = 1.4f;
        videoPlayer.Play();
        playState = PlayState.Looping;

        videoPlayer.isLooping = true;
        videoPlayer.waitForFirstFrame = false;

        while (true)
        {
            yield return new WaitUntil(() => videoPlayer.frame == 140 || playState != PlayState.Looping);
            
            videoPlayer.frame = 51;
        }
    }

    void ChangeVideo(VideoClip clip, float speed){
        videoPlayer.clip = clip;
        videoPlayer.playbackSpeed = speed;
        videoPlayer.Play();
    }


    IEnumerator ChangePlaystate()
    {
        if (playStates.Count>0)
        {
            switch (playStates[0])
            {
                case PlayState.Normal:
                    ChangeVideo(normal, playbackSpeed);
                    yield return new WaitForSeconds(.5f);
                    //yield return new WaitWhile(() => videoPlayer.isPlaying);

                    break;
                    
                case PlayState.Disabled:
                    ChangeVideo(disabled, playbackSpeed);
                    yield return new WaitForSeconds(.5f);
                    //yield return new WaitWhile(() => videoPlayer.isPlaying);

                    break;
                    
                case PlayState.Looping:
                    videoPlayer.clip = loop;
                    videoPlayer.playbackSpeed = 1;
                    videoPlayer.Play();

                    videoPlayer.isLooping = true;
                    videoPlayer.waitForFirstFrame = false;

                    while (playState == PlayState.Looping)
                    {
                        yield return new WaitUntil(() => videoPlayer.frame == 140 || playState == PlayState.StopLoop);

                        videoPlayer.frame = 51;
                    }

                    break;

                case PlayState.StopLoop :
                    ChangeVideo(stop, 1);

                    videoPlayer.isLooping = false;
                    videoPlayer.waitForFirstFrame = true;

                    playState = PlayState.Normal;

                    yield return new WaitForSeconds(1f);

                    break;

                default:
                    break;
            }
        }

        playStates.RemoveAt(0);

        if (playStates.Count!=0)
        {
            StartCoroutine(ChangePlaystate());
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
        playStates.Clear();
        playState = PlayState.Normal;
        videoPlayer.isLooping = false;
        videoPlayer.waitForFirstFrame = true;

    }

}
