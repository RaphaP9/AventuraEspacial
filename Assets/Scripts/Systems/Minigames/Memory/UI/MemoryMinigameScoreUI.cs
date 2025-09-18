using TMPro;
using UnityEngine;

public class MemoryMinigameScoreUI : MinigameScoreUI
{
    private void OnEnable()
    {
        MemoryMinigameScoreManager.OnScoreInitialized += MemoryMinigameScoreManager_OnScoreInitialized;
        MemoryMinigameScoreManager.OnScoreIncreased += MemoryMinigameScoreManager_OnScoreIncreased;
    }

    private void OnDisable()
    {
        MemoryMinigameScoreManager.OnScoreInitialized -= MemoryMinigameScoreManager_OnScoreInitialized;
        MemoryMinigameScoreManager.OnScoreIncreased -= MemoryMinigameScoreManager_OnScoreIncreased;
    }

    #region Subcriptions
    private void MemoryMinigameScoreManager_OnScoreInitialized(object sender, MemoryMinigameScoreManager.OnScoreInitializedEventArgs e)
    {
        SetScoreText(e.currentScore);
    }

    private void MemoryMinigameScoreManager_OnScoreIncreased(object sender, MemoryMinigameScoreManager.OnScoreIncreasedEventArgs e)
    {
        SetScoreText(e.currentScore);
    }
    #endregion
}
