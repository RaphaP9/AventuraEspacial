using UnityEngine;
using System;
using Lofelt.NiceVibrations;

public class HapticManager : MonoBehaviour
{
    public static HapticManager Instance { get; private set; }

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.LogWarning("There is more than one HapticManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void PlayHaptic(HapticPreset hapticPreset, bool forceHaptic)
    {
        if (!forceHaptic && !VibrationStateManager.Instance.VibrationEnabled) return;

        HapticPatterns.PlayPreset(GetLofeltHapticPreset(hapticPreset));
    }

    public HapticPatterns.PresetType GetLofeltHapticPreset(HapticPreset hapticPreset)
    {
        switch (hapticPreset)
        {
            case HapticPreset.None:
            default:
                return HapticPatterns.PresetType.None;
            case HapticPreset.Selection:
                return HapticPatterns.PresetType.Selection;
            case HapticPreset.SoftImpact:
                return HapticPatterns.PresetType.SoftImpact;
            case HapticPreset.LightImpact:
                return HapticPatterns.PresetType.LightImpact;
            case HapticPreset.MediumImpact:
                return HapticPatterns.PresetType.MediumImpact;
            case HapticPreset.RigidImpact:
                return HapticPatterns.PresetType.RigidImpact;
            case HapticPreset.HeavyImpact:
                return HapticPatterns.PresetType.HeavyImpact;
            case HapticPreset.Success:
                return HapticPatterns.PresetType.Success;
            case HapticPreset.Warning:
                return HapticPatterns.PresetType.Warning;
            case HapticPreset.Failure:
                return HapticPatterns.PresetType.Failure;
        }
    }
}
