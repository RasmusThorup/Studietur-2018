using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGPSCoordinates : MonoBehaviour {

    //TextMesh textMesh;
    TextMeshProUGUI textMesh;
    public bool constantUpdate;
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayGPS(){
        textMesh.text = "Latitude: " + GPS.Instance.longitude.ToString() + "\n Longitude: " + GPS.Instance.latitude.ToString();
    }

    private void Update()
    {
        if (constantUpdate)
        {
            textMesh.text = "Latitude: " + GPS.playerCoordinates.x.ToString() + "\n Longitude: " + GPS.playerCoordinates.y.ToString();
        }
    }
}
