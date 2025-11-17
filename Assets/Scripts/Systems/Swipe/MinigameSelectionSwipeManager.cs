using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameSelectionSwipeManager : SwipeManager
{
    [Header("Lists")]
    [SerializeField] private List<CanvasGroup> blockingCanvasGroups;

    protected override bool CanSwipe()
    {
        foreach(CanvasGroup canvasGroup in blockingCanvasGroups)
        {
            if(canvasGroup.blocksRaycasts == true) return false;
        }

        return true;
    }
}
