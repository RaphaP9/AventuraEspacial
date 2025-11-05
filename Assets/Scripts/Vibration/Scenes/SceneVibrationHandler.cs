using Lofelt.NiceVibrations;
using UnityEngine;

public class SceneVibrationHandler : MonoBehaviour
{
    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public void PlayHapticUnforced(HapticPatterns.PresetType hapticPreset) => HapticManager.Instance.PlayHaptic(hapticPreset, false);
    public void PlayHapticForced(HapticPatterns.PresetType hapticPreset) => HapticManager.Instance.PlayHaptic(hapticPreset, true);
}
