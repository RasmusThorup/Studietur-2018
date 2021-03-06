﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScrollingNumbersController : MonoBehaviour {

    //public ScrollRect numbersScrollRect;

    [Range(0, 1000)]
    public float rangeFloat;
    public float randomSpinSpeed;
    public float randomSpinSpeedMargin;

    public float spindownSpeed;

    public bool keepSpinning;

    bool isSpinning;
    bool[] queueAnotherSpindown;

    float[] numbersSpinSpeed;
    float[] lerpValue;
    int[] spinDirection;
    public int[] dateNumbers = new int[8];

    ScrollRect[] _scroll_rects;
    RectTransform[] _scrollRectTransforms;
    Transform[] _listContainerTransforms;
    RectTransform[] _listContainerRectTransforms;

    Coroutine slowdownSpinCoroutine;

    string today;

    public UnityEvent numbersSpiningRandom;
    public UnityEvent numbersStopScrolling;

    int counter;

	void Start () {
        _scroll_rects = gameObject.GetComponentsInChildren<ScrollRect>();

        _scrollRectTransforms = new RectTransform[_scroll_rects.Length];
        _listContainerTransforms = new Transform[_scroll_rects.Length];
        lerpValue = new float[_scroll_rects.Length];
        numbersSpinSpeed = new float[_scroll_rects.Length];
        spinDirection = new int[_scroll_rects.Length];
        queueAnotherSpindown = new bool[_scroll_rects.Length];


        for (int i = 0; i < _scroll_rects.Length; i++)
        {
            _scrollRectTransforms[i] = _scroll_rects[i].gameObject.GetComponent<RectTransform>();
        }

        for (int i = 0; i < _scroll_rects.Length; i++)
        {
            _listContainerTransforms[i] = _scroll_rects[i].content;
        }

        for (int i = 0; i < dateNumbers.Length; i++)
        {
            numbersSpinSpeed[i]= randomSpinSpeed * Random.Range(1 - randomSpinSpeedMargin, 1 + randomSpinSpeedMargin);
        }

        for (int i = 0; i < dateNumbers.Length; i++)
        {
            spinDirection[i] = 0;
        }
    }

    private void OnEnable()
    {
        counter = 0;

        //for (int i = 0; i < dateNumbers.Length; i++)
        //{
        //    dateNumbers[i] = GameController.timeTravelPlaceSettings.timetravelData[yearselector].dates[i] * 25;
        //}
    }

    void Update () {

        //foreach (var listContainerTransform in _listContainerTransforms)
        //{
        //    listContainerTransform.localPosition = new Vector3(0, rangeFloat, 0);
        //}

        //_listContainerTransform.localPosition = new Vector3(0, rangeFloat, 0);
    }


    public void StartRandomSpin(){

        if (!isSpinning)
        {
            for (int i = 0; i < _listContainerTransforms.Length; i++)
            {
                StartCoroutine(TimetravelSpinNumber(i));
            }
            isSpinning = true;
            numbersSpiningRandom.Invoke();
            counter++;
        }
        else
        {
        }
    }


    public void SetDates(int yearIndex){

        StopAllCoroutines();

        if (yearIndex == GameController.timeTravelPlaceSettings.timetravelData.Length-1)
        {
            for (int i = 0; i < dateNumbers.Length; i++)
            {
                //Tjekker om måneden har en eller to cifre.

                if (System.DateTime.Now.Day > 9)
                {
                    today = System.DateTime.Now.Day.ToString();
                } else
                {
                    today = "0" + System.DateTime.Now.Day.ToString();
                }

                if (System.DateTime.Now.Month > 9)
                {
                    today += System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString();
                }
                else
                {
                    today += "0" + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString();
                }

                //print("Length " + today.Length);

                dateNumbers[i] = int.Parse(today[i].ToString());

                dateNumbers[i] *= 25;
            }

        } else
        {
            for (int i = 0; i < dateNumbers.Length; i++)
            {
                dateNumbers[i] = int.Parse(GameController.timeTravelPlaceSettings.timetravelData[yearIndex].timetravelDate[i].ToString());
                
                dateNumbers[i] *= 25;
            }
        }

        for (int i = 0; i < _listContainerTransforms.Length; i++)
        {
            slowdownSpinCoroutine = StartCoroutine(SetNumbersSlowdown(i));
        }

        isSpinning = false;

        if (counter != 0)
            numbersStopScrolling.Invoke();

    }


    IEnumerator SetNumbersSlowdown(int index){

        StartCoroutine(pTween.To(numbersSpinSpeed[index], 0, 1, t =>
        {
            if (_listContainerTransforms[index].localPosition.y < dateNumbers[index])
            {
                lerpValue[index] = Mathf.Lerp(_listContainerTransforms[index].localPosition.y, 0, t);
                queueAnotherSpindown[index] = true;

            }else
            {
                lerpValue[index] = Mathf.Lerp(_listContainerTransforms[index].localPosition.y, dateNumbers[index], t);
                queueAnotherSpindown[index] = false;
            }

            _listContainerTransforms[index].localPosition = new Vector3(0, lerpValue[index], 0);

        }));

        if (queueAnotherSpindown[index])
        {
            StartCoroutine(pTween.To(spindownSpeed, 0, 1, t =>
            {
                lerpValue[index] = Mathf.Lerp(250, dateNumbers[index], t);
                queueAnotherSpindown[index] = false;

                _listContainerTransforms[index].localPosition = new Vector3(0, lerpValue[index], 0);

            }));
        }

        slowdownSpinCoroutine = null;

        yield break;
    }

    IEnumerator TimetravelSpinNumber(int index){

        if (slowdownSpinCoroutine != null)
        {
            StopCoroutine(slowdownSpinCoroutine);
            slowdownSpinCoroutine = null;
        }

        yield return StartCoroutine(pTween.To(numbersSpinSpeed[index], 250, 0, t =>
        {

            Vector3 spin;
            spin = new Vector3(0, t, 0);

            _listContainerTransforms[index].localPosition = spin;

        }));

        if (keepSpinning)
        {
            StartCoroutine(TimetravelSpinNumber(index));
        }

        yield break;
    }
}
