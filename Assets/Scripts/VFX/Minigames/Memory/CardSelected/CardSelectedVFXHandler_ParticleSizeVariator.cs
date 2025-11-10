using UnityEngine;
using UnityEngine.VFX;

public class CardSelectedVFXHandler_ParticleSizeVariator : CardSelectedVFXHandler
{
    [Header("Component")]
    [SerializeField] private VisualEffect visualEffect;

    [Header("Settings")]
    [SerializeField] private float defaultParticleSize;
    [SerializeField] private string particleSizePropertyName;

    [Header("Debug")]
    [SerializeField] private bool debug;

    public override void SetSizeByFactor(float factor)
    {
        if (!visualEffect.HasFloat(particleSizePropertyName))
        {
            if(debug) Debug.Log($"Visual Effect does not have a {particleSizePropertyName} property");
            return;
        }

        float processedParticleSize = defaultParticleSize * factor;

        visualEffect.SetFloat(particleSizePropertyName, processedParticleSize);
    }
}
