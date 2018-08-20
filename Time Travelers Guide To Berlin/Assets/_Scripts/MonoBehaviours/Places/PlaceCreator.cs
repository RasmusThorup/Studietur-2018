using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

//[ExecuteInEditMode]
public class PlaceCreator : MonoBehaviour
{
    [Required]
    public GameObject placePrefab;

    public PlaceToVisit[] placesToVisit;


    [Button("Update places to visit")]
    private void UpdatePlacesToVisit()
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
        }

        foreach (var child in children)
        {
            DestroyImmediate(child);
        }

        for (int i = 0; i < placesToVisit.Length; i++)
        {
            GameObject newPlace = Instantiate(placePrefab, transform);
            PlaceSetup newPlaceUI = newPlace.GetComponent<PlaceSetup>();
            newPlaceUI.placeToVisit = placesToVisit[i];

            newPlaceUI.UpdatePlaceInfo();
        }

        enabled = false;
    }
}
