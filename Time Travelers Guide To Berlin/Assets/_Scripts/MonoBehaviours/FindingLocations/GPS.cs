using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GPS : MonoBehaviour {

    public static GPS Instance { set; get; }

    public static Vector2 playerCoordinates;

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

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <=0)
        {
            Debug.Log("Timed Out");

            gPSServiceIsRunning = false;
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determin device location");

            gPSServiceIsRunning = false;
            yield break;
        }

        //latitude = Input.location.lastData.latitude;
        playerCoordinates.x = Input.location.lastData.latitude;

        //longitude = Input.location.lastData.longitude;
        playerCoordinates.y = Input.location.lastData.longitude;


        gPSServiceIsRunning = false;
        yield break;
    }
}
