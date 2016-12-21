using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoodNightMypi.TimeAction;

public class SmallScene : MonoBehaviour
{
    public List<GameObject> childs = new List<GameObject>();
    public bool isFirst = false;

    public TimeBehaviour enableAnimation;
    public TimeBehaviour disableAnimation;


    /// <summary>
    /// SendMessageReceiver
    /// </summary>
    public void EndScene(SmallScene scene)
    {
        SmallSceneManager.SwitchScene(scene);
    }

    public void SetActiveScene(bool active)
    {
        foreach (var child in childs)
        {
            child.SetActive(active);
        }
        gameObject.SetActive(active);
    }

    public Coroutine EnableSequence()
    {
        if (enableAnimation == null)
            return null;
        return enableAnimation.PlayTimeAction();
    }
    public Coroutine DisableSequence()
    {
        if (disableAnimation == null)
            return null;
        return disableAnimation.PlayTimeAction();
    }
}

