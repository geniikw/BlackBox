using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Image))]
public class Card : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler {

    Image m_image;
    public Image image { get { return m_image; } }
    
    private readonly Vector3 backCardEulerAngle = new Vector3(60, 0, 0);

    public card cardType;
    public int currentIndex;

    readonly float yMaxRatio = 0.1f;
    readonly float ySubmitRatio = 0.06f;
    
    float yMax    { get { return Screen.height * yMaxRatio; }  }   
    float ySubmit { get { return Screen.height * ySubmitRatio; } }

    Vector3 m_pressPosition;
    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_circle = Camera.main.GetComponent<CameraCircleMove>();
    }
    private void OnEnable()
    {
        InitGraphic();
    }
    private void Update()
    {
        if(cardType != card.Up && cardType != card.Down)
        {
            var y = -m_circle.currentAngle;
            switch (cardType)
            {
                case card.Front: y += 90; break;
                case card.Back: y += -90; break;
                case card.Right: break;
                case card.Left: y += 180; break;
            }

            transform.rotation = Quaternion.Euler(backCardEulerAngle.x, 0, y);
        }
    }    
    /// <summary>
    /// 보간 x
    /// </summary>
    public void InitGraphic()
    {
        switch (cardType)
        {
            case card.Back: 
            case card.Front:
            case card.Left: 
            case card.Right:
                image.sprite = GameResources.GetDirection(card.Back);
                transform.rotation = Quaternion.Euler(backCardEulerAngle);
                break;
            case card.Up:
                transform.rotation = Quaternion.identity;
                image.sprite = GameResources.GetDirection(card.Up);
                break;
            case card.Down:
                transform.rotation = Quaternion.identity;
                image.sprite = GameResources.GetDirection(card.Down);
                break;
        }
    }

    CameraCircleMove m_circle;
    public bool isIndeck = true;

    //interface implement
    public void OnDrag(PointerEventData eventData)
    {
        var d = eventData.position-eventData.pressPosition;
        d.y = Mathf.Clamp(d.y, 0f, yMax);
        transform.position = m_pressPosition + new Vector3(0, d.y, 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        var d = eventData.position - eventData.pressPosition;

        if(d.y > ySubmit)
        {
            ///bad code.
            transform.parent.parent.GetComponent<CardBox>().Delete(transform);
        }
        else
        {
            transform.position = m_pressPosition;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_pressPosition = transform.position;
    }
}
