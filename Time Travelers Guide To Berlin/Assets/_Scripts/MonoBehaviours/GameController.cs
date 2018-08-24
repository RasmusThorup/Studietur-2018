using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    

    public float setMinDistanceInKm;

    public static float minDistance;

    public static bool isTimeTraveling;

    //public PlaceToVisit debugPlaceToVisit;

    public static PlaceToVisit timeTravelPlaceSettings;

    public static bool debugMode;

    //private void Start()
    //{
    //    if (debugPlaceToVisit)
    //    {
    //        timeTravelPlaceSettings = debugPlaceToVisit;
    //    }
    //}

    private void Update()
    {
        minDistance = setMinDistanceInKm;

        if (!isTimeTraveling && !debugMode)
        {
            GPS.Instance.StartGPS();
        }else
        {
            
        }
    }

    public void DebugModeToggle(bool debugOn){

        debugMode = debugOn;

        if (debugOn)
        {
            
        } else
        {
            
        }
    }
}
