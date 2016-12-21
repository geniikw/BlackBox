using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodNightMypi.TimeAction;

    public class CamSizeAction : TimeBehaviour
    {

        public float targetSize = 5f;
        public Camera cam;
        public float time;
        public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public Transform back;

        public override IEnumerator TimeAction(Action callback = null)
        {
            if (cam == null)
                cam = Camera.main;


            float t = 0;
            float s = cam.orthographicSize;
            while (t < 1f)
            {
                t += Time.deltaTime/time;
            cam.orthographicSize = Mathf.Lerp(s, targetSize,curve.Evaluate(t));
                back.localScale = Vector3.one * cam.orthographicSize / 5f;
                yield return null;
            }
            if (callback != null)
                callback();
        }
    }


