using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class EventDetectorSlider : Slider, IBeginDragHandler, IEndDragHandler
{
    public event EventHandler OnDragStart;
    public event EventHandler OnDragEnd;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDragStart?.Invoke(this, EventArgs.Empty);
        Debug.Log("Start Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEnd?.Invoke(this, EventArgs.Empty);
        Debug.Log("End Drag");
    }
}