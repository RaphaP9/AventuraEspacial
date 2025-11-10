using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class ComboVFXHandler_Particles : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform screenRectTransformRefference;
    [SerializeField] private VisualEffect visualEffect;

    [Header("Settings")]
    [SerializeField] private string screenWidthPropertyName;
    [SerializeField] private string screenHeightPropertyName;
    [SerializeField] private string particleRatePropertyName;
    [Space]
    [SerializeField, Range(2, 10)] private int minComboValue;
    [SerializeField, Range(2, 10)] private int maxComboValue;
    [Space]
    [SerializeField, Range(10, 200)] private int minComboParticleRate;
    [SerializeField, Range(10, 200)] private int maxComboParticleRate;

    [Header("Runtime Filled")]
    [SerializeField] private float width;
    [SerializeField] private float height;

    [Header("Debug")]
    [SerializeField] private bool debug;

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
        StopVFX();
        StartCoroutine(InitializationCoroutine());
    }

    private IEnumerator InitializationCoroutine()
    {
        yield return null; //Wait one Frame
        InitializeDistances();

        SetVFXDimension(screenWidthPropertyName, width);
        SetVFXDimension(screenHeightPropertyName, height);
    }

    private void InitializeDistances()
    {
        //We want to get half the rect transform refference width and height
        height = screenRectTransformRefference.rect.height;
        width = screenRectTransformRefference.rect.width;
    }

    private void SetVFXDimension(string propertyName, float value)
    {
        if (!visualEffect.HasFloat(propertyName))
        {
            if (debug) Debug.Log($"Visual Effect does not have a {propertyName} property");
            return;
        }

        visualEffect.SetFloat(propertyName, value);
    }

    private void SetParticleRateByCombo(int combo)
    {
        float comboT = Mathf.InverseLerp(minComboValue, maxComboValue, combo);
        float particleRateFloat = Mathf.Lerp(minComboParticleRate, maxComboParticleRate, comboT);

        SetParticleRate(Mathf.RoundToInt(particleRateFloat));
    }

    private void SetParticleRate(int particleRate)
    {
        if (!visualEffect.HasFloat(particleRatePropertyName))
        {
            if (debug) Debug.Log($"Visual Effect does not have a {particleRatePropertyName} property");
            return;
        }

        visualEffect.SetFloat(particleRatePropertyName, particleRate);
    }

    private void PlayVFX() => visualEffect.Play();
    private void StopVFX() => visualEffect.Stop();

    #region Subscriptions
    private void MinigameScoreManager_OnComboGained(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        PlayVFX();
        SetParticleRateByCombo(e.comboGained);
    }
    private void MinigameScoreManager_OnComboUpdated(object sender, MinigameScoreManager.OnComboGainedEventArgs e)
    {
        SetParticleRateByCombo(e.comboGained);
    }

    private void MinigameScoreManager_OnComboLost(object sender, System.EventArgs e)
    {
        StopVFX();
        SetParticleRateByCombo(0);
    }
    #endregion
}
