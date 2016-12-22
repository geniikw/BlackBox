using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodNightMypi.TimeAction;
using UnityEngine.Events;
using System;

public class CoroutineWait : TimeBehaviour {
    
    public UnityEvent OnWait;
    
    public override IEnumerator TimeAction(Action callback = null)
    {
        OnWait.Invoke();


        yield return null;

    }

   
}
