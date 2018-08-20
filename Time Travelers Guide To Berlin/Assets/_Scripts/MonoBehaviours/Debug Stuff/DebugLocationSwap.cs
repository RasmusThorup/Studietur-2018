using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugLocationSwap : MonoBehaviour {

    public GameObject placesToVisitGO;
    public GameObject overwriteCoordPrefab;

    PlaceToVisit placeToVisit;

	void Start () {
		
        for (int i = 0; i < placesToVisitGO.transform.childCount; i++)
        {
            GameObject newOverwriteGO = Instantiate(overwriteCoordPrefab, transform);
            PlaceSetup placeToVisitSetup = placesToVisitGO.transform.GetChild(i).GetComponent<PlaceSetup>();

            newOverwriteGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = placeToVisitSetup.placeToVisit.placeName;
            newOverwriteGO.GetComponent<DebugOverwritePlayerPosition>().overwriteCoord = placeToVisitSetup.placeToVisit;
        }

	}
	

	void Update () {
		

	}
}
