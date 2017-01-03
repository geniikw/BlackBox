using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCircle : MonoBehaviour, IDragHandler
{
    public CameraCircleMove cam;
    public float dragVerticalSensitivity = 1f;

    public float dragHorizontalSensitivity = 1f;
    
    public void OnDrag(PointerEventData eventData)
    {
        cam.currentAngle += eventData.delta.x* dragVerticalSensitivity;
        cam.height += eventData.delta.y* dragHorizontalSensitivity;
        cam.height = Mathf.Clamp(cam.height, -10f, 10f);
    }
}
