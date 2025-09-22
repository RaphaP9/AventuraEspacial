using UnityEngine;

public class MasterVolumeSliderUIHandler : VolumeSliderUIHandler
{
    private void OnEnable()
    {
        MasterVolumeManager.OnMasterVolumeManagerInitialized += MasterVolumeManager_OnMasterVolumeManagerInitialized;
        MasterVolumeManager.OnMasterVolumeChanged += MasterVolumeManager_OnMasterVolumeChanged;
    }

    private void OnDisable()
    {
        MasterVolumeManager.OnMasterVolumeManagerInitialized -= MasterVolumeManager_OnMasterVolumeManagerInitialized;
        MasterVolumeManager.OnMasterVolumeChanged -= MasterVolumeManager_OnMasterVolumeChanged;
    }

    protected override VolumeManager GetVolumeManager() => MasterVolumeManager.Instance;

    private void MasterVolumeManager_OnMasterVolumeManagerInitialized(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void MasterVolumeManager_OnMasterVolumeChanged(object sender, VolumeManager.OnVolumeChangedEventArgs e)
    {
        //UpdateVisual();
    }
}
