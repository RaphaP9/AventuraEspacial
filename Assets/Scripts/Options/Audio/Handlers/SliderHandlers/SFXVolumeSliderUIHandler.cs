using UnityEngine;

public class SFXVolumeSliderUIHandler : VolumeSliderUIHandler
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SFXVolumeManager.OnSFXVolumeManagerInitialized += SFXVolumeManager_OnSFXVolumeManagerInitialized;
        SFXVolumeManager.OnSFXVolumeChanged += SFXVolumeManager_OnSFXVolumeChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
