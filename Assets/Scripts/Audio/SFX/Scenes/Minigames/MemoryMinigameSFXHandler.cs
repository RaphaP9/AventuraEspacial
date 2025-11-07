using UnityEngine;

public class MemoryMinigameSFXHandler : MinigameSFXHandler
{
    protected override void OnEnable()
    {
        base.OnEnable();
        MemoryMinigameManager.OnFirstCardRevealed += MemoryMinigameManager_OnFirstCardRevealed;
        MemoryMinigameManager.OnSecondCardRevealed += MemoryMinigameManager_OnSecondCardRevealed;

        MemoryMinigameManager.OnPairMatch += MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed += MemoryMinigameManager_OnPairFailed;

        MemoryMinigameManager.OnMemoryRoundStart += MemoryMinigameManager_OnMemoryRoundStart;
        MemoryMinigameManager.OnMemoryRoundEnd += MemoryMinigameManager_OnMemoryRoundEnd;

        MemoryMinigameManager.OnRevealTimeEnd += MemoryMinigameManager_OnRevealTimeEnd;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MemoryMinigameManager.OnFirstCardRevealed -= MemoryMinigameManager_OnFirstCardRevealed;
        MemoryMinigameManager.OnSecondCardRevealed -= MemoryMinigameManager_OnSecondCardRevealed;

        MemoryMinigameManager.OnPairMatch -= MemoryMinigameManager_OnPairMatch;
        MemoryMinigameManager.OnPairFailed -= MemoryMinigameManager_OnPairFailed;

        MemoryMinigameManager.OnMemoryRoundStart -= MemoryMinigameManager_OnMemoryRoundStart;
        MemoryMinigameManager.OnMemoryRoundEnd -= MemoryMinigameManager_OnMemoryRoundEnd;

        MemoryMinigameManager.OnRevealTimeEnd -= MemoryMinigameManager_OnRevealTimeEnd;
    }

    #region Subscriptions
    private void MemoryMinigameManager_OnFirstCardRevealed(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.firstCardSelected);
    }

    private void MemoryMinigameManager_OnSecondCardRevealed(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.secondCardSelected);
    }

    private void MemoryMinigameManager_OnPairMatch(object sender, System.EventArgs e)
    {
        //PlaySFX_Pausable(SFXPool.pairMatch);
    }

    private void MemoryMinigameManager_OnPairFailed(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.pairFail);
    }

    private void MemoryMinigameManager_OnMemoryRoundStart(object sender, MemoryMinigameManager.OnMemoryRoundEventArgs e)
    {
        PlaySFX_Pausable(SFXPool.memoryRoundBegin);
    }

    private void MemoryMinigameManager_OnMemoryRoundEnd(object sender, MemoryMinigameManager.OnMemoryRoundEventArgs e)
    {
        PlaySFX_Pausable(SFXPool.memoryRoundCompleted);
    }

    private void MemoryMinigameManager_OnRevealTimeEnd(object sender, MemoryMinigameManager.OnRevealTimeEventArgs e)
    {
        PlaySFX_Pausable(SFXPool.cardsRevealTimeEnd);
    }

    #endregion
}
