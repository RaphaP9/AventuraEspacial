using UnityEngine;

public class SFXVolumeSliderUIHandler : VolumeSliderUIHandler
{
    private void OnEnable()
    {
        SFXVolumeManager.OnSFXVolumeManagerInitialized += SFXVolumeManager_OnSFXVolumeManagerInitialized;
        SFXVolumeManager.OnSFXVolumeChanged += SFXVolumeManager_OnSFXVolumeChanged;
    }

    private void OnDisable()
    {
        SFXVolumeManager.OnSFXVolumeManagerInitialized -= SFXVolumeManager_OnSFXVolumeManagerInitialized;
        SFXVolumeManager.OnSFXVolumeChanged -= SFXVolumeManager_OnSFXVolumeChanged;
    }

    protected override VolumeManager GetVolumeManager() => SFXVolumeManager.Instance;

    private void SFXVolumeManager_OnSFXVolumeManagerInitialized(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void SFXVolumeManager_OnSFXVolumeChanged(object sender, VolumeManager.OnVolumeChangedEventArgs e)
    {
        //UpdateVisual();
    }
}
