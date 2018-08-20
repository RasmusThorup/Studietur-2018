﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class ScrollingNumbersController : MonoBehaviour {

    //public ScrollRect numbersScrollRect;

    [Range(0, 1000)]
    public float rangeFloat;
    public float randomSpinSpeed;
    public float randomSpinSpeedMargin;
    public int[] dateNumbers;

    public float spindownSpeed;
    public AnimationCurve spindownCurve;

    public bool keepSpinning;

    bool isSpinning;
    public bool[] queueAnotherSpindown;

    float[] numbersSpinSpeed;
    float[] lerpValue;
    int[] spinDirection;

    ScrollRect[] _scroll_rects;
    RectTransform[] _scrollRectTransforms;
    Transform[] _listContainerTransforms;
    RectTransform[] _listContainerRectTransforms;

    public int yearselector;

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

        //for (int i = 0; i < dateNumbers.Length; i++)
        //{
        //    dateNumbers[i] = dateNumbers[i] * 25;
        //}

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

        }else
        {
        }
    }


    public void SetDates(int yearIndex){

        StopAllCoroutines();

        for (int i = 0; i < dateNumbers.Length; i++)
        {
            dateNumbers[i] = GameController.timeTravelPlaceSettings.timetravelData[yearIndex].dates[i] * 25;
        }

        for (int i = 0; i < _listContainerTransforms.Length; i++)
        {
            StartCoroutine(SetNumbersSlowdown(i));
        }

        isSpinning = false;

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

        yield break;
    }

    IEnumerator TimetravelSpinNumber(int index){
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