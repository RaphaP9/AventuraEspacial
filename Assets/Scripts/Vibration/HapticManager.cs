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

    public void PlayHaptic(HapticPatterns.PresetType hapticPreset, bool forceHaptic)
    {
        if (!forceHaptic && !VibrationStateManager.Instance.VibrationEnabled) return;
        HapticPatterns.PlayPreset(hapticPreset);
    }
}
