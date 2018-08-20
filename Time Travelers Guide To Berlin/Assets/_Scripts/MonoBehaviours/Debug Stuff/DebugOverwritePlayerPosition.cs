using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOverwritePlayerPosition : MonoBehaviour {

    public PlaceToVisit overwriteCoord;

    public void OverwritePlayerCoordinates(){

        if (GameController.debugMode)
        {
            GPS.playerCoordinates = overwriteCoord.placeCoordinates;
        }

    }

    public void DisableDebugMenu(){
        
        if (GameController.debugMode)
        {
            transform.parent.parent.gameObject.SetActive(false);
        }
    }

}
