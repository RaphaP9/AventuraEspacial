using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
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

    [Header("Lists")]
    [SerializeField] private List<SnappingScrollMenuItemUI> items;

    [Header("Settings")]
    [SerializeField, Range(1f,100f)] private float snapSpeed = 10f;
    [SerializeField] private int startIndex;

    [Header("Runtime Filled")]
    [SerializeField] private SnappingScrollMenuItemUI targetSnapItem;
    [SerializeField] private int currentIndex;

    private bool initializationLogicPerformed = false;

    #region Events
    public static event EventHandler<OnItemsInitializedEventArgs> OnItemsInitialized;

    public static event EventHandler OnFirstItemReached;
    public static event EventHandler OnLastItemReached;

    public static event EventHandler OnFirstItemReachedStart;
    public static event EventHandler OnLastItemReachedStart;

    public static event EventHandler OnLastItemAway;
    public static event EventHandler OnFirstItemAway;

    public static event EventHandler<OnItemSnapEventArgs> OnItemSnap;
    #endregion

    #region Custom Classes
    [System.Serializable]
    public class OnItemsInitializedEventArgs : EventArgs
    {
        public List<SnappingScrollMenuItemUI> items;
    }

    public class OnItemSnapEventArgs : EventArgs
    {
        public int itemIndex;
        public bool instantly;
    }
    #endregion

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
        //NOTE: This asumes previous scene was same resolution as this scene, so Canvas Layout will not take time to rebuild

        //Wait one frames
        yield return null;

        InitializeItems();
        InstantSnapToStartIndex();

        initializationLogicPerformed = true;
    }

    private void InitializeItems()
    {
        int index = 0;

        foreach(SnappingScrollMenuItemUI item in items)
        {
            Vector2 refferencePosition = UIUtilities.GetCanvasPosition(item.RectTransform, canvas) - UIUtilities.GetCanvasPosition(scrollRect.viewport, canvas);

            item.SetAssignedIndex(index);
            item.SetRefferencePosition(refferencePosition);

            index++;
        }

        OnItemsInitialized?.Invoke(this, new OnItemsInitializedEventArgs { items = items });
    }

    private void InstantSnapToStartIndex()
    {
        currentIndex = startIndex;
        UpdateTargetSnapItemToIndex(currentIndex);

        content.anchoredPosition = -targetSnapItem.RefferencePosition;

        if (currentIndex <= 0) OnFirstItemReachedStart?.Invoke(this, EventArgs.Empty);
        if (currentIndex >= items.Count -1) OnLastItemReachedStart?.Invoke(this, EventArgs.Empty);

        OnItemSnap?.Invoke(this, new OnItemSnapEventArgs { itemIndex = currentIndex, instantly = true });
    }

    private void HandleItemSnap()
    {
        if (!initializationLogicPerformed) return;
        if (scrollRectDragDetector.IsDragging) return;

        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, -targetSnapItem.RefferencePosition, Time.deltaTime * snapSpeed);
    }

    #region Displacement Commands
    public void DisplaceRightCommand()
    {
        TryIncreaseIndex();
        UpdateTargetSnapItemToIndex(currentIndex);

        OnItemSnap?.Invoke(this, new OnItemSnapEventArgs { itemIndex = currentIndex, instantly = false});
    }

    public void DisplaceLeftCommand()
    {
        TryDecreaseIndex();
        UpdateTargetSnapItemToIndex(currentIndex);

        OnItemSnap?.Invoke(this, new OnItemSnapEventArgs { itemIndex = currentIndex, instantly = false });
    }
    #endregion

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

    private void UpdateTargetSnapItemToIndex(int index)
    {
        foreach(SnappingScrollMenuItemUI item in items)
        {
            if (item.AssignedIndex != index) continue; //If not the same index, continue
            if (targetSnapItem == item) continue; //If already snapped, continue

            item.TriggerSnapEvents();
            targetSnapItem = item;
        }

        //targetSnapItem = items[index]; //Also can be
    }

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

