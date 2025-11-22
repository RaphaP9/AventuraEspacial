using UnityEngine;
using UnityEngine.VFX;

public class FigureDragVFXHandler_Trail : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform rectTransformRefference;

    [SerializeField] private FigureHandler figureHandler;
    [SerializeField] private VisualEffect visualEffect;

    [Header("Settings")]
    [SerializeField, Range(0f, 0.005f)] private float minVertexDistance;

    [Header("Runtime Filled")]
    [SerializeField] private bool VFXEnabled;
    [SerializeField] private bool VFXEnabledDueToVertexDistance;

    private Vector3 previousRectTransformPosition = Vector3.zero;

    private const string SPAWN_ENABLED_PROPERTY_NAME = "SpawnEnabled";

    private void OnEnable()
    {
        figureHandler.OnThisFigureDragStart += FigureHandler_OnThisFigureDragStart;
        figureHandler.OnThisFigureDragEnd += FigureHandler_OnThisFigureDragEnd;
    }

    private void OnDisable()
    {
        figureHandler.OnThisFigureDragStart -= FigureHandler_OnThisFigureDragStart;
        figureHandler.OnThisFigureDragEnd -= FigureHandler_OnThisFigureDragEnd;
    }

    private void Start()
    {
        StopVFX();
    }

    private void Update()
    {
        HandleVertexDistance();
        previousRectTransformPosition = GetRectTransformPosition();
    }

    private void HandleVertexDistance()
    {
        if (!VFXEnabled) return;

        if(Vector3.Distance(GetRectTransformPosition(), previousRectTransformPosition) < minVertexDistance)
        {
            if (VFXEnabled && VFXEnabledDueToVertexDistance)
            {
                SetVFXEnablement(false);
                VFXEnabledDueToVertexDistance = false;
            }
        }
        else
        {
            if (VFXEnabled && !VFXEnabledDueToVertexDistance)
            {
                SetVFXEnablement(true);
                VFXEnabledDueToVertexDistance = true;
            }
        }      
    }

    private Vector3 GetRectTransformPosition() => rectTransformRefference.position;

    private void PlayVFX() => visualEffect.Play();
    private void StopVFX() => visualEffect.Stop();

    private void SetVFXEnablement(bool enable)
    {
        if (!visualEffect.HasBool(SPAWN_ENABLED_PROPERTY_NAME)) return;

        visualEffect.SetBool(SPAWN_ENABLED_PROPERTY_NAME, enable);
    }

    private void FigureHandler_OnThisFigureDragStart(object sender, System.EventArgs e)
    {
        PlayVFX();
        SetVFXEnablement(true);

        VFXEnabled = true;
        VFXEnabledDueToVertexDistance = true;
    }

    private void FigureHandler_OnThisFigureDragEnd(object sender, System.EventArgs e)
    {
        StopVFX();

        VFXEnabled = false;
        VFXEnabledDueToVertexDistance = true;
    }
}
