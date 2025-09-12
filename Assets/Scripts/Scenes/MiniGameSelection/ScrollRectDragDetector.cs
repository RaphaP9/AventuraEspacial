using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollRectDragDetector : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [Header("Runtime Filled")]
    [SerializeField] private bool isDragging;

    public bool IsDragging => isDragging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }
}
