using UnityEngine;

public class MusicVolumeSliderUIHandler : VolumeSliderUIHandler
{
    private void OnEnable()
    {
        MusicVolumeManager.OnMusicVolumeManagerInitialized += MusicVolumeManager_OnMusicVolumeManagerInitialized;
        MusicVolumeManager.OnMusicVolumeChanged += MusicVolumeManager_OnMusicVolumeChanged;
    }

    private void OnDisable()
    {
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
