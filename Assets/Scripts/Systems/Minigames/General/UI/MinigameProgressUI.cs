using UnityEngine;
using UnityEngine.UI;
using System;

public class MinigameProgressUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Slider progressSlider;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 10f)] protected float smoothLerpFactor;

    [Header("Runtime Filled")]
    [SerializeField] protected float targetValue;

    private const float TARGET_REACHED_THRESHOLD = 0.001f;

    private void OnEnable()
    {
        MinigameManager.OnGameInitialized += MinigameManager_OnGameInitialized;

        MinigameManager.OnRoundStart += MinigameManager_OnRoundStart;
        MinigameManager.OnRoundEnd += MinigameManager_OnRoundEnd;
    }

    private void OnDisable()
    {
        MinigameManager.OnGameInitialized -= MinigameManager_OnGameInitialized;

        MinigameManager.OnRoundStart -= MinigameManager_OnRoundStart;
        MinigameManager.OnRoundEnd -= MinigameManager_OnRoundEnd;
    }

    #region Logic
    protected void Update()
    {
        HandleProgressLerping();
    }

    protected void HandleProgressLerping()
    {
        if (TargetReached()) return;

        progressSlider.value = Mathf.Lerp(progressSlider.value, targetValue, smoothLerpFactor * Time.deltaTime);
    }

    protected bool TargetReached() => Math.Abs(progressSlider.value - targetValue) < TARGET_REACHED_THRESHOLD;
    protected void SetProgressInstantly(float progress) => progressSlider.value = progress;

    protected void SetTargetValue(int currentRoundIndex, int totalRounds, bool roundCompleted)
    {
        int progressValue = roundCompleted ? currentRoundIndex + 1 : currentRoundIndex;
        float progressPercentage = (float)progressValue / totalRounds;

        targetValue = progressPercentage;
    }
    #endregion

    #region Subscriptions
    private void MinigameManager_OnGameInitialized(object sender, EventArgs e)
    {
        SetProgressInstantly(0f);
    }

    private void MinigameManager_OnRoundStart(object sender, MinigameManager.OnRoundEventArgs e)
    {
        SetTargetValue(e.roundIndex, e.totalRounds, false);
    }

    private void MinigameManager_OnRoundEnd(object sender, MinigameManager.OnRoundEventArgs e)
    {
        SetTargetValue(e.roundIndex, e.totalRounds, true);
    }
    #endregion
}
