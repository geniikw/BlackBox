using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoodNightMypi.TimeAction;

public class Ingame : MonoBehaviour {

    public TimeBehaviour mapDestroy;
    public TimeBehaviour mapCreate;

    public CardBox cardBox;
    public Text cutView;

    public Transform playerCube;

    int m_cutRemain = 0;

    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void OnEnable()
    {
        GameInit();
    }
    public void GameInit()
    {
        cutRemain = GameResources.GetMapData(MapManager.instance.stage).cutLimit;
    }

    public int cutRemain
    {
        set
        {
            if (cutView != null)
                cutView.text = value.ToString();
            m_cutRemain = value; ;
        }
        get
        {
            return m_cutRemain;
        }

    }
    public void CutMinus()
    {
        cutRemain--;
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        SmallSceneManager.instance.input = false;
        //init
        var initCamPos = Camera.main.transform.position;
        Camera.main.GetComponent<CameraCircleMove>().enabled = false;
        Camera.main.GetComponent<CameraSizeTool>().SetCamSize(3f, 0.2f);
        Camera.main.transform.rotation = Quaternion.identity;
        yield return TweenTrans.Position(Camera.main.transform, MapManager.instance.GetCamPosition(0) + Vector3.down, 0.2f);

        Vector3 currentPos = MapManager.instance.currentRoute[0] + Vector3.down;
        var n = 0;
        bool gameOver = false;
        while(cardBox.cardCount > 0)
        {
            var c = cardBox.firstCard;
            yield return StartCoroutine(cardBox.DeleteSingleCardInCode(0));
            currentPos += GetCardDir(c);
            if (MapManager.instance.currentRoute.Contains(currentPos))
            {
                yield return GoRoute(n++, 0.2f);
            }
            else
            {
                gameOver = true;
                break;
            }   
        }
        Debug.Log("RouteEnd");
        var score = Mathf.Max(cutRemain + 3, 0);
        if (currentPos.y != MapManager.instance.currentSize.y-1 || gameOver)
        {
            Debug.Log("GameOver");
            TinySceneManager.instance.SetScene("GameOver");
        }
        else
        {
            PlayerData.instance.ClearStage(MapManager.instance.stage, Mathf.Clamp(cutRemain+3,0,3));
            PlayerData.instance.SaveData();

            Debug.Log("GameClear");
            TinySceneManager.instance.SetScene("GameClear");
        }

        yield return StartCoroutine(PlayEnd(initCamPos));
    }

    IEnumerator PlayEnd(Vector3 camInitPos)
    {
        Camera.main.GetComponent<CameraSizeTool>().SetCamSize(5f, 0.2f);
        yield return TweenTrans.Position(Camera.main.transform, camInitPos, 0.2f);
        Camera.main.GetComponent<CameraCircleMove>().enabled = true;
        SmallSceneManager.instance.input = true;
    }

    public void ResetStage(int offset)
    {
        var stage = MapManager.instance.stage + offset;
        StartCoroutine(ResetStageSeq(stage));
    }
    IEnumerator ResetStageSeq(int stage)
    {
        SmallSceneManager.instance.input = false;
        yield return mapDestroy.PlayTimeAction();
        
        yield return StartCoroutine(cardBox.DeleteAllCard());
        MapManager.instance.stage = stage;
        GameInit();
        
        yield return mapCreate.PlayTimeAction();
        SmallSceneManager.instance.input = true;
    }


    IEnumerator GoRoute(int index,float time)
    {
        StartCoroutine(TweenTrans.Position(Camera.main.transform, MapManager.instance.GetCamPosition(index), time, curve));
        yield return StartCoroutine(TweenTrans.Position(playerCube, MapManager.instance.GetRoutePosition(index), time, curve));
    }

    Vector3 GetCardDir(card c)
    {
        switch (c)
        {
            case card.Back:return new Vector3(0, 0, -1);              
            case card.Front:return new Vector3(0, 0, 1);
            case card.Left:return new Vector3(-1, 0, 0);
            case card.Right:return new Vector3(1, 0, 0);
            case card.Down:return new Vector3(0, -1, 0);
            case card.Up:return new Vector3(0, 1, 0);
            case card.None:return new Vector3(0, 0, 0);
        }
        return Vector3.zero;
    }


}
