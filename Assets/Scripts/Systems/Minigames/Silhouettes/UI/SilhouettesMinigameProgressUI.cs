using System;
using UnityEngine;

public class SilhouettesMinigameProgressUI : MinigameProgressUI
{
    private void OnEnable()
    {
        SilhouettesMinigameManager.OnGameInitialized += SilhouettesMinigameManager_OnGameInitialized;

        SilhouettesMinigameManager.OnRoundStart += SilhouettesMinigameManager_OnRoundStart;
        SilhouettesMinigameManager.OnRoundEnd += SilhouettesMinigameManager_OnRoundEnd;
    }

    private void OnDisable()
    {
        SilhouettesMinigameManager.OnGameInitialized -= SilhouettesMinigameManager_OnGameInitialized;

        SilhouettesMinigameManager.OnRoundStart -= SilhouettesMinigameManager_OnRoundStart;
        SilhouettesMinigameManager.OnRoundEnd -= SilhouettesMinigameManager_OnRoundEnd;
    }

    #region Subscriptions
    private void SilhouettesMinigameManager_OnGameInitialized(object sender, EventArgs e)
    {
        SetProgressInstantly(0f);
    }

    private void SilhouettesMinigameManager_OnRoundStart(object sender, SilhouettesMinigameManager.OnRoundEventArgs e)
    {
        SetTargetValue(e.roundIndex, e.totalRounds, false);
    }

    private void SilhouettesMinigameManager_OnRoundEnd(object sender, SilhouettesMinigameManager.OnRoundEventArgs e)
    {
        SetTargetValue(e.roundIndex, e.totalRounds, true);
    }
    #endregion
}
