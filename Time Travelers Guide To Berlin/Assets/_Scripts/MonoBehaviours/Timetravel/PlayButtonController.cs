using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMOD.Studio;
using FMODUnity;

public class PlayButtonController : MonoBehaviour {

    bool narratorSpeaking;

    public UnityEvent StartPlay;
    public UnityEvent StopPlay;

    EventInstance instance;

    private void OnEnable()
    {
        narratorSpeaking = false;
    }

    public void PlayButtonPressed(){

        if (!narratorSpeaking)
        {
            //Start Play
            instance = RuntimeManager.CreateInstance(GameController.timeTravelPlaceSettings.timetravelData[TimetravelController.currentYearScriptableObjectsIndex].timetravelNarratorFMODEvent);
            instance.start();

            StartPlay.Invoke();

            narratorSpeaking = true;
        } else
        {
            //Stop Play
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();

            StopPlay.Invoke();

            narratorSpeaking = false;
        }
    }
}
