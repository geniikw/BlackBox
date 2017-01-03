using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodNightMypi.TimeAction;
using System;

public class AllCardDeleteWait : TimeBehaviour {
    
    public override IEnumerator TimeAction(Action callback = null)
    {
        yield return StartCoroutine(GetComponent<CardBox>().DeleteAllCard());
    }

    
}
