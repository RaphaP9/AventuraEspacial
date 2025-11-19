using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class MainMenuStarsVFXHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectDimensionsChangeDetector rectDimensionsChangeDetector;
    [SerializeField] private RectTransform screenRectTransformRefference;
    [SerializeField] private VisualEffect visualEffect;

    [Header("Settings")]
    [SerializeField] private string screenWidthPropertyName;
    [SerializeField] private string screenHeightPropertyName;

    [Header("Runtime Filled")]
    [SerializeField] private float width;
    [SerializeField] private float height;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private void OnEnable()
    {
        rectDimensionsChangeDetector.OnRectDimensionsChanged += RectDimensionsChangeDetector_OnRectDimensionsChanged;
    }

    private void OnDisable()
    {
        rectDimensionsChangeDetector.OnRectDimensionsChanged -= RectDimensionsChangeDetector_OnRectDimensionsChanged;
    }

    private void Start()
    {
        InitializeDistances();
        SetVFXDimension(screenWidthPropertyName, width);
        SetVFXDimension(screenHeightPropertyName, height);
    }

    private void InitializeDistances()
    {
        //We want to get the rect transform refference width and height
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

    #region Subscriptions
    private void RectDimensionsChangeDetector_OnRectDimensionsChanged(object sender, System.EventArgs e)
    {
        InitializeDistances();
        SetVFXDimension(screenWidthPropertyName, width);
        SetVFXDimension(screenHeightPropertyName, height);
    }
    #endregion
}
