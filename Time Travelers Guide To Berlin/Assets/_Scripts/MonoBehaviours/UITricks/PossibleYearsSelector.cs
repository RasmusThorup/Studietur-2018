using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

//[ExecuteInEditMode]
public class PossibleYearsSelector : MonoBehaviour {
    

    public Vector2 magnificationMinMax;

    [Range(0, 1)]
    public float select;

    public float margin;

    float lerp;
    float sectionAmount;

    private void Update()
    {
        //YearSelector(1-select);
    }

    private void Start()
    {
        sectionAmount = 1 / (float)transform.childCount;
    }

    private void OnEnable()
    {
        YearSelector(0);


        //Make the right circles fill out correct.

        //for (int i = transform.childCount-1; i >= 0; i--)
        //{
        //    UICircle circle = transform.GetChild(i).GetComponent<UICircle>();

            //int fillDistance = transform.childCount / GameController.timeTravelPlaceSettings.timetravelData.Length;

            //if (i%fillDistance == 0)
            //{
            //    circle.Fill = true;
            //}else
            //{
            //    circle.Fill = false;
            //}


        //}

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

            transform.GetChild(i - 1).GetComponent<LayoutElement>().minWidth = lerp;
        }

    }


}
