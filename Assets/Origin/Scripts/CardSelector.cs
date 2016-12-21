using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class CardSelector : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler{

    public card currentCardType { get { return m_currentCard.cardType; } }
    public readonly int cardKind = 6;
    public float angleUnit { get { return 180f / cardKind; } }
    public UnityEvent OnClick;

    public Color ButtonColor;

    List<Transform> m_childs = new List<Transform>();
    List<Image> m_childsImage = new List<Image>();
    List<Card> m_childCards = new List<Card>();

    public float sensibility = 0.1f;
    public float checkMagnitude = 5f;
    public float radius = 20f;

    public float clickSubmitMinTime = 0.5f;
    Card m_currentCard;
     
    float m_angle = 0f;
    bool m_isHDraging = false;
    bool m_isVDraging = false;
    bool m_isClick = false;

    void Start()
    {
        m_childs.Clear();
        m_childsImage.Clear();
        m_childCards.Clear();
        for (int n=0; n< transform.childCount; n++)
        {
            var childTrans = transform.GetChild(n);
            m_childs.Add(childTrans);
            m_childsImage.Add(childTrans.GetComponent<Image>());
            m_childCards.Add(childTrans.GetComponent<Card>());
        }
    }

    void Update()
    {
        for (int n = 0; n < transform.childCount; n++)
        {
            var child = m_childs[n];
            var image = m_childsImage[n];
            var init = Mathf.PI * 2f / transform.childCount * n + angleUnit / 180f * Mathf.PI;
            var angle = ((m_angle + init) % (Mathf.PI * 2f));

            var anglee = angle / Mathf.PI * 180f;
            var dif1 = Mathf.Min(angleUnit, Mathf.Abs(anglee - 270f));
            var dif2 = Mathf.Min(angleUnit, Mathf.Abs(anglee + 90));
            var diff = Mathf.Min(dif1, dif2);

            if(diff < 1f)
            {
                if (m_currentCard != m_childCards[n])
                {
                    m_currentCard = m_childCards[n];
                }
            }

            var a = (angleUnit - diff) / angleUnit;
            var c = ButtonColor;
            c.a = a;
            image.color = c;
            child.transform.localPosition = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
        }
    }

    IEnumerator AngleTween(float delta, float time)
    {
        SmallSceneManager.instance.input = false;
        float t = 0;
        float s = m_angle;
        float e = m_angle - delta/180f*Mathf.PI;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            m_angle = Mathf.Lerp(s, e, t);
            
            yield return null;
        }
        SmallSceneManager.instance.input = true;
    }
  
    private void ResetValues()
    {
        m_isHDraging = false;
        m_isVDraging = false;
        m_isClick = false;
        m_checker = Vector2.zero;
    }
    

    Vector2 m_checker;
    public void OnDrag(PointerEventData eventData)
    {
        if (!m_isClick)
            return;

        if (!m_isHDraging && !m_isVDraging)
            m_checker += eventData.delta;

        if (Mathf.Abs(m_checker.y) > checkMagnitude)
            m_isVDraging = true;

        if (Mathf.Abs(m_checker.x) > checkMagnitude)
            m_isHDraging = true;

        if (m_isHDraging)
        {
            m_angle += eventData.delta.x* sensibility;
            m_angle = m_angle %( Mathf.PI * 2f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_isClick)
            return;

        m_isClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_isHDraging)
        {
            var a = m_angle / Mathf.PI * 180f - Mathf.Round(m_angle / Mathf.PI * 180f / 60f) * 60f;
            StartCoroutine(AngleTween(a, 0.2f));
        }
        else
        {
            if(m_checker.magnitude < 0.1f && Time.realtimeSinceStartup- eventData.clickTime < clickSubmitMinTime)
            {
                ClickEvent();
            }
        }
        ResetValues();
    }

    public void ClickEvent()
    {
        OnClick.Invoke();
    }
}
