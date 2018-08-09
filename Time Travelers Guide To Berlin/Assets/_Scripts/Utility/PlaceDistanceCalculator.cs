using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaceDistanceCalculator{


    public static double HaversineDistance(Vector2 placeCoordinates)
    {
        double R = 6371;
        var lat = (GPS.playerCoordinates.x - placeCoordinates.x) * Mathf.Deg2Rad;
        var lng = (GPS.playerCoordinates.y - placeCoordinates.y) * Mathf.Deg2Rad;
        var h1 = Mathf.Sin(lat / 2) * Mathf.Sin(lat / 2) +
                      Mathf.Cos(placeCoordinates.x*Mathf.Deg2Rad) * Mathf.Cos(GPS.playerCoordinates.x*Mathf.Deg2Rad) *
                      Mathf.Sin(lng / 2) * Mathf.Sin(lng / 2);
        var h2 = 2 * Mathf.Asin(Mathf.Min(1, Mathf.Sqrt(h1)));
        return R * h2;
    }

    //public static double HaversineDistance(Vector2 place1, Vector2 place2)
    //{
    //    double R = 6371;
    //    var lat = (place2.x - place1.x) * Mathf.Deg2Rad;
    //    var lng = (place2.y - place1.y) * Mathf.Deg2Rad;
    //    var h1 = Mathf.Sin(lat / 2) * Mathf.Sin(lat / 2) +
    //                  Mathf.Cos(place1.x * Mathf.Deg2Rad) * Mathf.Cos(place2.x * Mathf.Deg2Rad) *
    //                  Mathf.Sin(lng / 2) * Mathf.Sin(lng / 2);
    //    var h2 = 2 * Mathf.Asin(Mathf.Min(1, Mathf.Sqrt(h1)));
    //    return R * h2;
    //}

    public static float DirectionToPlace(Vector2 place1, Vector2 place2){

        return 1;
    }
}
