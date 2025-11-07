using UnityEngine;

public class SilhouettesMinigameSFXHandler : MinigameSFXHandler
{
    protected override void OnEnable()
    {
        base.OnEnable();

        SilhouettesMinigameManager.OnFigureDragStart += SilhouettesMinigameManager_OnFigureDragStart;

        SilhouettesMinigameManager.OnSilhouetteMatch += SilhouettesMinigameManager_OnSilhouetteMatch;
        SilhouettesMinigameManager.OnSilhouetteFailed += SilhouettesMinigameManager_OnSilhouetteFailed;
        SilhouettesMinigameManager.OnFigureReturnToOriginalPosition += SilhouettesMinigameManager_OnFigureReturnToOriginalPosition;

        SilhouettesMinigameManager.OnSilhouettesRoundStart += SilhouettesMinigameManager_OnSilhouettesRoundStart;
        SilhouettesMinigameManager.OnSilhouettesRoundEnd += SilhouettesMinigameManager_OnSilhouettesRoundEnd;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        SilhouettesMinigameManager.OnFigureDragStart -= SilhouettesMinigameManager_OnFigureDragStart;

        SilhouettesMinigameManager.OnSilhouetteMatch -= SilhouettesMinigameManager_OnSilhouetteMatch;
        SilhouettesMinigameManager.OnSilhouetteFailed -= SilhouettesMinigameManager_OnSilhouetteFailed;
        SilhouettesMinigameManager.OnFigureReturnToOriginalPosition -= SilhouettesMinigameManager_OnFigureReturnToOriginalPosition;

        SilhouettesMinigameManager.OnSilhouettesRoundStart -= SilhouettesMinigameManager_OnSilhouettesRoundStart;
        SilhouettesMinigameManager.OnSilhouettesRoundEnd -= SilhouettesMinigameManager_OnSilhouettesRoundEnd;
    }

    #region Subscriptions
    private void SilhouettesMinigameManager_OnFigureDragStart(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.figureDragStart);
    }

    private void SilhouettesMinigameManager_OnSilhouetteMatch(object sender, System.EventArgs e)
    {
        //PlaySFX_Pausable(SFXPool.silhouetteMatch);
    }

    private void SilhouettesMinigameManager_OnSilhouetteFailed(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.silhouetteFail);
    }

    private void SilhouettesMinigameManager_OnFigureReturnToOriginalPosition(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.figureReturn);
    }

    private void SilhouettesMinigameManager_OnSilhouettesRoundStart(object sender, SilhouettesMinigameManager.OnSilhouettesRoundEventArgs e)
    {
        PlaySFX_Pausable(SFXPool.silhouettesRoundBegin);
    }

    private void SilhouettesMinigameManager_OnSilhouettesRoundEnd(object sender, SilhouettesMinigameManager.OnSilhouettesRoundEventArgs e)
    {
        PlaySFX_Pausable(SFXPool.silhouettesRoundCompleted);
    }
    #endregion
}
