using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodNightMypi.TimeAction;

public class TinyScene : MonoBehaviour {

    public TimeBehaviour enableAnimation;
    public TimeBehaviour disableAnimation;
    
    public void EnableScene()
    {
        if (enableAnimation == null)
            return ;
         enableAnimation.PlayTimeAction();
    }

    public void DisableScene()
    {
        if (disableAnimation == null)
            return ;
         disableAnimation.PlayTimeAction();
    }
}
