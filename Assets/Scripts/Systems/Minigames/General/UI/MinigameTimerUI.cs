using UnityEngine;
using UnityEngine.UI;
using System;

public class MinigameTimerUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MinigameTimerManager minigameTimerManager;
    [SerializeField] private Animator animator;

    [Header("UI Components")]
    [SerializeField] private Slider timerSlider;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 10f)] private float smoothLerpFactor;

    [Header("Runtime Filled")]
    [SerializeField] private float targetValue;

    private const float VALUE_REACHED_THRESHOLD = 0.001f;

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

    private void SetValueInstantly(float value) => timerSlider.value = value;

    private void HandleUI()
    {
        targetValue = minigameTimerManager.CurrentTime / minigameTimerManager.TotalTime;

        if (TargetReached()) return;

        timerSlider.value = Mathf.Lerp(timerSlider.value, targetValue, smoothLerpFactor * Time.deltaTime);
    }

    private bool TargetReached() => Math.Abs(timerSlider.value - targetValue) < VALUE_REACHED_THRESHOLD;

    #region Subscriptions
    private void MinigameTimerManager_OnTimeSet(object sender, MemoryMinigameTimerManager.OnTimeSetEventArgs e)
    {
        float value = minigameTimerManager.CurrentTime / minigameTimerManager.TotalTime;
        SetValueInstantly(value);
    }

    private void MinigameTimerManager_OnTimeWarning(object sender, EventArgs e)
    {
        animator.SetTrigger(WARNING_TRIGGER);
    }
    #endregion
}
