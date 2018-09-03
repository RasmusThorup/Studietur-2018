using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GPS : MonoBehaviour {

    public static GPS Instance { set; get; }

    public static Vector2 playerCoordinates;

    public GameObject connectionLostUI;
    public int maxWaitForInitializing;

    [BoxGroup("GPS Update Interval")]
    [InfoBox("Default value is 10")]
    public float desiredAccuracyInMeters;

    [BoxGroup("GPS Update Interval")]
    [InfoBox("Default value is 10")]
    public float updateDistanceInMeters;

    [BoxGroup("Current Coordinates")]
    public float latitude;

    [BoxGroup("Current Coordinates")]
    public float longitude;


    bool startGPSUpdate;

    [SerializeField]
    bool gPSServiceIsRunning;



	void Start () {
        Instance = this;
        //StartCoroutine(StartLocationService());
	}


    private void Update()
    {
        latitude = playerCoordinates.x;
        longitude = playerCoordinates.y;

        //if (!gPSServiceIsRunning && /*startGPSUpdate && */ !GameController.debugMode)
        //{
        //    StartCoroutine(StartLocationService());
        //}
    }

    public void StartGPS(){
        
        if (!gPSServiceIsRunning)
        {
            StartCoroutine(StartLocationService());
        }
    }

    IEnumerator StartLocationService(){

        gPSServiceIsRunning = true;

        if(!Input.location.isEnabledByUser){
            Debug.Log("User has not enabled GPS");

            gPSServiceIsRunning = false;
            yield break;

        }

        Input.location.Start(desiredAccuracyInMeters,updateDistanceInMeters);
        int maxWait = maxWaitForInitializing;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("Location Service Initializing Timer: " + maxWait);
        }

        if (maxWait <=0)
        {
            Debug.Log("Timed Out");
            connectionLostUI.SetActive(true);
            gPSServiceIsRunning = false;
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determin device location");

            connectionLostUI.SetActive(true);

            gPSServiceIsRunning = false;
            yield break;
        }

        if (connectionLostUI.activeSelf)
            connectionLostUI.SetActive(false);
        

        //latitude = Input.location.lastData.latitude;
        playerCoordinates.x = Input.location.lastData.latitude;

        //longitude = Input.location.lastData.longitude;
        playerCoordinates.y = Input.location.lastData.longitude;


        gPSServiceIsRunning = false;
        yield break;
    }
}
