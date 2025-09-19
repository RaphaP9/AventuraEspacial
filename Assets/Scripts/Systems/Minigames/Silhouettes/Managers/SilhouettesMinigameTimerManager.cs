using System;
using UnityEngine;

public class SilhouettesMinigameTimerManager : MinigameTimerManager
{
    [Header("Components")]
    [SerializeField] private SilhouettesMinigameSettings settings;

    private void OnEnable()
    {
        SilhouettesMinigameManager.OnGameInitialized += SilhouettesMinigameManager_OnGameInitialized;

        SilhouettesMinigameManager.OnSilhouetteMatch += SilhouettesMinigameManager_OnSilhouetteMatch;
        SilhouettesMinigameManager.OnSilhouetteFailed += SilhouettesMinigameManager_OnSilhouetteFailed;
    }

    private void OnDisable()
    {
        SilhouettesMinigameManager.OnGameInitialized -= SilhouettesMinigameManager_OnGameInitialized;

        SilhouettesMinigameManager.OnSilhouetteMatch -= SilhouettesMinigameManager_OnSilhouetteMatch;
        SilhouettesMinigameManager.OnSilhouetteFailed -= SilhouettesMinigameManager_OnSilhouetteFailed;
    }

    protected override float GetGameTime() => settings.gameTime;
    protected override bool CanPassTime() => SilhouettesMinigameManager.Instance.CanPassTime();

    #region Subscriptions
    private void SilhouettesMinigameManager_OnGameInitialized(object sender, EventArgs e)
    {
        SetTotalTime(settings.gameTime);
    }

    private void SilhouettesMinigameManager_OnSilhouetteMatch(object sender, EventArgs e)
    {
        IncreaseTime(settings.timeBonusOnMatch);
    }

    private void SilhouettesMinigameManager_OnSilhouetteFailed(object sender, EventArgs e)
    {
        DecreaseTime(settings.timePenaltyOnFail);
    }
    #endregion
}
