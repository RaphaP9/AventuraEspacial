using TMPro;
using UnityEngine;

public class MinigameComboUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI comboValueText;
    [SerializeField] private RectTransform comboScaleHolder;

    [Header("Settings")]
    [SerializeField, Range(2,10)] private int minComboValue;
    [SerializeField, Range(2, 10)] private int maxComboValue;
    [Space]
    [SerializeField, Range(0.5f,1.5f)] private float minComboScale;
    [SerializeField, Range(0.5f, 1.5f)] private float maxComboScale;
    [Space]
    [SerializeField, Range(1f, 10f)] private float scaleSmoothFactor;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";
    private const string UPDATE_TRIGGER = "Update";

    private const string MULTIPLIER_CHARACTER = "X";

    private const float SCALE_THRESHOLD = 0.01f;

    private float targetScale = 1f;

    private void OnEnable()
    {
        MinigameScoreManager.OnComboGained += MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated += MinigameScoreManager_OnComboUpdated;
        MinigameScoreManager.OnComboLost += MinigameScoreManager_OnComboLost;
    }

    private void OnDisable()
    {
        MinigameScoreManager.OnComboGained -= MinigameScoreManager_OnComboGained;
        MinigameScoreManager.OnComboUpdated -= MinigameScoreManager_OnComboUpdated;
        MinigameScoreManager.OnComboLost -= MinigameScoreManager_OnComboLost;
    }

    private void Start()
    {
        SetTargetScaleByCombo(0);
        SetScaleImmediately();
    }

    private void Update()
    {
        HandleScaleLerping();
    }

    private void HandleScaleLerping()
    {
        if(Mathf.Abs(comboScaleHolder.localScale.x - targetScale) <= SCALE_THRESHOLD) return;

        Vector3 targetScaleVector = targetScale * Vector3.one;

        comboScaleHolder.localScale = Vector3.Lerp(comboScaleHolder.localScale, targetScaleVector, scaleSmoothFactor * Time.deltaTime);
    }

    private void SetScaleImmediately()
    {
        Vector3 targetScaleVector = targetScale * Vector3.one;
        comboScaleHolder.localScale = targetScaleVector;
    }

    private void SetTargetScaleByCombo(int combo)
    {
        float comboT = Mathf.InverseLerp(minComboValue, maxComboValue, combo);
        float scale = Mathf.Lerp(minComboScale, maxComboScale, comboT);

        targetScale = scale;
    }

    #region Animations
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
    #endregion

    private void SetComboValueText(int combo) => comboValueText.text = $"{MULTIPLIER_CHARACTER}{combo}";

    #region Subscriptions
    private void MinigameScoreManager_OnComboGained(object sender, MemoryMinigameScoreManager.OnComboGainedEventArgs e)
    {
        SetComboValueText(e.comboGained);
        SetTargetScaleByCombo(e.comboGained);
        ShowUI();
    }

    private void MinigameScoreManager_OnComboUpdated(object sender, MemoryMinigameScoreManager.OnComboGainedEventArgs e)
    {
        SetComboValueText(e.comboGained);
        SetTargetScaleByCombo(e.comboGained);
        UpdateUI();
    }

    private void MinigameScoreManager_OnComboLost(object sender, System.EventArgs e)
    {
        SetTargetScaleByCombo(0);
        HideUI();
    }
    #endregion
}
