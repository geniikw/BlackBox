using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using GoodNightMypi.TimeAction;

public class StageSelector :  MonoBehaviour{
    
    public Transform SelectPosition;
    public Transform StageCube;
    public float cubeRotationTime = 2f;
    public float buttonLineTime = 1f;
    public Image ButtonImage;
    public Transform biMask;
    public List<Vector2> StagePositionList = new List<Vector2>();
    public float buttonMoveTime = 0.2f;
    public float buttonDistance = 30f;
    
    public float maskMoveTime = 0.5f;
    [Header("큐브가 도는거에대한 변수")]

    public float cubeRotateTime = 1f;
    public float cubeRotateSpeed = 3000f;
    public float cubeBreakTime = 0.3f;

    public RectTransform nextButton;
    public RectTransform backButton;
    public RectTransform nextActivePosition;
    public RectTransform backActivePosition;

    private Vector3 nextButtonHidePosition;
    private Vector3 backButtonHidePosition;
    private int currentStage = 0;
    private int clearStage = -1;


    public List<Sprite> stageSprite;
    public Sprite lockSprite;
    void Start()
    {
        nextButtonHidePosition = nextButton.transform.position;
        backButtonHidePosition = backButton.transform.position;
    }

    IEnumerator OnEnableCoroutine()
    {
        SmallSceneManager.instance.es.enabled = false;
        StageCube.GetComponent<Animator>().enabled = false;
        StageCube.GetComponent<KeepRotation>().enabled = false;
        StageCube.gameObject.SetActive(true);
        yield return StartCoroutine(InitializeStageCube());
        yield return StartCoroutine(InitializeStageButton(0));
        SmallSceneManager.instance.es.enabled = true;
    }
    
    private IEnumerator InitializeStageCube()
    {
        StageCube.position = SelectPosition.position;
        StageCube.localScale = Vector3.one * 3f;
        var sQ = StageCube.rotation;
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime / cubeRotationTime;
            StageCube.rotation = Quaternion.Lerp(sQ, Quaternion.identity, AnimationCurve.EaseInOut(0,0,1,1).Evaluate(t));

            yield return null;
        }
    }

    private IEnumerator InitializeStageButton(int stageIndex)
    {
        if(clearStage >= stageIndex)
        {
            ButtonImage.sprite = stageSprite[stageIndex];
        }
        else
        {
            ButtonImage.sprite = lockSprite;
        }
        ButtonImage.gameObject.SetActive(true);

        yield return StartCoroutine(MoveMask(new Vector2(50,50), Vector2.zero, maskMoveTime));
        StartCoroutine(TweenTransform.Position(backButton, backButtonHidePosition, buttonMoveTime));
        yield return StartCoroutine(TweenTransform.Position(nextButton, nextButtonHidePosition, buttonMoveTime));
        currentStage = stageIndex;
    }

    private IEnumerator MoveMask(Vector2 start, Vector3 end, float time)
    {
        var ButtonImageWorldPosition = ButtonImage.transform.position;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime/ time;
            biMask.transform.localPosition = Vector3.Lerp(start, end, AnimationCurve.EaseInOut(0, 0, 1, 1).Evaluate(t));
            ButtonImage.transform.position = ButtonImageWorldPosition;

            yield return null;
        }
        yield return null;
    }

    private IEnumerator DisableStageButton()
    {
        yield return StartCoroutine(MoveMask(Vector2.zero, new Vector2(50, 50), maskMoveTime));
        ButtonImage.gameObject.SetActive(false);
        StartCoroutine(TweenTransform.Position(backButton, backActivePosition.position, buttonMoveTime));
        yield return StartCoroutine(TweenTransform.Position(nextButton, nextActivePosition.position,  buttonMoveTime));
    }

    IEnumerator SwitchStage(int stage)
    {
        SmallSceneManager.instance.es.enabled = false;
        yield return StartCoroutine(DisableStageButton());

        yield return StartCoroutine(RotateHighSpeed(cubeRotateTime));

        yield return StartCoroutine(InitializeStageButton(stage));

        SmallSceneManager.instance.es.enabled = true;
    }

    IEnumerator RotateHighSpeed(float time)
    {
        float t = 0;
        var xRandom = Random.Range(-1f, 1f);
        var zRandom = Random.Range(-1f, 1f);
        while (t < 1)
        {
            t += Time.deltaTime / time;
            StageCube.Rotate(new Vector3(t * cubeRotateSpeed * Time.deltaTime* xRandom
                                        ,t * cubeRotateSpeed * Time.deltaTime
                                        ,t * cubeRotateSpeed * Time.deltaTime * zRandom));
            yield return null;
        }
        t = 0;
        var start = StageCube.rotation;
        while (t < 1)
        {
            t += Time.deltaTime / cubeBreakTime;
            StageCube.rotation = Quaternion.Lerp(start, Quaternion.identity,AnimationCurve.EaseInOut(0,0,1,1).Evaluate(t));
            yield return null;
        }

    }

    public void StageUp()
    {
        if(currentStage+1 > StagePositionList.Count-1)
        {
            Debug.LogWarning("마지막 스테이지 입니다.");
            return;
        }
        StartCoroutine(SwitchStage(currentStage + 1));
    }
    public void StageDown()
    {
        if(currentStage <= 0)
        {
            Debug.LogWarning("처음스테이지 입니다.");
            return;
        }
        StartCoroutine(SwitchStage(currentStage - 1));
    }
    private bool CheckLock()
    {
        return currentStage + 1 > clearStage + 1;
    }
    public void StartStage()
    {
        if (CheckLock())
        {
            Debug.Log("잠겨있습니다.");
            return;
        }
    }
}
