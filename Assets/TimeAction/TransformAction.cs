using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoodNightMypi.TimeAction
{
    
    public class TransformAction : TimeBehaviour
    {
        public bool tweenPosition = true;
        public bool tweenScale = false;
        public bool tweenRotation = false;
        public Transform m_target;
        public Transform moveTo;
        public float time = 1f;
        public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public override IEnumerator TimeAction(Action callback = null)
        {
            var t = 0f;
            var target = m_target == null ? transform : m_target;

            var startPos = target.position;
            var startScale = target.localScale;
            var startRot = target.rotation;

            while (t < 1f)
            {
                t += Time.deltaTime / time;

                if(tweenPosition)
                    target.position = Vector3.Lerp(startPos, moveTo.position, curve.Evaluate(t));
                if (tweenScale)
                    target.localScale = Vector3.Lerp(startScale, moveTo.localScale, curve.Evaluate(t));
                if (tweenRotation)
                    target.rotation = Quaternion.Lerp(startRot, moveTo.rotation, curve.Evaluate(t));
                
                yield return null;
            }

            if (tweenPosition)
                target.position = moveTo.position;
            if (tweenScale)
                target.localScale = moveTo.localScale;
            if (tweenRotation)
                target.rotation = moveTo.rotation; 


            if (callback != null)
                callback();
        }
    }
}

