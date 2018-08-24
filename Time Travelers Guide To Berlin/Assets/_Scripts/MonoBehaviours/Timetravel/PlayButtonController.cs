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

    public AudioSource microphoneSource;

    EventInstance instance;

    public int sampleRateMin = 0;
    public int sampleRateMax = 0;

    public string deviceName;

    private void Start()
    {
        deviceName = Microphone.devices[0];
        Microphone.GetDeviceCaps(deviceName, out sampleRateMin, out sampleRateMax);
    }

    private void OnEnable()
    {
        narratorSpeaking = false;
    }

    public void PlayButtonPressed(){

        if (!narratorSpeaking)
        {
            //Start Play
            if (TimetravelController.currentYearScriptableObjectsIndex == GameController.timeTravelPlaceSettings.timetravelData.Length-1)
            {
                //Debug.Log("Microphone on");
                //microphoneSource.clip = Microphone.Start(Microphone.devices[0], true, 2, 44100);
                //microphoneSource.Play();


                microphoneSource.clip = Microphone.Start(deviceName, true, 2, sampleRateMax);

                while (!(Microphone.GetPosition(deviceName) > 1))
                {
                    // Wait until the recording has started
                }

                microphoneSource.loop = true;
                microphoneSource.Play();

            }
            else{

                instance = RuntimeManager.CreateInstance(GameController.timeTravelPlaceSettings.timetravelData[TimetravelController.currentYearScriptableObjectsIndex].timetravelNarratorFMODEvent);
                instance.start();
                
                StartPlay.Invoke();
                
            }

            narratorSpeaking = true;

        } else
        {
            if (TimetravelController.currentYearScriptableObjectsIndex == GameController.timeTravelPlaceSettings.timetravelData.Length - 1)
            {
                microphoneSource.Stop();
                Microphone.End(deviceName);

            }else{

                //Stop Play
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                
                StopPlay.Invoke();
                
            }

            narratorSpeaking = false;
        }
    }

    public void StopNarratorFMODEvent()
    {
        if (narratorSpeaking)
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();

            narratorSpeaking = false;
        }
    }
}
