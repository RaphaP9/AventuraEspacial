using UnityEngine;

public class MemoryMinigameVibrationHandler : MinigameSceneVibrationHandler
{
    [Header("Memory Specifics")]
    [SerializeField] private HapticPreset firstCardRevealedHapticPreset;
    [SerializeField] private HapticPreset secondCardRevealedHapticPreset;
    [Space]
    [SerializeField] private HapticPreset pairMatchHapticPreset;
    [SerializeField] private HapticPreset pairFailedHapticPreset;
    [Space]
    [SerializeField] private HapticPreset memoryRoundEndHapticPreset;

    protected override void OnEnable()
    {
        base.OnEnable();
        MemoryMinigameManager.OnFirstCardRevealed += MemoryMinigameManager_OnFirstCardRevealed;
        MemoryMinigameManager.OnSecondCardRevealed += MemoryMinigameManager_OnSecondCardRevealed;

        MemoryMinigameManager.OnPairMatch += MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed += MemoryMinigameManager_OnPairFailed;

        MemoryMinigameManager.OnMemoryRoundEnd += MemoryMinigameManager_OnMemoryRoundEnd;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MemoryMinigameManager.OnFirstCardRevealed -= MemoryMinigameManager_OnFirstCardRevealed;
        MemoryMinigameManager.OnSecondCardRevealed -= MemoryMinigameManager_OnSecondCardRevealed;

        MemoryMinigameManager.OnPairMatch -= MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed -= MemoryMinigameManager_OnPairFailed;

        MemoryMinigameManager.OnMemoryRoundEnd -= MemoryMinigameManager_OnMemoryRoundEnd;
    }

    #region Subscriptions
    private void MemoryMinigameManager_OnFirstCardRevealed(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(firstCardRevealedHapticPreset);
    }

    private void MemoryMinigameManager_OnSecondCardRevealed(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(secondCardRevealedHapticPreset);
    }

    private void MemoryMinigameManager_OnPairMatch(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(pairMatchHapticPreset);
    }

    private void MemoryMinigameManager_OnPairFailed(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(pairFailedHapticPreset);
    }

    private void MemoryMinigameManager_OnMemoryRoundEnd(object sender, MemoryMinigameManager.OnMemoryRoundEventArgs e)
    {
        PlayHaptic_Unforced(memoryRoundEndHapticPreset);
    }
    #endregion
}