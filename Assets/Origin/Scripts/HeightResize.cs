using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightResize : MonoBehaviour {

    public RectTransform cardDeck;
    public RectTransform canvasRect;
    public float minus;
    private void Start()
    {
        var size = canvasRect.rect.height - cardDeck.rect.height- minus;
        var rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        rect.anchoredPosition = new Vector2(0, -size / 2f);

    }


}
