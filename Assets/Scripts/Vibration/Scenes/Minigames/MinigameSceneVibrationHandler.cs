using UnityEngine;

public class MinigameSceneVibrationHandler : SceneVibrationHandler
{
    [Header("Combo")]
    [SerializeField] private HapticPreset comboX2HapticPreset;
    [SerializeField] private HapticPreset comboX3HapticPreset;
    [SerializeField] private HapticPreset comboX4HapticPreset;
    [SerializeField] private HapticPreset comboX5HapticPreset;

    [Header("Minigame End")]
    [SerializeField] private HapticPreset minigameWonHapticPreset;
    [SerializeField] private HapticPreset minigameLostHapticPreset;

    [Header("Progress")]
    [SerializeField] private HapticPreset progressChangedHapticPreset;

    [Header("Time")]
    [SerializeField] private HapticPreset timeWarningHapticPreset;

    protected override void OnEnable()
    {
        base.OnEnable();

        MinigameScoreManager.OnComboGained += MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated += MinigameScoreManager_OnComboUpdated;

        MinigameManager.OnGameWon += MinigameManager_OnGameWon;
        MinigameManager.OnGameLost += MinigameManager_OnGameLost;

        MinigameManager.OnRoundEnd += MinigameManager_OnRoundEnd;

        MinigameTimerManager.OnTimeWarning += MinigameTimerManager_OnTimeWarning;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MinigameScoreManager.OnComboGained -= MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated -= MinigameScoreManager_OnComboUpdated;

        MinigameManager.OnGameWon -= MinigameManager_OnGameWon;
        MinigameManager.OnGameLost -= MinigameManager_OnGameLost;

        MinigameManager.OnRoundEnd -= MinigameManager_OnRoundEnd;

        MinigameTimerManager.OnTimeWarning -= MinigameTimerManager_OnTimeWarning;
    }

    private void PlayComboHaptic(int combo)
    {
        switch (combo)
        {
            case 2:
                PlayHaptic_Unforced(comboX2HapticPreset);
                break;
            case 3:
                PlayHaptic_Unforced(comboX3HapticPreset);
                break;
            case 4:
                PlayHaptic_Unforced(comboX4HapticPreset);
                break;
            case 5:
                PlayHaptic_Unforced(comboX5HapticPreset);
                break;
            default:
                break;
        }
    }

    #region Subscriptions
    private void MinigameScoreManager_OnComboUpdated(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        PlayComboHaptic(e.comboGained);
    }

    private void MinigameScoreManager_OnComboGained(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        PlayComboHaptic(e.comboGained);
    }

    private void MinigameManager_OnGameWon(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(minigameWonHapticPreset);
    }

    private void MinigameManager_OnGameLost(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(minigameLostHapticPreset);
    }

    private void MinigameManager_OnRoundEnd(object sender, MinigameManager.OnRoundEventArgs e)
    {
        PlayHaptic_Unforced(progressChangedHapticPreset);
    }

    private void MinigameTimerManager_OnTimeWarning(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(timeWarningHapticPreset);
    }
    #endregion
}
