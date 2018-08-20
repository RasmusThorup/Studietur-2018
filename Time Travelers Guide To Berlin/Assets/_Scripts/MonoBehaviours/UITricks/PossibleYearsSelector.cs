using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using NaughtyAttributes;

//[ExecuteInEditMode]
public class PossibleYearsSelector : MonoBehaviour
{
    public Vector2 magnificationMinMax;

    [Range(0, 1)]
    public float select;

    public float margin;

    float lerp;
    float sectionAmount;

    float enabledChildrenCounter;

    public bool[] possibleYearSelections;

    private void Start()
    {
        sectionAmount = 1 / (float)transform.childCount;
        possibleYearSelections = new bool[transform.childCount];
        TimetravelController.possibleYearSelectionValues = new int[transform.childCount];
    }

    private void OnEnable()
    {
        YearSelector(0);

        //Make the right circles fill out correct.

        int dateIndex = 0;

        int fillDistance = Mathf.FloorToInt((transform.childCount-1) / (GameController.timeTravelPlaceSettings.timetravelData.Length-1));

        for (int i = 0; i < transform.childCount; i++)
        {
            UICircle circle = transform.GetChild(i).GetComponent<UICircle>();

            if (i % fillDistance == 0 /*|| i == 0 */|| i == transform.childCount - 1)
            {
                circle.Fill = true;
                possibleYearSelections[i] = true;
                TimetravelController.possibleYearSelectionValues[i] = dateIndex;
                dateIndex++;
            }
            else
            {
                circle.Fill = false;
                possibleYearSelections[i] = false;
                TimetravelController.possibleYearSelectionValues[i] = -1;
            }
        }
    }
    public void YearSelector(float selection){

        float currentSection;
        float lerpAmount;

        float newSelection = 1 - selection;

        for (int i = 1; i <= transform.childCount; i++)
        {
            
            currentSection = sectionAmount * i;

            if (newSelection <= currentSection && newSelection > currentSection - margin)
            {
                lerpAmount = Mathf.InverseLerp(currentSection, currentSection - margin, newSelection);

                lerp = Mathf.Lerp(magnificationMinMax.y, magnificationMinMax.x, lerpAmount);

            }
            else if (newSelection > currentSection && newSelection < currentSection + margin)
            {
                lerpAmount = Mathf.InverseLerp(currentSection, currentSection + margin, newSelection);

                lerp = Mathf.Lerp(magnificationMinMax.y, magnificationMinMax.x, lerpAmount);
            }
            else
            {
                lerp = magnificationMinMax.x;
            }

            transform.GetChild(i-1).GetComponent<LayoutElement>().minWidth = lerp;
        }

    }



}
