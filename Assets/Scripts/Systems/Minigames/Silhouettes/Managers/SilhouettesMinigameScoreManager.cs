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

    protected override int GetBaseScorePerHit() => settings.baseScorePerSilhouetteMatch;
    protected override int GetBonusScorePerCombo() => settings.bonusScorePerCombo;
    protected override int GetMinCombo() => settings.minCombo;
    protected override int GetMaxCombo() => settings.maxCombo;

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
