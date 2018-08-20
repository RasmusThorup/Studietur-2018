﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimetravelController : MonoBehaviour {

    public static int currentYearIndex;
    public static int[] possibleYearSelectionValues;
    public static int currentYearScriptableObjectsIndex;

    public ScrollingNumbersController scrollingNumbersController;

    public UnityEvent timeTravelPossible;
    public UnityEvent timeTravelNotPossible;

    public void SetTimetravelYearIndex(float value){
        currentYearIndex = Mathf.RoundToInt(25 - (value * 25))-1;
    }

    public void CheckTimetravelPossibility()
    {

        if (possibleYearSelectionValues[currentYearIndex] != -1)
        {
            //Jump in time
            scrollingNumbersController.SetDates(possibleYearSelectionValues[currentYearIndex]);

            currentYearScriptableObjectsIndex = possibleYearSelectionValues[currentYearIndex];

            timeTravelPossible.Invoke();

        }
        else
        {
            timeTravelNotPossible.Invoke();
            //Spin to win
        }


    }

}