using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;

public class FigureHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("UI Components")]
    [SerializeField] private Image figureImage;

    [Header("Components")]
    [SerializeField] private FigureAnimationController animatorController;
    [SerializeField] private RectTransform transformToDrag;

    [Header("Movement Settings")]
    [SerializeField,Range(1f,100f)] private float smoothMovementFactor;
    [SerializeField, Range(0.5f,3f)] private float moveToOriginalPositionTime;
    [SerializeField] private AnimationCurve moveToOriginalPositionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [Space]
    [SerializeField, Range(0.5f, 3f)] private float moveToBackpackTime;
    [SerializeField] private AnimationCurve moveToBackpackCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Runtime Filled")]
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private SilhouetteSO silhouetteSO;

    [SerializeField] private bool isMoving;
    [SerializeField] private bool isDragging;
    [SerializeField] private bool isMatched;
    [SerializeField] private bool isHolding;

    public SilhouetteSO SilhouetteSO => silhouetteSO;

    public static event EventHandler<OnFigureEventArgs> OnFigureDragStart;
    public static event EventHandler<OnFigureEventArgs> OnFigureDragEnd;

    private Canvas canvas;

    public class OnFigureEventArgs : EventArgs
    {
        public FigureHandler figureHandler;
    }

    private void Update()
    {
        HandleDrag();
    }

    #region Setters

    public void SetSilhouhette(SilhouetteSO silhouetteSO)
    {
        this.silhouetteSO = silhouetteSO;

        isDragging = false;
        isMatched = false;

        SetFigureImage(silhouetteSO.sprite);

        StoreCanvas();
        StoreOriginalPosition();
    }

    private void SetFigureImage(Sprite sprite) => figureImage.sprite = sprite;
    private void StoreOriginalPosition() => originalPosition = transformToDrag.anchoredPosition;
    private void StoreCanvas() => canvas = gameObject.GetComponentInParent<Canvas>();
    #endregion

    #region Follow and Lerping
    private void FollowPointerPosition()
    {
        Vector2 localPoint = GetMouseLocalPoint();
        transformToDrag.anchoredPosition = localPoint;
    }

    private void LerpToPointerPosition()
    {
        Vector2 localPoint = GetMouseLocalPoint();

        transformToDrag.anchoredPosition = Vector2.Lerp(transformToDrag.anchoredPosition, localPoint,smoothMovementFactor * Time.deltaTime);
    }

    private Vector2 GetMouseLocalPoint()
    {
        RectTransform parentRect = transformToDrag.parent as RectTransform;
        Vector2 screenPos = InputUtilities.GetMousePosition();
        Camera cam = null;

        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay) cam = canvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenPos, cam, out Vector2 localPoint);
        return localPoint;
    }

    private IEnumerator AnimateMovement(RectTransform targetRect, Vector2 startPos, Vector2 endPos, float duration, AnimationCurve animationCurve)
    {
        isMoving = true;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float normalizedTime = elapsedTime / duration;
            float curveValue = animationCurve.Evaluate(normalizedTime);

            targetRect.anchoredPosition = Vector2.Lerp(startPos, endPos, curveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetRect.anchoredPosition = endPos;

        isMoving = false;
    }
    #endregion

    #region Pointer Methods
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging) return;
        if (isHolding) return;
        if (isMatched) return;
        if (!SilhouettesMinigameManager.Instance.CanDragSilhouette()) return;

        StopAllCoroutines();
        isHolding = true;
        OnFigureDragStart?.Invoke(this, new OnFigureEventArgs { figureHandler = this });

        StoreCanvas(); //Remove Later
        StoreOriginalPosition(); //Remove Later
    }

    public void HandleDrag()
    {
        if(!isHolding) return;

        isDragging = true;
        LerpToPointerPosition();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        isDragging = false;

        OnFigureDragEnd?.Invoke(this, new OnFigureEventArgs { figureHandler = this });
        ReturnToOriginalPosition();
    }
    #endregion

    #region Public Methods
    public void ReturnToOriginalPosition()
    {
        StartCoroutine(AnimateMovement(transformToDrag, transformToDrag.anchoredPosition, originalPosition, moveToOriginalPositionTime, moveToOriginalPositionCurve));
    }
    #endregion
}
