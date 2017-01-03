using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearCoroutine : MonoBehaviour {

    public Text score;
    public Text maxCut;
    public Text cut;
    public Button next;

    private void OnEnable()
    {
        var stage = MapManager.instance.stage;
        if (stage + 2 > GameResources.GetMapDataCount() || stage > PlayerData.instance.m_playerData.clearRecord.Count - 1)
            next.interactable = false;
        else
            next.interactable = true;

        StartCoroutine(OnEnableSeq());
    }

    IEnumerator OnEnableSeq()
    {
        yield return null;
    }
}
