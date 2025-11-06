using UnityEngine;

public class SilhouettesMinigameVibrationHandler : MinigameVibrationHandler
{
    [Header("Memory Specifics")]
    [SerializeField] private HapticPreset figureDragStartHapticPreset;
    [SerializeField] private HapticPreset figureDragEndHapticPreset;
    [Space]
    [SerializeField] private HapticPreset silhouetteMatchHapticPreset;
    [SerializeField] private HapticPreset silhouetteFailedHapticPreset;
    [SerializeField] private HapticPreset figureReturnHapticPreset;
    [Space]
    [SerializeField] private HapticPreset silhouettesRoundEndHapticPreset;

    protected override void OnEnable()
    {
        base.OnEnable();

        SilhouettesMinigameManager.OnFigureDragStart += SilhouettesMinigameManager_OnFigureDragStart;
        SilhouettesMinigameManager.OnFigureDragEnd += SilhouettesMinigameManager_OnFigureDragEnd;

        SilhouettesMinigameManager.OnSilhouetteMatch += SilhouettesMinigameManager_OnSilhouetteMatch;
        SilhouettesMinigameManager.OnSilhouetteFailed += SilhouettesMinigameManager_OnSilhouetteFailed;
        SilhouettesMinigameManager.OnFigureReturnToOriginalPosition += SilhouettesMinigameManager_OnFigureReturnToOriginalPosition;

        SilhouettesMinigameManager.OnSilhouettesRoundEnd += SilhouettesMinigameManager_OnSilhouettesRoundEnd;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        SilhouettesMinigameManager.OnFigureDragStart -= SilhouettesMinigameManager_OnFigureDragStart;
        SilhouettesMinigameManager.OnFigureDragEnd -= SilhouettesMinigameManager_OnFigureDragEnd;

        SilhouettesMinigameManager.OnSilhouetteMatch -= SilhouettesMinigameManager_OnSilhouetteMatch;
        SilhouettesMinigameManager.OnSilhouetteFailed -= SilhouettesMinigameManager_OnSilhouetteFailed;
        SilhouettesMinigameManager.OnFigureReturnToOriginalPosition -= SilhouettesMinigameManager_OnFigureReturnToOriginalPosition;

        SilhouettesMinigameManager.OnSilhouettesRoundEnd -= SilhouettesMinigameManager_OnSilhouettesRoundEnd;
    }

    #region Subscriptions
    private void SilhouettesMinigameManager_OnFigureDragStart(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(figureDragStartHapticPreset);
    }

    private void SilhouettesMinigameManager_OnFigureDragEnd(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(figureDragEndHapticPreset);
    }

    private void SilhouettesMinigameManager_OnSilhouetteMatch(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(silhouetteMatchHapticPreset);
    }

    private void SilhouettesMinigameManager_OnSilhouetteFailed(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(silhouetteFailedHapticPreset);
    }

    private void SilhouettesMinigameManager_OnFigureReturnToOriginalPosition(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(figureReturnHapticPreset);
    }

    private void SilhouettesMinigameManager_OnSilhouettesRoundEnd(object sender, SilhouettesMinigameManager.OnSilhouettesRoundEventArgs e)
    {
        PlayHaptic_Unforced(silhouettesRoundEndHapticPreset);
    }
    #endregion
}