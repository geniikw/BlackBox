using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CardBox : MonoBehaviour {

    public int cardVisualMaxCount = 15;
    public CardSelector cardSelector;
    public float xUnit = 18.7f;
    public Button playButton;

    public float cardAddTime = 0.2f;
    public float cardAdjustTime = 0.2f;

    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    public int cardBoxLimit = 15;//other까지 활용한 최대 갯수
    List<card> m_cardBox = new List<card>();//실직적인 데이터
    List<Card> m_cardPool = new List<Card>();//pool
    List<Transform> m_viewList = new List<Transform>();//보여지고있는 cardlist

    public void Clear()
    {
        m_cardBox.Clear();

        m_viewList.ForEach(t => t.gameObject.SetActive(false));
    }

    int m_viewFirstIndex = 0;
    
    Vector3 nextCardSlotLocalPosition
    {
        get
        {
            return transform.TransformPoint(transform.Find("Deck/Anchor").localPosition + new Vector3(xUnit * (m_viewList.Count - 1), 0, 0));
        }
    }

    Vector3 GetCardSlotPostiion(int index)
    {
        return transform.TransformPoint(transform.Find("Deck/Anchor").localPosition + new Vector3(xUnit * index, 0, 0));
    }

    private void Start()
    {
        var deck = transform.Find("Deck");
        for(int n =0;n< deck.childCount;n++)
        {
            var card = deck.GetChild(n).GetComponent<Card>();
            if (card != null)
                m_cardPool.Add(card);
        }
    }
    
    IEnumerator AddCardSeq()
    {
        SmallSceneManager.instance.input = false;

        if (m_viewList.Count == m_cardPool.Count)
        {
            yield return StartCoroutine(FullToOther());
        }

        var c = GetCardFromPool();
        c.cardType = cardSelector.currentCardType;
        c.InitGraphic();
        c.transform.position = cardSelector.transform.position;
       
        yield return StartCoroutine(TweenTransform.Position(c.transform, nextCardSlotLocalPosition, cardAdjustTime,curve)); ;
        m_viewList.Add(c.transform);

        if (m_cardBox.Count >= 1) { playButton.interactable = true; }

        SmallSceneManager.instance.input = true;


    }
    IEnumerator FullToOther()
    {
        //다찼을때 모든 카드가 ...으로 뭉침.
        yield return null;
    }
    IEnumerator OtherSeq()
    {
        SmallSceneManager.instance.input = false;
       
        yield return null;

        SmallSceneManager.instance.input = true;
    }
    IEnumerator DeleteSeq(int viewIdx)
    {
        SmallSceneManager.instance.input = false;
        var moveList = m_viewList.Where(t => m_viewList.IndexOf(t) > viewIdx);

        var idx = viewIdx-1;
        foreach (var trans in moveList)
        {
            StartCoroutine(TweenTransform.Position(trans, GetCardSlotPostiion(idx++), cardAdjustTime, curve));
        }
        m_viewList[viewIdx].gameObject.SetActive(false);
        m_viewList.RemoveAt(viewIdx);

        yield return null;


        if (m_cardBox.Count == 0) { playButton.interactable = false; }
        SmallSceneManager.instance.input = true;
    }
    
    public Coroutine AddCard()
    {
        if(m_cardBox.Count >= cardBoxLimit)
        {
            Debug.LogWarning("cardBox is full. cardbox.Count : "+m_cardBox.Count +" max : "+cardBoxLimit);
            return null;
        }
        else
        {
            m_cardBox.Add(cardSelector.currentCardType);
            return StartCoroutine(AddCardSeq());
        }
    }

    public Coroutine Other()
    {
        //비주얼만 건듬.
        return StartCoroutine(OtherSeq());
    }

    public Coroutine Delete(Transform toDel)
    {
        var index = (m_viewList.IndexOf(toDel) + m_viewFirstIndex);
        m_cardBox.RemoveAt(index);
        return StartCoroutine(DeleteSeq(index));
    }
    public Card GetCardFromPool()
    {
        var card = m_cardPool.First(c => !c.gameObject.activeInHierarchy);
        card.gameObject.SetActive(true);
        card.InitGraphic();
        return card;
    }

}
