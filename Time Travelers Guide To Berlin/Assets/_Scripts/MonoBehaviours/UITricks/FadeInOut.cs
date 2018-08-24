using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class FadeInOut : MonoBehaviour
{

    public Transform fadeOutPos;
    public GameObject timemachineUI;
    public UnityEvent action;

    Animator fadeAnimator;

   public GameObject child;
    public VideoPlayer videoPlayer;

    public UnityEvent OnFadeOut;
    

    void Start()
    {
        fadeAnimator = GetComponent<Animator>();

    }


    public void StartFade()
    {
        OnFadeOut.Invoke();

        transform.DetachChildren();

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            Touch touch = Input.GetTouch(0);

            transform.position = touch.position;
        }

        child.transform.SetParent(transform);

        fadeAnimator.SetTrigger("StartFadeUI");

        videoPlayer.Play();

    }

    public void PlaceFadeAtNewPos()
    {
        transform.DetachChildren();
        transform.position = fadeOutPos.position;
        child.transform.SetParent(transform);
    }

    public void EnableTimemachineUI()
    {
        timemachineUI.SetActive(true);
    }

    public void InvokeAction()
    {

        action.Invoke();
    }

    public void DisableChild()
    {
        //child.SetActive(false);
    }
}
