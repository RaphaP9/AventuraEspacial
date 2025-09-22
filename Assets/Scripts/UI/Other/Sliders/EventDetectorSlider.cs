using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class EventDetectorSlider : Slider, IBeginDragHandler, IEndDragHandler
{
    public event EventHandler OnDragStart;
    public event EventHandler OnDragEnd;
    public event EventHandler OnUpPointer;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDragStart?.Invoke(this, EventArgs.Empty);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEnd?.Invoke(this, EventArgs.Empty);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        OnUpPointer?.Invoke(this, EventArgs.Empty);
    }
}