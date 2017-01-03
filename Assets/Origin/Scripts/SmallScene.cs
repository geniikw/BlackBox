using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoodNightMypi.TimeAction;
using UnityEngine.Events;

public class SmallScene : MonoBehaviour
{
    public List<GameObject> childs = new List<GameObject>();
    public bool isFirst = false;

    public TimeBehaviour enableAnimation;
    public TimeBehaviour disableAnimation;
    [Header("back버튼을 눌렀을 때 갈 씬.")]
    public SmallScene prevScene;

    public UnityEvent OnBackButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButton.Invoke();
        }
    }

    /// <summary>
    /// SendMessageReceiver
    /// </summary>
    public void EndScene(SmallScene scene)
    {
        SmallSceneManager.SwitchScene(scene);
    }

    public void EndSceneByidx(int sceneidx)
    {
        SmallSceneManager.SwitchScene(sceneidx);
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

