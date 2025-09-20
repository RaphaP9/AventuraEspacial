using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FigureHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Components")]
    [SerializeField] private Image figureImage;

    [Header("Components")]
    [SerializeField] private FigureAnimationController animatorController;
    [SerializeField] private RectTransform transformToDrag;

    [Header("Movement Settings")]
    [SerializeField, Range(0.5f,3f)] private float moveToOriginTime;
    [SerializeField, Range(0.5f, 3f)] private float moveToBackpackTime;
    [SerializeField] private AnimationCurve moveToOriginCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private AnimationCurve moveToBackpackCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Runtime Filled")]
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private SilhouetteSO silhouetteSO;
    [SerializeField] private bool isDragging;
    [SerializeField] private bool isMatched;

    public static event EventHandler<OnFigureEventArgs> OnFigureDragStart;
    public static event EventHandler<OnFigureEventArgs> OnFigureDragEnd;

    public class OnFigureEventArgs : EventArgs
    {
        public FigureHandler figureHandler;
    }

    public void SetSilhouhette(SilhouetteSO silhouetteSO)
    {
        this.silhouetteSO = silhouetteSO;

        isDragging = false;
        isMatched = false;

        SetFigureImage(silhouetteSO.sprite);
        StoreOriginalPosition();
    }

    private void SetFigureImage(Sprite sprite) => figureImage.sprite = sprite;
    private void StoreOriginalPosition() => originalPosition = transformToDrag.anchoredPosition;

    private void FollowPointerPosition(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        transformToDrag.anchoredPosition = localPoint;
    }

    private IEnumerator AnimateMovement(Transform targetTransform, Vector3 startPos, Vector3 endPos, float duration, AnimationCurve animationCurve)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float normalizedTime = elapsedTime / duration;
            float curveValue = animationCurve.Evaluate(normalizedTime);

            targetTransform.position = Vector3.Lerp(startPos, endPos, curveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetTransform.position = endPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragging) return;
        if (isMatched) return;
        if (!SilhouettesMinigameManager.Instance.CanDragSilhouette()) return;

        isDragging = true;
        OnFigureDragStart?.Invoke(this, new OnFigureEventArgs { figureHandler = this });
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        FollowPointerPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        OnFigureDragEnd?.Invoke(this, new OnFigureEventArgs { figureHandler = this });
    }
}
