using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayButtonController : MonoBehaviour {

    bool narratorSpeaking;

    public UnityEvent StartPlay;
    public UnityEvent StopPlay;

    private void OnEnable()
    {
        narratorSpeaking = false;
    }

    public void PlayButtonPressed(){

        if (!narratorSpeaking)
        {
            StartPlay.Invoke();
            PlayFMODYearEvent();
            narratorSpeaking = true;
        } else
        {
            StopPlay.Invoke();
            narratorSpeaking = false;
        }
    }

    public void PlayFMODYearEvent(){

        print("Start to play for year " + GameController.timeTravelPlaceSettings.timetravelData[TimetravelController.currentYearScriptableObjectsIndex].timetravelFMODEventRef);

    }
}
