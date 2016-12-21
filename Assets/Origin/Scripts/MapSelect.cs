using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoodNightMypi.TimeAction;

public class MapSelect : MonoBehaviour {

    public Transform playerCube;
    public Text enterButtonText;

    public Button next;
    public Button back;

    public AnimatorWait enable;
    public AnimatorWait disable;

    public AnimationCurve rotateCurve = AnimationCurve.EaseInOut(0,0,1,1);

    public int selectRotateStep = 3;
    public float selectRotateStepTime = 0.2f;

    public float alphaTweenSpeed = 0.2f;

    /// <summary>
    /// 약간의 트릭으로 이전에 썼던 벡터는 안나온다.
    /// </summary>
    Vector3[] Angles = new Vector3[arrayCount] { new Vector3(0, 90, 0), new Vector3(0, -90, 0), new Vector3(90, 0, 0),
                                              new Vector3(-90,0,0),new Vector3(0,0,90), new Vector3(0,0,90)};
    const int arrayCount = 6;
    bool isFirst = true;
    Quaternion GetRandomEuler()
    {
        if (isFirst) isFirst = false;
        var idx = Random.Range(0, isFirst? arrayCount : arrayCount-1);
        SwapEulerDeck(idx, arrayCount-1);
        return Quaternion.Euler(Angles[arrayCount-1]);
    }
    void SwapEulerDeck(int a, int b)
    {
        var buffer = Angles[a];
        Angles[a] = Angles[b];
        Angles[b] = buffer;
    }
    
    IEnumerator StageSwitchAnimation(int stage) {
        SmallSceneManager.instance.input = false;

        yield return disable.PlayTimeAction();
        
        int n = 0;
        while (n++ < selectRotateStep)
            yield return StartCoroutine(TweenTransform.Rotation(playerCube, playerCube.rotation * GetRandomEuler(), selectRotateStepTime, rotateCurve));

        MapManager.instance.stage = stage;
        enterButtonText.text = (stage+1).ToString();

        if (stage + 2 > GameResources.GetMapDataCount())
            next.interactable = false;
        else
            next.interactable = true;
        if (stage <= 0)
            back.interactable = false;
        else
            back.interactable = true;

        yield return enable.PlayTimeAction();

        GetComponent<Animator>().enabled = true;
        SmallSceneManager.instance.input = true;
    }
    
    public void StageUp()
    {
        StartCoroutine(StageSwitchAnimation(MapManager.instance.stage + 1));
    }
    public void StageDown()
    {
        StartCoroutine(StageSwitchAnimation(MapManager.instance.stage - 1));
    }
}
