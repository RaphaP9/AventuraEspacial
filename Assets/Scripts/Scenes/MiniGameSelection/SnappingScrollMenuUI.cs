using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Collections;
using System;

public class SnappingScrollMenuUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform content;
    [Space]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private ScrollRectDragDetector scrollRectDragDetector;

    [Header("Settings")]
    [SerializeField, Range(1f,100f)] private float snapSpeed = 10f;
    [SerializeField] private int startIndex;

    [Header("Runtime Filled")]
    [SerializeField] private List<ItemRefferencePosition> items;
    [SerializeField] private ItemRefferencePosition targetSnapItem;
    [SerializeField] private int currentIndex;

    private bool initializationLogicPerformed = false;

    private const float STOP_SNAP_THRESHOLD = 0.1f;

    public static event EventHandler OnFirstItemReached;
    public static event EventHandler OnLastItemReached;

    public static event EventHandler OnFirstItemReachedStart;
    public static event EventHandler OnLastItemReachedStart;

    public static event EventHandler OnLastItemAway;
    public static event EventHandler OnFirstItemAway;

    [System.Serializable]
    public class ItemRefferencePosition
    {
        public RectTransform rectTransform;
        public Vector2 refferencePosition;
    }

    private void OnEnable()
    {
        SwipeManager.OnSwipeLeft += SwipeManager_OnSwipeLeft;
        SwipeManager.OnSwipeRight += SwipeManager_OnSwipeRight;
    }

    private void OnDisable()
    {
        SwipeManager.OnSwipeLeft -= SwipeManager_OnSwipeLeft;
        SwipeManager.OnSwipeRight -= SwipeManager_OnSwipeRight;
    }

    private void Start()
    {
        StartCoroutine(InitializationCoroutine());
    }

    private void Update()
    {
        HandleItemSnap();
    }

    private IEnumerator InitializationCoroutine()
    {
        yield return null; //Wait one Frame
        InitializeItems();
        InstantSnapToStartIndex();

        initializationLogicPerformed = true;
    }

    private void InitializeItems()
    {
        foreach(Transform child in content.transform)
        {
            RectTransform rectTransform = child.GetComponent<RectTransform>();
            Vector2 refferencePosition = UIUtilities.GetCanvasPosition(rectTransform, canvas) - UIUtilities.GetCanvasPosition(scrollRect.viewport, canvas);

            ItemRefferencePosition itemRefferencePosition = new ItemRefferencePosition { rectTransform = rectTransform, refferencePosition = refferencePosition};
            items.Add(itemRefferencePosition);
        }
    }

    private void InstantSnapToStartIndex()
    {
        currentIndex = startIndex;
        UpdateTargetSnapItemToIndex(currentIndex);

        content.anchoredPosition = -targetSnapItem.refferencePosition;

        if (currentIndex <= 0) OnFirstItemReachedStart?.Invoke(this, EventArgs.Empty);
        if (currentIndex >= items.Count -1) OnLastItemReachedStart?.Invoke(this, EventArgs.Empty);
    }

    private void HandleItemSnap()
    {
        if (!initializationLogicPerformed) return;
        if (scrollRectDragDetector.IsDragging) return;

        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, -targetSnapItem.refferencePosition, Time.deltaTime * snapSpeed);
    }

    public void DisplaceRightCommand()
    {
        TryIncreaseIndex();
        UpdateTargetSnapItemToIndex(currentIndex);
    }

    public void DisplaceLeftCommand()
    {
        TryDecreaseIndex();
        UpdateTargetSnapItemToIndex(currentIndex);
    }

    #region Increase Decrease Index
    private void TryIncreaseIndex()
    {
        if (currentIndex >= items.Count - 1) return; //Is in last index

        int previousIndex = currentIndex;
        currentIndex++;

        if (currentIndex >= items.Count - 1)
        {
            OnLastItemReached?.Invoke(this, EventArgs.Empty);
        }

        if(previousIndex <= 0)
        {
            OnFirstItemAway?.Invoke(this, EventArgs.Empty);
        }
    }

    private void TryDecreaseIndex()
    {
        if (currentIndex <= 0) return; //Is in first index

        int previousIndex = currentIndex;
        currentIndex--;

        if (currentIndex <= 0)
        {
            OnFirstItemReached?.Invoke(this, EventArgs.Empty);
        }

        if (previousIndex >= items.Count - 1)
        {
            OnLastItemAway?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion

    private void UpdateTargetSnapItemToIndex(int index) => targetSnapItem = items[index];

    #region Subscriptions
    private void SwipeManager_OnSwipeRight(object sender, System.EventArgs e)
    {
        DisplaceLeftCommand();
    }

    private void SwipeManager_OnSwipeLeft(object sender, System.EventArgs e)
    {
        DisplaceRightCommand();
    }
    #endregion
}

