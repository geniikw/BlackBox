using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodNightMypi.TimeAction;

public class TinyScene : MonoBehaviour {

    public TimeBehaviour enableAnimation;
    public TimeBehaviour disableAnimation;

    public virtual bool IsInitialize
    {   get
        {
            return true;
        }
    }


    public void EnableScene()
    {
        if (enableAnimation == null)
            return ;
         enableAnimation.PlayTimeAction();
    }
    public Coroutine EnableSceneInCode()
    {
        if (enableAnimation == null)
            return null;
        return enableAnimation.PlayTimeAction();
    }

    public void DisableScene()
    {
        if (disableAnimation == null)
            return ;
         disableAnimation.PlayTimeAction();
    }
    public Coroutine DisableSceneInCode()
    {
        if (disableAnimation == null)
            return null;
        return disableAnimation.PlayTimeAction();
    }
}
