using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDirection : MonoBehaviour {

    public float trueHeading;
    public float bearing;

    public Vector2 placeCoordinates;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        Input.compass.enabled = true;

        trueHeading = Input.compass.trueHeading;
        bearing = Bearing();

        float dir = trueHeading - bearing;

        transform.localRotation = new Quaternion(0,0,dir,0);

	}

    public float Bearing(){

        var y = Mathf.Sin(placeCoordinates.y - GPS.playerCoordinates.y) * Mathf.Cos(placeCoordinates.x);
        var x = Mathf.Cos(GPS.playerCoordinates.x) * Mathf.Sin(placeCoordinates.x)
                     - Mathf.Sin(GPS.playerCoordinates.x) * Mathf.Cos(placeCoordinates.x) 
                     * Mathf.Cos(placeCoordinates.y - GPS.playerCoordinates.y);

        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }
}
