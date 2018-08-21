using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TimetravelFMODPreviewController : MonoBehaviour {
    
    public bool printToConsole;

    bool previewIsPlaying;
    EventInstance instance;

    public void PlayFMODYearPreview()
    {
        if (!previewIsPlaying)
        {
            //Start Preview
            instance = RuntimeManager.CreateInstance(GameController.timeTravelPlaceSettings.timetravelData[TimetravelController.currentYearScriptableObjectsIndex].timetravelPreviewFMODEvent);
            instance.start();

            previewIsPlaying = true;

        } else
        {
            if (printToConsole)
            {
                print("Fmod event already playing");
            }
        }
    }

    public void StopFMODYearPreview(){

        if (previewIsPlaying)
        {
            //Stop preview;
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();

            previewIsPlaying = false;

        }else
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
