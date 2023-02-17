using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private PointerEventData _pointer;

    public static Action<Vector2> OnDownEvent;
    public static Action<Vector2> OnUpEvent;
    public static Action<Vector2> OnDragEvent;

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointer = eventData;
        OnDownEvent?.Invoke(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUpEvent?.Invoke(eventData.position);
    }
}
