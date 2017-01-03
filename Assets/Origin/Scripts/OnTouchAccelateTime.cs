using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnTouchAccelateTime : MonoBehaviour ,IPointerDownHandler,IPointerUpHandler{

    public float TimeScaleMax = 4f;
    public float accelateSpeed = 3f;

    bool isDown = false;
    float currentScale = 1f;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDown)
            return;
        isDown = true;
        
    }

    public void Update()
    {
        if (isDown)
        {
            currentScale += Time.deltaTime* accelateSpeed;
        }
        else
        {
            currentScale -= Time.deltaTime* accelateSpeed;
        }

        currentScale = Mathf.Clamp(currentScale, 1f, TimeScaleMax);


        Time.timeScale = currentScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }

    public void OnDisable()
    {
        Time.timeScale = 1f;
    }

}
