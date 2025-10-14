using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;
using UnityEngine.Localization;

public class FinalScoreUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MinigameScoreManager minigameScoreManager;
    [SerializeField] private MinigameFinalScoreSettingsSO minigameFinalScoreSettingsSO;
    [Space]
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI relatedMessageText;
    [SerializeField] private Image relatedImage;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private const string HIDE_TRIGGER = "Hide";
    private const string SHOW_TRIGGER = "Show";

    private MinigameFinalScoreSetting setting;
    private bool hasBeenSet = false;
    private void OnEnable()
    {
        MinigameManager.OnGameWinning += MinigameManager_OnGameWinning;
        MinigameManager.OnGameLosing += MinigameManager_OnGameLosing;

        MinigameManager.OnGameWon += MinigameManager_OnGameWon;
        MinigameManager.OnGameLost += MinigameManager_OnGameLost;

        LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
    }

    private void OnDisable()
    {
        MinigameManager.OnGameWinning -= MinigameManager_OnGameWinning;
        MinigameManager.OnGameLosing -= MinigameManager_OnGameLosing;

        MinigameManager.OnGameWon -= MinigameManager_OnGameWon;
        MinigameManager.OnGameLost -= MinigameManager_OnGameLost;

        LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
    }

    #region Animations
    public void ShowUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }
    #endregion

    #region Setters
    private void SetUIByScore(int score)
    {
        setting = GetMinigameFinalScoreSettingByScore(score);
        hasBeenSet = true;

        SetScoreText(score);
        SetUIByStoredSetting();
    }

    private void SetUIByStoredSetting()
    {
        if (!hasBeenSet) return;

        SetRelatedMessageTextBySetting(setting);
        SetRelatedImageSpriteBySetting(setting);
    }

    private void SetScoreText(int score) => scoreText.text = score.ToString();

    private void SetRelatedMessageTextBySetting(MinigameFinalScoreSetting setting)
    {
        relatedMessageText.text = LocalizationSettings.StringDatabase.GetLocalizedString(minigameFinalScoreSettingsSO.stringLocalizationTable, setting.messageLocalizationBinding);
    }

    private void SetRelatedImageSpriteBySetting(MinigameFinalScoreSetting setting)
    {
        relatedImage.sprite = setting.sprite;
    }
    #endregion

    #region Utility Methods

    private MinigameFinalScoreSetting GetMinigameFinalScoreSettingByScore(int score)
    {
        foreach(MinigameFinalScoreSetting scoreSetting in minigameFinalScoreSettingsSO.minigameFinalScoreSettingsList)
        {
            if(score >= scoreSetting.minimunScore) return scoreSetting;
        }

        if (debug) Debug.Log($"No score setting matches the minimum score. Score obtained: {score}. Returning last list element");
        return minigameFinalScoreSettingsSO.minigameFinalScoreSettingsList[^1];
    }
    #endregion

    #region Subscriptions

    private void MinigameManager_OnGameWinning(object sender, EventArgs e)
    {
        SetUIByScore(minigameScoreManager.CurrentScore);
    }

    private void MinigameManager_OnGameLosing(object sender, EventArgs e)
    {
        SetUIByScore(minigameScoreManager.CurrentScore);
    }

    private void MinigameManager_OnGameWon(object sender, System.EventArgs e)
    {
        ShowUI();
    }

    private void MinigameManager_OnGameLost(object sender, System.EventArgs e)
    {
        ShowUI();
    }

    private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
    {
        SetUIByStoredSetting();
    }

    #endregion
}
