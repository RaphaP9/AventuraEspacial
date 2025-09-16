using TMPro;
using UnityEngine;

public class MemoryMinigameComboUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI comboValueText;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";
    private const string UPDATE_TRIGGER = "Update";

    private const string MULTIPLIER_CHARACTER = "X";

    private void OnEnable()
    {
        MemoryMinigameScoreManager.OnComboGained += MemoryMinigameScoreManager_OnComboGained;
        MemoryMinigameScoreManager.OnComboUpdated += MemoryMinigameScoreManager_OnComboUpdated;
        MemoryMinigameScoreManager.OnComboLost += MemoryMinigameScoreManager_OnComboLost;
    }

    private void OnDisable()
    {
        MemoryMinigameScoreManager.OnComboGained -= MemoryMinigameScoreManager_OnComboGained;
        MemoryMinigameScoreManager.OnComboUpdated -= MemoryMinigameScoreManager_OnComboUpdated;
        MemoryMinigameScoreManager.OnComboLost -= MemoryMinigameScoreManager_OnComboLost;
    }

    private void ShowUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.ResetTrigger(UPDATE_TRIGGER);

        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(UPDATE_TRIGGER);

        animator.SetTrigger(HIDE_TRIGGER);
    }

    private void UpdateUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(HIDE_TRIGGER);

        animator.SetTrigger(UPDATE_TRIGGER);
    }

    private void SetComboValueText(int combo) => comboValueText.text = $"{MULTIPLIER_CHARACTER}{combo}";

    #region Subscriptions
    private void MemoryMinigameScoreManager_OnComboGained(object sender, MemoryMinigameScoreManager.OnComboGainedEventArgs e)
    {
        SetComboValueText(e.comboGained);
        ShowUI();
    }

    private void MemoryMinigameScoreManager_OnComboUpdated(object sender, MemoryMinigameScoreManager.OnComboGainedEventArgs e)
    {
        SetComboValueText(e.comboGained);
        UpdateUI();
    }

    private void MemoryMinigameScoreManager_OnComboLost(object sender, System.EventArgs e)
    {
        HideUI();
    }
    #endregion
}
