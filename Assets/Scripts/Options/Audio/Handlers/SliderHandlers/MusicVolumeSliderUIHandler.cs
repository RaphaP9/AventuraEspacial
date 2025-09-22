using UnityEngine;

public class MusicVolumeSliderUIHandler : VolumeSliderUIHandler
{
    protected override void OnEnable()
    {
        base.OnEnable();
        MusicVolumeManager.OnMusicVolumeManagerInitialized += MusicVolumeManager_OnMusicVolumeManagerInitialized;
        MusicVolumeManager.OnMusicVolumeChanged += MusicVolumeManager_OnMusicVolumeChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        MusicVolumeManager.OnMusicVolumeManagerInitialized -= MusicVolumeManager_OnMusicVolumeManagerInitialized;
        MusicVolumeManager.OnMusicVolumeChanged -= MusicVolumeManager_OnMusicVolumeChanged;
    }

    protected override VolumeManager GetVolumeManager() => MusicVolumeManager.Instance;

    private void MusicVolumeManager_OnMusicVolumeManagerInitialized(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void MusicVolumeManager_OnMusicVolumeChanged(object sender, VolumeManager.OnVolumeChangedEventArgs e)
    {
        //UpdateVisual();
    }
}
