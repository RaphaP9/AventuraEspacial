using UnityEngine;

public class MinigameSFXHandler : SceneSFXHandler
{
    private const int COMBO_X2_VALUE = 2;
    private const int COMBO_X3_VALUE = 3;
    private const int COMBO_X4_VALUE = 4;
    private const int COMBO_X5_VALUE = 5;

    protected override void OnEnable()
    {
        base.OnEnable();

        MinigameScoreManager.OnRegularHit += MinigameScoreManager_OnRegularHit;
        MinigameScoreManager.OnComboGained += MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated += MinigameScoreManager_OnComboUpdated;
        MinigameScoreManager.OnMaxComboHit += MinigameScoreManager_OnMaxComboHit;

        MinigameManager.OnGameWon += MinigameManager_OnGameWon;
        MinigameManager.OnGameLost += MinigameManager_OnGameLost;

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

        MinigameTimerManager.OnTimeWarning -= MinigameTimerManager_OnTimeWarning;
    }

    private void PlayComboSFX(int combo)
    {
        switch (combo)
        {
            case COMBO_X2_VALUE:
                PlaySFX_Pausable(SFXPool.comboX2);
                break;
            case COMBO_X3_VALUE:
                PlaySFX_Pausable(SFXPool.comboX3);
                break;
            case COMBO_X4_VALUE:
                PlaySFX_Pausable(SFXPool.comboX4);
                break;
            case COMBO_X5_VALUE:
                PlaySFX_Pausable(SFXPool.comboX5);
                break;
            default:
                break;
        }
    }

    #region Subscriptions

    private void MinigameScoreManager_OnRegularHit(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.regularHit);
    }

    private void MinigameScoreManager_OnComboUpdated(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        PlayComboSFX(e.comboGained);
    }

    private void MinigameScoreManager_OnComboGained(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        PlayComboSFX(e.comboGained);
    }

    private void MinigameScoreManager_OnMaxComboHit(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.maxComboHit);
    }

    private void MinigameManager_OnGameWon(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.winMinigame);
    }

    private void MinigameManager_OnGameLost(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.loseMinigame);
    }

    private void MinigameTimerManager_OnTimeWarning(object sender, System.EventArgs e)
    {
        PlaySFX_Pausable(SFXPool.timeWarning);
    }
    #endregion
}