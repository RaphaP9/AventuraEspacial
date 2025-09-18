using System;
using UnityEngine;
using UnityEngine.UI;

public class MemoryMinigameProgressUI : MinigameProgressUI
{
    private void OnEnable()
    {
        MemoryMinigameManager.OnGameInitialized += MemoryMinigameManager_OnGameInitialized;

        MemoryMinigameManager.OnRoundStart += MemoryMinigameManager_OnRoundStart;
        MemoryMinigameManager.OnRoundEnd += MemoryMinigameManager_OnRoundEnd;
    }

    private void OnDisable()
    {
        MemoryMinigameManager.OnGameInitialized -= MemoryMinigameManager_OnGameInitialized;

        MemoryMinigameManager.OnRoundStart -= MemoryMinigameManager_OnRoundStart;
        MemoryMinigameManager.OnRoundEnd -= MemoryMinigameManager_OnRoundEnd;
    }

    #region Subscriptions
    private void MemoryMinigameManager_OnGameInitialized(object sender, EventArgs e)
    {
        SetProgressInstantly(0f);
    }

    private void MemoryMinigameManager_OnRoundStart(object sender, MemoryMinigameManager.OnRoundEventArgs e)
    {
        SetTargetValue(e.roundIndex, e.totalRounds, false);
    }

    private void MemoryMinigameManager_OnRoundEnd(object sender, MemoryMinigameManager.OnRoundEventArgs e)
    {
        SetTargetValue(e.roundIndex, e.totalRounds, true);
    }
    #endregion
}
