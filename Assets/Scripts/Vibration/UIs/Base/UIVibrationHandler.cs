using UnityEngine;

public class UIVibrationHandler : MonoBehaviour
{
    public void PlayHaptic_Unforced(HapticPreset hapticPreset) => HapticManager.Instance.PlayHaptic(hapticPreset, false);
    public void PlayHaptic_Forced(HapticPreset hapticPreset) => HapticManager.Instance.PlayHaptic(hapticPreset, true);
}
