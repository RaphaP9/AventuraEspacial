using UnityEngine;

public class SilhouettesMinigameScoreManager : MinigameScoreManager
{
    [Header("Components")]
    [SerializeField] private SilhouettesMinigameSettings settings;

    private void OnEnable()
    {
        SilhouettesMinigameManager.OnSilhouetteMatch += SilhouettesMinigameManager_OnSilhouetteMatch;
        SilhouettesMinigameManager.OnSilhouetteFailed += SilhouettesMinigameManager_OnSilhouetteFailed;
    }

    private void OnDisable()
    {
        SilhouettesMinigameManager.OnSilhouetteMatch -= SilhouettesMinigameManager_OnSilhouetteMatch;
        SilhouettesMinigameManager.OnSilhouetteFailed -= SilhouettesMinigameManager_OnSilhouetteFailed;
    }

    protected override int GetBaseScorePerHit() => 0;
    protected override int GetBonusScorePerCombo() => 0;
    protected override int GetMaxCombo() => 0;

    #region Subscriptions
    private void SilhouettesMinigameManager_OnSilhouetteMatch(object sender, System.EventArgs e)
    {
        ProcessHit();
    }

    private void SilhouettesMinigameManager_OnSilhouetteFailed(object sender, System.EventArgs e)
    {
        ProcessFail();
    }
    #endregion
}
