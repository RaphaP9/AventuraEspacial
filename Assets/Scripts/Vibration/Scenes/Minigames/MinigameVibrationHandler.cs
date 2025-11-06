using UnityEngine;
using System.Collections.Generic;

public class MinigameVibrationHandler : SceneVibrationHandler
{
    [Header("Hits")]
    [SerializeField] private HapticPreset regularHitHapticPreset;
    [SerializeField] private HapticPreset maxComboHitHapticPreset;
    [Space]
    [SerializeField] private List<ComboValueHapticRelationship> comboValueHapticRelationships;

    [Header("Minigame End")]
    [SerializeField] private HapticPreset minigameWonHapticPreset;
    [SerializeField] private HapticPreset minigameLostHapticPreset;

    [Header("Progress")]
    [SerializeField] private HapticPreset progressChangedHapticPreset;

    [Header("Time")]
    [SerializeField] private HapticPreset timeWarningHapticPreset;

    [System.Serializable]
    public class ComboValueHapticRelationship
    {
        [Range(2, 10)] public int comboValue;
        public HapticPreset hapticPreset;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        MinigameScoreManager.OnRegularHit += MinigameScoreManager_OnRegularHit;
        MinigameScoreManager.OnComboGained += MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated += MinigameScoreManager_OnComboUpdated;
        MinigameScoreManager.OnMaxComboHit += MinigameScoreManager_OnMaxComboHit;

        MinigameManager.OnGameWon += MinigameManager_OnGameWon;
        MinigameManager.OnGameLost += MinigameManager_OnGameLost;

        MinigameManager.OnRoundEnd += MinigameManager_OnRoundEnd;

        MinigameTimerManager.OnTimeWarning += MinigameTimerManager_OnTimeWarning;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MinigameScoreManager.OnRegularHit -= MinigameScoreManager_OnRegularHit;
        MinigameScoreManager.OnComboGained -= MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated -= MinigameScoreManager_OnComboUpdated;
        MinigameScoreManager.OnMaxComboHit -= MinigameScoreManager_OnMaxComboHit;

        MinigameManager.OnGameWon -= MinigameManager_OnGameWon;
        MinigameManager.OnGameLost -= MinigameManager_OnGameLost;

        MinigameManager.OnRoundEnd -= MinigameManager_OnRoundEnd;

        MinigameTimerManager.OnTimeWarning -= MinigameTimerManager_OnTimeWarning;
    }

    private void PlayComboHaptic(int combo)
    {
        foreach(ComboValueHapticRelationship relationship in comboValueHapticRelationships)
        {
            if(combo == relationship.comboValue)
            {
                PlayHaptic_Unforced(relationship.hapticPreset);
                return;
            }
        }
    }

    #region Subscriptions

    private void MinigameScoreManager_OnRegularHit(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(regularHitHapticPreset);
    }

    private void MinigameScoreManager_OnComboUpdated(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        PlayComboHaptic(e.comboGained);
    }

    private void MinigameScoreManager_OnComboGained(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        PlayComboHaptic(e.comboGained);
    }

    private void MinigameScoreManager_OnMaxComboHit(object sender, System.EventArgs e)
    {
        PlayHaptic_Unforced(maxComboHitHapticPreset);
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
