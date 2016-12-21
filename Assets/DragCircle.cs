using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCircle : MonoBehaviour, IDragHandler
{
    public CameraCircleMove cam;
        
    public void OnDrag(PointerEventData eventData)
    {
        cam.currentAngle += eventData.delta.x;
    }
}
