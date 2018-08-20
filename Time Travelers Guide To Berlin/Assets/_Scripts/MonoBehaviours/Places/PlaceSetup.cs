using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PlaceSetup : MonoBehaviour {

    public PlaceToVisit placeToVisit;

    string unit;

    Button timeMachineButton;
    FadeInOut fadeInOut;

    private void Start()
    {
        timeMachineButton = GetComponentInChildren<Button>();
        fadeInOut = FindObjectOfType<FadeInOut>();
    }

    public void UpdatePlaceInfo()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = placeToVisit.name;
        
        transform.GetChild(2).GetComponent<Image>().sprite = placeToVisit.icon;
    }

    private void Update()
    {
        double dist = PlaceDistanceCalculator.HaversineDistance(placeToVisit.placeCoordinates);

        if (dist<GameController.minDistance)
        {
            timeMachineButton.interactable = true;
        }else
        {
            timeMachineButton.interactable = false;
        }


        if (dist < 1)
        {
            dist *= 1000;
            unit = " m";
        } else
        {
            unit = " km";
        }

        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Distance: " + dist.ToString("F2") + unit;
    }

    public void StartTimeMachineWindow()
    {
        GameController.timeTravelPlaceSettings = placeToVisit;
        Debug.Log("setting place to visit");
        fadeInOut.StartFade();
    }
}
