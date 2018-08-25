﻿/// Credit Tomasz Schelenz 
/// Sourced from - https://bitbucket.org/ddreaper/unity-ui-extensions/issues/46/feature-uiknob#comment-29243988

using UnityEngine.Events;
using UnityEngine.EventSystems;
using NaughtyAttributes;
using System.Collections;
using FMODUnity;
/// <summary>
/// KNOB controller
/// 
/// Fields
/// - direction - direction of rotation CW - clockwise CCW - counter clock wise
/// - knobValue - Output value of the control
/// - maxValue - max value knob can rotate to, if higher than loops value or set to 0 - it will be ignored, and max value will be based on loops
/// - loops - how any turns around knob can do
/// - clampOutput01 - if true the output knobValue will be clamped between 0 and 1 regardless of number of loops.
/// - snapToPosition - snap to step. NOTE: max value will override the step.
/// - snapStepsPerLoop - how many snap positions are in one knob loop;
/// - OnValueChanged - event that is called every frame while rotationg knob, sends <float> argument of knobValue
/// NOTES
/// - script works only in images rotation on Z axis;
/// - while dragging outside of control, the rotation will be cancelled
/// </summary>
/// 
namespace UnityEngine.UI.Extensions
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UI/Extensions/UI_Knob")]
    public class UI_Knob : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler
    {
        public enum Direction { CW, CCW };
        [Tooltip("Direction of rotation CW - clockwise, CCW - counterClockwise")]
        public Direction direction = Direction.CW;
        [ReadOnly]
        public float knobValue;
        [Tooltip("Max value of the knob, maximum RAW output value knob can reach, overrides snap step, IF set to 0 or higher than loops, max value will be set by loops")]
        public float maxValue = 0;
        [Tooltip("How many rotations knob can do, if higher than max value, the latter will limit max value")]
        public int loops = 1;
        [Tooltip("Clamp output value between 0 and 1, usefull with loops > 1")]
        public bool clampOutput01 = false;
        [Tooltip("snap to position?")]
        public bool snapToPosition = false;
        [Tooltip("Number of positions to snap")]
        public int snapStepsPerLoop = 10;
        [Space(30)]
        public KnobFloatValueEvent OnValueChanged;
        private float _currentLoops = 0;
        private float _previousValue = 0;
        private float _initAngle;
        private float _currentAngle;
        private Vector2 _currentVector;
        private Quaternion _initRotation;
        private bool _canDrag = false;

        //Hack thing
        Quaternion startRotation;
        Coroutine lerpSnapCoroutine;

        public bool useLerpSnap;
        bool _canUseLerpSnap;
        public float lerpSpeed;

        [ReadOnly]
        public float sendingValue;

        public UnityEvent KnoPointerDown;
        public UnityEvent KnobBeginDrag;
        public UnityEvent KnobStoppedDrag;
        public UnityEvent KnobEnter;
        bool pointerDown;

        //FMOD 
        int dragCounter;
        public int distancePerClick;
        public StudioEventEmitter clockTickEvent;

        private void Start()
        {
            startRotation = transform.rotation;
        }

        private void OnEnable()
        {
            transform.rotation = startRotation;
            knobValue = 0;
            _currentLoops = 0;
            InvokeEvents(0);

            _previousValue = 0;

            KnobStoppedDrag.Invoke();

            //FMOD Stuff
            dragCounter = 0;
        }

        //private void Update()
        //{
        //    newKnobValue = knobValue;
        //    knobSpeed = Mathf.Abs(oldKnobValue - newKnobValue * 2);
        //    oldKnobValue = newKnobValue;
        //    clockTickEvent.SetParameter("Speed", knobSpeed);
        //}

        IEnumerator StartLerpSnap(float coroutineKnobValue){
            
            float snapStep = 1 / (float)snapStepsPerLoop;
            float newValue = Mathf.Round(coroutineKnobValue / snapStep) * snapStep;

            float knobValueHolder = coroutineKnobValue;

            yield return StartCoroutine(pTween.To(lerpSpeed, 0, 1, t =>
            {
                float invokeValue = Mathf.Lerp(knobValueHolder, newValue, t);
                transform.eulerAngles = new Vector3(0, 0, 360 * invokeValue);
                InvokeEvents(invokeValue + _currentLoops);
            }));

            lerpSnapCoroutine = null;

            yield break;
        }

        IEnumerator LerpValue(){

            yield break;
        } 

        //ONLY ALLOW ROTATION WITH POINTER OVER THE CONTROL
        public void OnPointerDown(PointerEventData eventData)
        {
            _canDrag = true;

            if (lerpSnapCoroutine != null)
            {
                StopCoroutine(lerpSnapCoroutine);
                lerpSnapCoroutine = null;
            }

            KnoPointerDown.Invoke();

            pointerDown = true;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _canDrag = false;

            if (useLerpSnap&& _canUseLerpSnap)
                lerpSnapCoroutine = StartCoroutine(StartLerpSnap(knobValue));
            
            _canUseLerpSnap = false;

            KnobStoppedDrag.Invoke();

            pointerDown = false;

        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _canDrag = true;

            if (lerpSnapCoroutine != null)
            {
                StopCoroutine(lerpSnapCoroutine);
                lerpSnapCoroutine = null;
            }

            if (pointerDown)
            {
                KnobEnter.Invoke();
            }
           
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _canDrag = false;

            if (useLerpSnap && _canUseLerpSnap)
                lerpSnapCoroutine = StartCoroutine(StartLerpSnap(knobValue));

            _canUseLerpSnap = false;

            KnobStoppedDrag.Invoke();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            SetInitPointerData(eventData);
            _canUseLerpSnap = true;

            KnobBeginDrag.Invoke();
        }
        void SetInitPointerData(PointerEventData eventData)
        {
            _initRotation = transform.rotation;
            _currentVector = eventData.position - (Vector2)transform.position;
            _initAngle = Mathf.Atan2(_currentVector.y, _currentVector.x) * Mathf.Rad2Deg;
        }
        public void OnDrag(PointerEventData eventData)
        {
            //CHECK IF CAN DRAG
            if (!_canDrag)
            {
                SetInitPointerData(eventData);
                return;
            }
            _currentVector = eventData.position - (Vector2)transform.position;
            _currentAngle = Mathf.Atan2(_currentVector.y, _currentVector.x) * Mathf.Rad2Deg;

            Quaternion addRotation = Quaternion.AngleAxis(_currentAngle - _initAngle, this.transform.forward);
            addRotation.eulerAngles = new Vector3(0, 0, addRotation.eulerAngles.z);

            Quaternion finalRotation = _initRotation * addRotation;

            if (direction == Direction.CW)
            {
                knobValue = 1 - (finalRotation.eulerAngles.z / 360f);

                if (snapToPosition)
                {
                    SnapToPosition(ref knobValue);
                    finalRotation.eulerAngles = new Vector3(0, 0, 360 - 360 * knobValue);
                }
            }
            else
            {
                knobValue = (finalRotation.eulerAngles.z / 360f);

                if (snapToPosition)
                {
                    SnapToPosition(ref knobValue);
                    finalRotation.eulerAngles = new Vector3(0, 0, 360 * knobValue);
                }
            }

            //PREVENT OVERROTATION
            if (Mathf.Abs(knobValue - _previousValue) > 0.5f)
            {
                if (knobValue < 0.5f && loops > 1 && _currentLoops < loops - 1)
                {
                    _currentLoops++;
                }
                else if (knobValue > 0.5f && _currentLoops >= 1)
                {
                    _currentLoops--;
                }
                else
                {
                    if (knobValue > 0.5f && _currentLoops == 0)
                    {
                        knobValue = 0;
                        transform.localEulerAngles = Vector3.zero;
                        SetInitPointerData(eventData);
                        InvokeEvents(knobValue + _currentLoops);
                        return;
                    }
                    else if (knobValue < 0.5f && _currentLoops == loops - 1)
                    {
                        knobValue = 1;
                        transform.localEulerAngles = Vector3.zero;
                        SetInitPointerData(eventData);
                        InvokeEvents(knobValue + _currentLoops);
                        return;
                    }
                }
            }

            //CHECK MAX VALUE
            if (maxValue > 0)
            {
                if (knobValue + _currentLoops > maxValue)
                {
                    knobValue = maxValue;
                    float maxAngle = direction == Direction.CW ? 360f - 360f * maxValue : 360f * maxValue;
                    transform.localEulerAngles = new Vector3(0, 0, maxAngle);
                    SetInitPointerData(eventData);
                    InvokeEvents(knobValue);
                    return;
                }
            }

            transform.rotation = finalRotation;
            InvokeEvents(knobValue + _currentLoops);

            _previousValue = knobValue;


            //Fmod stuff
            dragCounter++;
            if (dragCounter % distancePerClick == 0)
            {
                clockTickEvent.Play();
            }

        }
        private void SnapToPosition(ref float knobValue)
        {
            float snapStep = 1 / (float)snapStepsPerLoop;
            float newValue = Mathf.Round(knobValue / snapStep) * snapStep;
            knobValue = newValue;
        }
        private void InvokeEvents(float value)
        {
            if (clampOutput01)
                value /= loops;

            if (value>/*0.96f*/ 1 - (1 / (snapStepsPerLoop * loops)))
            {
                value = /*0.96f*/1 - (1 / (snapStepsPerLoop * loops));
            }

            OnValueChanged.Invoke(value);

            sendingValue = value;

        }

    }

    [System.Serializable]
    public class KnobFloatValueEvent : UnityEvent<float> { }

    //public class UserLiftedEvent : UnityEvent { }

}