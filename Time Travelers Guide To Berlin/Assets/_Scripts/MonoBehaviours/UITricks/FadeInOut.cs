using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeInOut : MonoBehaviour {

    public Transform fadeOutPos;
    public GameObject timemachineUI;
    public UnityEvent action;

    Animator fadeAnimator;


	void Start () {
        fadeAnimator = GetComponent<Animator>();
	}
	

    public void StartFade(){

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            transform.position = Input.mousePosition;
        } else
        {
            Touch touch = Input.GetTouch(0);

            transform.position = touch.position;
        }

        fadeAnimator.SetTrigger("StartFadeUI");
    }

    public void PlaceFadeAtNewPos(){
        transform.position = fadeOutPos.position;
    }

    public void EnableTimemachineUI(){
        timemachineUI.SetActive(true);
    }

    public void InvokeAction(){
        action.Invoke();  
    }
}
