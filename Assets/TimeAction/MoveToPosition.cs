using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoodNightMypi.TimeAction
{
    public class MoveToPosition : TimeBehaviour
    {
        public Vector3 endVector;
        public Transform target;
        public float time = 0.2f;
        public AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);

        public override IEnumerator TimeAction(Action callback = null)
        {
            var target = this.target == null ? transform:this.target;
            float t = 0;

            var start = target.localPosition;
            var end = target.localPosition + endVector;

            while(t < 1f)
            {
                t += Time.deltaTime/ time;

                target.localPosition = Vector3.Lerp(start, end,  curve.Evaluate(t));

                yield return null;
            }

            if (callback != null)
                callback();
        }
    }

}

