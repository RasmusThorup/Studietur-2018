using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TimetravelFMODPreviewController : MonoBehaviour {

    StudioEventEmitter eventEmitter;

    public bool printToConsole;

	// Use this for initialization
	void Start () {
        eventEmitter = GetComponent<StudioEventEmitter>();
	}

    public void PlayFMODYearPreview()
    {
        if (!eventEmitter.IsPlaying())
        {
            eventEmitter.Event = GameController.timeTravelPlaceSettings.timetravelData[TimetravelController.currentYearScriptableObjectsIndex].timetravelPreviewFMODEvent;
            eventEmitter.Play();
        } else
        {
            if (printToConsole)
            {
                print("Fmod event already playing");
            }
        }
    }

    public void StopFMODYearPreview(){
        if (eventEmitter.IsPlaying())
        {
            eventEmitter.Stop();
            eventEmitter.Event = null;
        } else
        {
            if (printToConsole)
            {
                print("Fmod event already arent playing");
            }
        }

    }

    private void OnDisable()
    {
        StopFMODYearPreview();
    }
}
