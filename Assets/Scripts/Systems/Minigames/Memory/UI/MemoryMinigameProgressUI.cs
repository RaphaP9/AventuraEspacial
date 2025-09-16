using System;
using UnityEngine;
using UnityEngine.UI;
public class MemoryMinigameProgressUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Slider progressSlider;

    [Header("Settings")]
    [SerializeField, Range(0.1f, 10f)] private float smoothLerpFactor;

    [Header("Runtime Filled")]
    [SerializeField] private float targetValue;

    private const float TARGET_REACHED_THRESHOLD = 0.001f;

    private void OnEnable()
    {
        MemoryMinigameManager.OnGameInitialized += MemoryMinigameManager_OnGameInitialized;

        MemoryMinigameManager.OnRoundStart += MemoryMinigameManager_OnRoundStart;
        MemoryMinigameManager.OnRoundEnd += MemoryMinigameManager_OnRoundEnd;
    }

    private void OnDisable()
    {
        MemoryMinigameManager.OnGameInitialized -= MemoryMinigameManager_OnGameInitialized;

        MemoryMinigameManager.OnRoundStart -= MemoryMinigameManager_OnRoundStart;
        MemoryMinigameManager.OnRoundEnd -= MemoryMinigameManager_OnRoundEnd;
    }

    private void Update()
    {
        HandleProgressLerping();
    }

    private void HandleProgressLerping()
    {
        if (TargetReached()) return;

        progressSlider.value = Mathf.Lerp(progressSlider.value, targetValue, smoothLerpFactor * Time.deltaTime);
    }

    private bool TargetReached() => Math.Abs(progressSlider.value - targetValue) < TARGET_REACHED_THRESHOLD;    
    private void SetProgressInstantly(float progress) => progressSlider.value = progress;

    private void SetTargetValue(int currentRoundIndex, int totalRounds, bool roundCompleted)
    {
        int progressValue = roundCompleted ? currentRoundIndex + 1 : currentRoundIndex;
        float progressPercentage = (float) progressValue / totalRounds;

        targetValue = progressPercentage;
    }

    #region Subscriptions
    private void MemoryMinigameManager_OnGameInitialized(object sender, EventArgs e)
    {
        SetProgressInstantly(0f);
    }

    private void MemoryMinigameManager_OnRoundStart(object sender, MemoryMinigameManager.OnRoundEventArgs e)
    {
        SetTargetValue(e.roundIndex, e.totalRounds, false);
    }

    private void MemoryMinigameManager_OnRoundEnd(object sender, MemoryMinigameManager.OnRoundEventArgs e)
    {
        SetTargetValue(e.roundIndex, e.totalRounds, true);
    }
    #endregion
}
