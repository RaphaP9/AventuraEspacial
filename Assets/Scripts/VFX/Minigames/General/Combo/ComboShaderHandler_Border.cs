using System;
using UnityEngine;
using UnityEngine.UI;

public class ComboShaderHandler_Border : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image shaderImage;

    [Header("Settings")]
    [SerializeField] private string intensityMultiplierPropertyName;
    [Space]
    [SerializeField, Range(2, 10)] private int minComboValue;
    [SerializeField, Range(2, 10)] private int maxComboValue;
    [Space]
    [SerializeField, Range(0f, 2f)] private float minComboIntensityMultiplier;
    [SerializeField, Range(0f, 2f)] private float maxComboIntensityMultiplier;
    [Space]
    [SerializeField, Range(1f, 10f)] private float smoothFactor;

    private Material material;
    private float targetIntensityMultiplier = 0f;

    private const float NO_COMBO_INTENSITY_MULTIPLIER = 0f;
    private const float INTENSITY_MULTIPLIER_THRESHOLD = 0.01f;

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

    private void Awake()
    {
        material = shaderImage.material;
        shaderImage.enabled = true;
    }

    private void Start()
    {
        SetTargetIntensityMultiplier(NO_COMBO_INTENSITY_MULTIPLIER);
        SetIntensityMultiplier(NO_COMBO_INTENSITY_MULTIPLIER);
    }

    private void Update()
    {
        HandleIntensityMultiplierLerping();
    }

    private void HandleIntensityMultiplierLerping()
    {
        if (!material.HasFloat(intensityMultiplierPropertyName)) return;

        float currentIntensityMultiplier = material.GetFloat(intensityMultiplierPropertyName);

        if (Mathf.Abs(currentIntensityMultiplier - targetIntensityMultiplier) <= INTENSITY_MULTIPLIER_THRESHOLD) return;

        float newIntensityMultiplier = Mathf.Lerp(currentIntensityMultiplier, targetIntensityMultiplier, smoothFactor * Time.deltaTime);
        SetIntensityMultiplier(newIntensityMultiplier);
    }

    private void SetTargetIntensityMultiplierByCombo(int combo)
    {
        float comboT = Mathf.InverseLerp(minComboValue, maxComboValue, combo);
        float intensityMultiplier = Mathf.Lerp(minComboIntensityMultiplier, maxComboIntensityMultiplier, comboT);

        targetIntensityMultiplier = intensityMultiplier;
    }

    private void SetTargetIntensityMultiplier(float intensityMultiplier) => targetIntensityMultiplier = intensityMultiplier;

    private void SetIntensityMultiplier(float intensityMultiplier)
    {
        if (!material.HasFloat(intensityMultiplierPropertyName)) return;

        material.SetFloat(intensityMultiplierPropertyName, intensityMultiplier);
    }

    #region Subscriptions
    private void MinigameScoreManager_OnComboGained(object sender, MemoryMinigameScoreManager.OnComboGainedEventArgs e)
    {
        SetTargetIntensityMultiplierByCombo(e.comboGained);
    }

    private void MinigameScoreManager_OnComboUpdated(object sender, MemoryMinigameScoreManager.OnComboGainedEventArgs e)
    {
        SetTargetIntensityMultiplierByCombo(e.comboGained);
    }

    private void MinigameScoreManager_OnComboLost(object sender, System.EventArgs e)
    {
        SetTargetIntensityMultiplier(NO_COMBO_INTENSITY_MULTIPLIER);
    }
    #endregion
}
