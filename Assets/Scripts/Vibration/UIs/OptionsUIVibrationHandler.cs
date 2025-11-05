using UnityEngine;
using Lofelt.NiceVibrations;

public class OptionsUIVibrationHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private HapticPatterns.PresetType musicSliderHapticPreset;
    [SerializeField] private HapticPatterns.PresetType SFXSliderHapticPreset;
    [Space]
    [SerializeField] private HapticPatterns.PresetType vibrationToggleHapticPreset;

    private void OnEnable()
    {
        SFXVolumeSliderUIHandler.OnSFXSliderDragEnd += SFXVolumeSliderUIHandler_OnSFXSliderDragEnd;
        SFXVolumeSliderUIHandler.OnSFXSliderPointerUp += SFXVolumeSliderUIHandler_OnSFXSliderPointerUp;

        MusicVolumeSliderUIHandler.OnMusicSliderDragEnd += MusicVolumeSliderUIHandler_OnMusicSliderDragEnd;
        MusicVolumeSliderUIHandler.OnMusicSliderPointerUp += MusicVolumeSliderUIHandler_OnMusicSliderPointerUp;

        VibrationToggleUIHandler.OnVibrationToggled += VibrationToggleUIHandler_OnVibrationToggled;
    }
    private void OnDisable()
    {
        SFXVolumeSliderUIHandler.OnSFXSliderDragEnd -= SFXVolumeSliderUIHandler_OnSFXSliderDragEnd;
        SFXVolumeSliderUIHandler.OnSFXSliderPointerUp -= SFXVolumeSliderUIHandler_OnSFXSliderPointerUp;

        MusicVolumeSliderUIHandler.OnMusicSliderDragEnd -= MusicVolumeSliderUIHandler_OnMusicSliderDragEnd;
        MusicVolumeSliderUIHandler.OnMusicSliderPointerUp -= MusicVolumeSliderUIHandler_OnMusicSliderPointerUp;

        VibrationToggleUIHandler.OnVibrationToggled -= VibrationToggleUIHandler_OnVibrationToggled;
    }

    private void PlayMusicSliderHaptic() => HapticManager.Instance.PlayHaptic(musicSliderHapticPreset, false);
    private void PlaySFXSliderHaptic() => HapticManager.Instance.PlayHaptic(SFXSliderHapticPreset, false);
    private void PlayVibrationToggleHaptic() => HapticManager.Instance.PlayHaptic(vibrationToggleHapticPreset, false);

    #region Subscriptions
    private void MusicVolumeSliderUIHandler_OnMusicSliderPointerUp(object sender, System.EventArgs e)
    {
        PlayMusicSliderHaptic();
    }

    private void MusicVolumeSliderUIHandler_OnMusicSliderDragEnd(object sender, System.EventArgs e)
    {
        PlayMusicSliderHaptic();
    }

    private void SFXVolumeSliderUIHandler_OnSFXSliderPointerUp(object sender, System.EventArgs e)
    {
        PlaySFXSliderHaptic();
    }

    private void SFXVolumeSliderUIHandler_OnSFXSliderDragEnd(object sender, System.EventArgs e)
    {
        PlaySFXSliderHaptic();
    }

    private void VibrationToggleUIHandler_OnVibrationToggled(object sender, VibrationToggleUIHandler.OnVibrationToggledEventArgs e)
    {
        if(e.isOn) PlayVibrationToggleHaptic();
    }
    #endregion
}
