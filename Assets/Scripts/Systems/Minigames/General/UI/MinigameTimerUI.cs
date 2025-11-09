using UnityEngine;
using UnityEngine.UI;
using System;

public class MinigameTimerUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MinigameTimerManager minigameTimerManager;
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private Image timerImage;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 10f)] private float smoothLerpFactor;

    [Header("Runtime Filled")]
    [SerializeField] private float targetFill;

    private const float FILL_AMOUNT_REACHED_THRESHOLD = 0.001f;

    private const string WARNING_TRIGGER = "Warning";

    private void OnEnable()
    {
        MinigameTimerManager.OnTimeSet += MinigameTimerManager_OnTimeSet;
        MinigameTimerManager.OnTimeWarning += MinigameTimerManager_OnTimeWarning;
    }

    private void OnDisable()
    {
        MinigameTimerManager.OnTimeSet -= MinigameTimerManager_OnTimeSet;
        MinigameTimerManager.OnTimeWarning -= MinigameTimerManager_OnTimeWarning;
    }

    private void Update()
    {
        HandleUI();
    }

    private void SetFillInstantly(float fillAmount) => timerImage.fillAmount = fillAmount;

    private void HandleUI()
    {
        targetFill = minigameTimerManager.CurrentTime / minigameTimerManager.TotalTime;

        if (TargetReached()) return;

        timerImage.fillAmount = Mathf.Lerp(timerImage.fillAmount, targetFill, smoothLerpFactor * Time.deltaTime);
    }

    private bool TargetReached() => Math.Abs(timerImage.fillAmount - targetFill) < FILL_AMOUNT_REACHED_THRESHOLD;

    #region Subscriptions
    private void MinigameTimerManager_OnTimeSet(object sender, MemoryMinigameTimerManager.OnTimeSetEventArgs e)
    {
        float fill = minigameTimerManager.CurrentTime / minigameTimerManager.TotalTime;
        SetFillInstantly(fill);
    }

    private void MinigameTimerManager_OnTimeWarning(object sender, EventArgs e)
    {
        animator.SetTrigger(WARNING_TRIGGER);
    }
    #endregion
}
