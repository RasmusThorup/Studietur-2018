using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SwipeUpFromBackButton : MonoBehaviour {

    [Required]
    public Animator TimetravelUIAnim;

    [ReadOnly]
    public bool backButtonHeld;

    private void OnEnable()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    void SwipeDetector_OnSwipe(SwipeData data)
    {
        if (backButtonHeld && data.Direction == SwipeDirection.Up)
        {
            TimetravelUIAnim.SetTrigger("BackButton");
        }
    }

    private void OnDisable()
    {
        SwipeDetector.OnSwipe -= SwipeDetector_OnSwipe;
    }

    public void BackButtonIsHeld(bool isHeld){
        backButtonHeld = isHeld;
    }

    //public void PlayBackButtonAnim(){
    //    TimetravelUIAnim.SetTrigger("BackButton");
    //}
}
