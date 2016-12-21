using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoodNightMypi.TimeAction
{
    public class AnimatorWait : TimeBehaviour
    {
        public string currentAnimationName = "Idle";
        public string animationTrigger = "Enable";
        public string animationName = "Enable";

        public GameObject target;

        public override IEnumerator TimeAction(Action callback = null)
        {
            if (target == null)
                target = gameObject;
            var anim = target.GetComponent<Animator>();

            anim.SetTrigger(animationTrigger);
            while (anim.GetCurrentAnimatorStateInfo(0).IsName(currentAnimationName)) yield return null;
            while (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName)) yield return null;

            if (callback != null)
                callback();
        }

       
    }
}

