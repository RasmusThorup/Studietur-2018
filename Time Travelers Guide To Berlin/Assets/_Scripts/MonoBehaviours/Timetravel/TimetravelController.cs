using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class TimetravelController : MonoBehaviour {


    public bool showDebug;

    [ShowIf("showDebug")]
    public int currentYearIndexDebug;

    public static int currentYearIndex;

    public static int[] possibleYearSelectionValues;
    public static int currentYearScriptableObjectsIndex;

    public ScrollingNumbersController scrollingNumbersController;

    public UnityEvent timeTravelPossible;
    public UnityEvent timeTravelNotPossible;

    public void SetTimetravelYearIndex(float value){
        currentYearIndex = Mathf.RoundToInt(25 - (value * 25))-1;

        if (currentYearIndex == -1)
        {
            currentYearIndex = 0;
        }
    }

    private void LateUpdate()
    {
        if (showDebug)
        {
            currentYearIndexDebug = currentYearIndex;
        }
    }

    public void CheckTimetravelPossibility()
    {
        if (possibleYearSelectionValues == null)
            return;

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
