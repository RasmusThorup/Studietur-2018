﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.Events;

public class TimeTravelSetup : MonoBehaviour
{
    [Required]
    public GameObject timetravelYearPrefab;

    public GameObject content;

    public UnityEvent onTimetravelSetup;
    bool doTheThing = false;

    private void OnEnable()
    {

        if (content == null)
        {
            content = gameObject;
        }

        if (!content.activeInHierarchy)
        {
            content.SetActive(true);
        }

        if (doTheThing)
        {
            onTimetravelSetup.Invoke();
        }

        doTheThing = true;
    }

    public void StartTimetraveling(){
        
        GameController.isTimeTraveling = false;
        Input.location.Stop();

        content.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameController.timeTravelPlaceSettings.name;

        content.transform.GetChild(2).GetComponent<Image>().sprite = GameController.timeTravelPlaceSettings.icon;


        //Transform timeTravelSlider = content.transform.GetChild(3);

        // Old code from before updated UI
        //HorizontalScrollSnap hss = timeTravelSlider.GetComponent<HorizontalScrollSnap>();
        //GameObject[] childOut = new GameObject[0];
        //hss.RemoveAllChildren(out childOut);
        //if (childOut.Length > 0)
        //{
        //    foreach (var child in childOut)
        //    {
        //        Destroy(child.gameObject);
        //    }
        //}
        //for (int i = 0; i < GameController.timeTravelPlaceSettings.timetravelData.Length; i++)
        //{
        //    var newTimetravelYear = GameObject.Instantiate(timetravelYearPrefab);
        //    newTimetravelYear.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameController.timeTravelPlaceSettings.timetravelData[i].timetravelText;
        //    hss.AddChild(newTimetravelYear);
        //}
        //hss.GoToScreen(GameController.timeTravelPlaceSettings.timetravelData.Length - 1);
    }

    public void StopTimetraveling(){

        GameController.isTimeTraveling = false;

    }

    public void DisableTimetravelWindow(){
        gameObject.SetActive(false);
    }

    public void DisableTimetravelContent()
    {
        content.SetActive(false);
    }
        

    private void OnDisable()
    {
        
    }

}

