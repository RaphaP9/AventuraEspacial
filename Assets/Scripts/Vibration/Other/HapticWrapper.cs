using UnityEngine;
using Lofelt.NiceVibrations;

public class HapticWrapper : MonoBehaviour
{
    public void PlayHaptic_Unforced(HapticPatterns.PresetType hapticPreset) => HapticManager.Instance.PlayHaptic(hapticPreset, false);
    public void PlayHaptic_Forced(HapticPatterns.PresetType hapticPreset) => HapticManager.Instance.PlayHaptic(hapticPreset, true);

    //////
    
    public void PlayHaptic_Unforced_Selection() => PlayHaptic_Unforced(HapticPatterns.PresetType.Selection);
    public void PlayHaptic_Unforced_SoftImpact() => PlayHaptic_Unforced(HapticPatterns.PresetType.SoftImpact);
    public void PlayHaptic_Unforced_LightImpact() => PlayHaptic_Unforced(HapticPatterns.PresetType.LightImpact);
    public void PlayHaptic_Unforced_MediumImpact() => PlayHaptic_Unforced(HapticPatterns.PresetType.MediumImpact);
    public void PlayHaptic_Unforced_RigidImpact() => PlayHaptic_Unforced(HapticPatterns.PresetType.RigidImpact);
    public void PlayHaptic_Unforced_HeavyImpact() => PlayHaptic_Unforced(HapticPatterns.PresetType.HeavyImpact);
    public void PlayHaptic_Unforced_Success() => PlayHaptic_Unforced(HapticPatterns.PresetType.Success);
    public void PlayHaptic_Unforced_Warning() => PlayHaptic_Unforced(HapticPatterns.PresetType.Warning);
    public void PlayHaptic_Unforced_Failure() => PlayHaptic_Unforced(HapticPatterns.PresetType.Failure);

    //////
    public void PlayHaptic_Forced_Selection() => PlayHaptic_Forced(HapticPatterns.PresetType.Selection);
    public void PlayHaptic_Forced_SoftImpact() => PlayHaptic_Forced(HapticPatterns.PresetType.SoftImpact);
    public void PlayHaptic_Forced_LightImpact() => PlayHaptic_Forced(HapticPatterns.PresetType.LightImpact);
    public void PlayHaptic_Forced_MediumImpact() => PlayHaptic_Forced(HapticPatterns.PresetType.MediumImpact);
    public void PlayHaptic_Forced_RigidImpact() => PlayHaptic_Forced(HapticPatterns.PresetType.RigidImpact);
    public void PlayHaptic_Forced_HeavyImpact() => PlayHaptic_Forced(HapticPatterns.PresetType.HeavyImpact);
    public void PlayHaptic_Forced_Success() => PlayHaptic_Forced(HapticPatterns.PresetType.Success);
    public void PlayHaptic_Forced_Warning() => PlayHaptic_Forced(HapticPatterns.PresetType.Warning);
    public void PlayHaptic_Forced_Failure() => PlayHaptic_Forced(HapticPatterns.PresetType.Failure);
}
