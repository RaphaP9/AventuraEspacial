using UnityEngine;
using System.Collections.Generic;
using System;

public class ScrollSnapIndicatorsHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform indicatorUIPrefab;
    [SerializeField] private Transform indicatorsContainer;

    [Header("Runtime Filled")]
    [SerializeField] private List<ScrollSnapSingleIndicatorUI> scrollSnapSingleIndicators;
    [SerializeField] private int currentSnapIndicatorIndex;

    private void OnEnable()
    {
        SnappingScrollMenuUI.OnItemsInitialized += SnappingScrollMenuUI_OnItemsInitialized;
        SnappingScrollMenuUI.OnItemSnap += SnappingScrollMenuUI_OnItemSnap;
    }

    private void OnDisable()
    {
        SnappingScrollMenuUI.OnItemsInitialized -= SnappingScrollMenuUI_OnItemsInitialized;
        SnappingScrollMenuUI.OnItemSnap -= SnappingScrollMenuUI_OnItemSnap;
    }

    private void CreateIndicators(int quantity)
    {
        ClearContainer();

        for(int i = 0; i < quantity; i++)
        {
            Transform indicatorUI = Instantiate(indicatorUIPrefab, indicatorsContainer);

            ScrollSnapSingleIndicatorUI scrollSnapSingleIndicatorUI = indicatorUI.GetComponent<ScrollSnapSingleIndicatorUI>();

            if (scrollSnapSingleIndicatorUI == null) continue;

            scrollSnapSingleIndicatorUI.SetLinkedIndex(i);
            scrollSnapSingleIndicators.Add(scrollSnapSingleIndicatorUI);
        }
    }

    private void ClearContainer()
    {
        for (int i = indicatorsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(indicatorsContainer.GetChild(i).gameObject);
        }
    }

    private void HandleSnapInstantlyToIndex(int index)
    {
        scrollSnapSingleIndicators[currentSnapIndicatorIndex].UnsnapImmediately();
        currentSnapIndicatorIndex = index;
        scrollSnapSingleIndicators[currentSnapIndicatorIndex].SnapImmediately();
    }

    private void HandleSnapToIndex(int index)
    {
        scrollSnapSingleIndicators[currentSnapIndicatorIndex].Unsnap();
        currentSnapIndicatorIndex = index;
        scrollSnapSingleIndicators[currentSnapIndicatorIndex].Snap();
    }

    #region Subscriptions
    private void SnappingScrollMenuUI_OnItemsInitialized(object sender, SnappingScrollMenuUI.OnItemsInitializedEventArgs e)
    {
        CreateIndicators(e.items.Count);
    }

    private void SnappingScrollMenuUI_OnItemSnap(object sender, SnappingScrollMenuUI.OnItemSnapEventArgs e)
    {
        if (e.instantly) HandleSnapInstantlyToIndex(e.itemIndex);
        else HandleSnapToIndex(e.itemIndex);
    }
    #endregion
}
