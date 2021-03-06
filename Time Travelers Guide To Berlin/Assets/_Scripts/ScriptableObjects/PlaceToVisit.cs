﻿using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using FMODUnity;

[CreateAssetMenu]
public class PlaceToVisit : ScriptableObject {

    [BoxGroup("Place Name")]
    public string placeName;

    [BoxGroup("Place Icon")]
    public Sprite icon;

    [BoxGroup("Real World Coordinates")]
    public Vector2 placeCoordinates;


    [BoxGroup("Timetravel Data")]
    public TimetravelData[] timetravelData;


    [System.Serializable]
    public struct TimetravelData {
        
        public string timetravelDate;

        [EventRef]
        public string timetravelNarratorFMODEvent;

        [EventRef]
        public string timetravelPreviewFMODEvent;
    }

}
