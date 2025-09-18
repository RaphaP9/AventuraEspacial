using UnityEngine;
using System;

public class MemoryMinigameScoreManager : MinigameScoreManager
{
    [Header("Components")]
    [SerializeField] private MemoryMinigameSettings settings;

    private void OnEnable()
    {
        MemoryMinigameManager.OnPairMatch += MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed += MemoryMinigameManager_OnPairFailed;
    }

    private void OnDisable()
    {
        MemoryMinigameManager.OnPairMatch -= MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed -= MemoryMinigameManager_OnPairFailed;
    }

    protected override int GetBaseScorePerHit() => settings.baseScorePerPairMatch;
    protected override int GetBonusScorePerCombo() => settings.bonusScorePerCombo;
    protected override int GetMaxCombo() => settings.maxCombo;

    #region Subscriptions
    private void MemoryMinigameManager_OnPairMatch(object sender, System.EventArgs e)
    {
        ProcessHit();
    }

    private void MemoryMinigameManager_OnPairFailed(object sender, System.EventArgs e)
    {
        ProcessFail();
    }
    #endregion
}
