using UnityEngine;
using System;

public class MemoryMinigameTimerManager : MinigameTimerManager
{
    [Header("Components")]
    [SerializeField] private MemoryMinigameSettings settings;

    private void OnEnable()
    {
        MemoryMinigameManager.OnGameInitialized += MemoryMinigameManager_OnGameInitialized;

        MemoryMinigameManager.OnPairMatch += MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed += MemoryMinigameManager_OnPairFailed;
    }

    private void OnDisable()
    {
        MemoryMinigameManager.OnGameInitialized -= MemoryMinigameManager_OnGameInitialized;

        MemoryMinigameManager.OnPairMatch -= MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed -= MemoryMinigameManager_OnPairFailed;
    }

    protected override float GetGameTime() => settings.gameTime;
    protected override bool CanPassTime() => MemoryMinigameManager.Instance.CanPassTime();

    #region Subscriptions
    private void MemoryMinigameManager_OnGameInitialized(object sender, System.EventArgs e)
    {
        SetTotalTime(settings.gameTime);
    }

    private void MemoryMinigameManager_OnPairMatch(object sender, EventArgs e)
    {
        IncreaseTime(settings.timeBonusOnMatch);
    }

    private void MemoryMinigameManager_OnPairFailed(object sender, EventArgs e)
    {
        DecreaseTime(settings.timePenaltyOnFail);   
    }
    #endregion
}
