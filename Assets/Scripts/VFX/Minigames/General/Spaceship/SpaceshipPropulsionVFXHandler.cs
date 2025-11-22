using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class SpaceshipPropulsionVFXHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform rectTransformRefference;
    [SerializeField] private VisualEffect visualEffect;

    [Header("Settings")]
    [SerializeField, Range(0f, 0.005f)] private float minRefferenceDisplacement;
    [SerializeField, Range(0.1f, 5f)] private float VFXActiveTime;

    [Header("Runtime Filled")]
    [SerializeField] private bool VFXEnabled;
    [SerializeField] private bool VFXEnabledDueToVertexDistance;

    private Vector3 previousRectTransformPosition = Vector3.zero;

    private const string SPAWN_ENABLED_PROPERTY_NAME = "SpawnEnabled";

    private void OnEnable()
    {
        MinigameManager.OnRoundEnd += MinigameManager_OnRoundEnd;
    }

    private void OnDisable()
    {
        MinigameManager.OnRoundEnd -= MinigameManager_OnRoundEnd;
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

        if (Vector3.Distance(GetRectTransformPosition(), previousRectTransformPosition) < minRefferenceDisplacement)
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

    private IEnumerator PlayVFXCoroutine()
    {
        PlayVFX();
        SetVFXEnablement(true);
        VFXEnabled = true;

        yield return new WaitForSeconds(VFXActiveTime);

        StopVFX();
        VFXEnabled = false;
    }

    private void MinigameManager_OnRoundEnd(object sender, MinigameManager.OnRoundEventArgs e)
    {
        StopAllCoroutines();
        StartCoroutine(PlayVFXCoroutine());
    }
}
