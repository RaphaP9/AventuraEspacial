using UnityEngine;
using Lofelt.NiceVibrations;

public class HapticWrapper : MonoBehaviour
{
    private const string HAPTIC_PRESSET_NONE = "None";
    private const string HAPTIC_PRESSET_SELECTION = "Selection";
    private const string HAPTIC_PRESSET_SOFT_IMPACT = "SoftImpact";
    private const string HAPTIC_PRESSET_LIGHT_IMPACT = "LightImpact";
    private const string HAPTIC_PRESSET_MEDIUM_IMPACT = "MediumImpact";
    private const string HAPTIC_PRESSET_RIGID_IMPACT = "RigidImpact";
    private const string HAPTIC_PRESSET_HEAVY_IMPACT = "HeavyImpact";
    private const string HAPTIC_PRESSET_SUCCESS = "Success";
    private const string HAPTIC_PRESSET_WARNING = "Warning";
    private const string HAPTIC_PRESSET_FAILURE = "Failure";

    public void PlayHaptic_Unforced(HapticPreset hapticPreset) => HapticManager.Instance.PlayHaptic(hapticPreset, false);
    public void PlayHaptic_Forced(HapticPreset hapticPreset) => HapticManager.Instance.PlayHaptic(hapticPreset, true);
    
    public void PlayHaptic_Unforced(string hapticPreset)
    {
        switch (hapticPreset)
        {
            case HAPTIC_PRESSET_NONE:
            default:
                PlayHaptic_Unforced(HapticPreset.None);
                break;
            case HAPTIC_PRESSET_SELECTION:
                PlayHaptic_Unforced(HapticPreset.Selection);
                break;
            case HAPTIC_PRESSET_SOFT_IMPACT:
                PlayHaptic_Unforced(HapticPreset.SoftImpact);
                break;
            case HAPTIC_PRESSET_LIGHT_IMPACT:
                PlayHaptic_Unforced(HapticPreset.LightImpact);
                break;
            case HAPTIC_PRESSET_MEDIUM_IMPACT:
                PlayHaptic_Unforced(HapticPreset.MediumImpact);
                break;
            case HAPTIC_PRESSET_RIGID_IMPACT:
                PlayHaptic_Unforced(HapticPreset.RigidImpact);
                break;
            case HAPTIC_PRESSET_HEAVY_IMPACT:
                PlayHaptic_Unforced(HapticPreset.HeavyImpact);
                break;
            case HAPTIC_PRESSET_SUCCESS:
                PlayHaptic_Unforced(HapticPreset.Success);
                break;
            case HAPTIC_PRESSET_WARNING:
                PlayHaptic_Unforced(HapticPreset.Warning);
                break;
            case HAPTIC_PRESSET_FAILURE:
                PlayHaptic_Unforced(HapticPreset.Failure);
                break;
        }
    }

    public void PlayHaptic_Forced(string hapticPreset)
    {
        switch (hapticPreset)
        {
            case HAPTIC_PRESSET_NONE:
            default:
                PlayHaptic_Forced(HapticPreset.None);
                break;
            case HAPTIC_PRESSET_SELECTION:
                PlayHaptic_Forced(HapticPreset.Selection);
                break;
            case HAPTIC_PRESSET_SOFT_IMPACT:
                PlayHaptic_Forced(HapticPreset.SoftImpact);
                break;
            case HAPTIC_PRESSET_LIGHT_IMPACT:
                PlayHaptic_Forced(HapticPreset.LightImpact);
                break;
            case HAPTIC_PRESSET_MEDIUM_IMPACT:
                PlayHaptic_Forced(HapticPreset.MediumImpact);
                break;
            case HAPTIC_PRESSET_RIGID_IMPACT:
                PlayHaptic_Forced(HapticPreset.RigidImpact);
                break;
            case HAPTIC_PRESSET_HEAVY_IMPACT:
                PlayHaptic_Forced(HapticPreset.HeavyImpact);
                break;
            case HAPTIC_PRESSET_SUCCESS:
                PlayHaptic_Forced(HapticPreset.Success);
                break;
            case HAPTIC_PRESSET_WARNING:
                PlayHaptic_Forced(HapticPreset.Warning);
                break;
            case HAPTIC_PRESSET_FAILURE:
                PlayHaptic_Forced(HapticPreset.Failure);
                break;
        }
    }

}
