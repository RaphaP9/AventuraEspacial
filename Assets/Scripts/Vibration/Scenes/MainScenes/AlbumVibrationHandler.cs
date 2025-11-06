using UnityEngine;

public class AlbumVibrationHandler : SceneVibrationHandler
{
    [Header("Settings")]
    [SerializeField] private HapticPreset nextCutscenePanelHapticPreset;

    protected override void OnEnable()
    {
        base.OnEnable();
        AlbumSceneCutsceneUIHandler.OnNextCutscenePanelCreated += AlbumSceneCutsceneUIHandler_OnNextCutscenePanelCreated;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        AlbumSceneCutsceneUIHandler.OnNextCutscenePanelCreated -= AlbumSceneCutsceneUIHandler_OnNextCutscenePanelCreated;
    }

    #region Subscriptions
    private void AlbumSceneCutsceneUIHandler_OnNextCutscenePanelCreated(object sender, AlbumSceneCutsceneUIHandler.OnCutsceneEventArgs e)
    {
        PlayHaptic_Unforced(nextCutscenePanelHapticPreset);
    }
    #endregion
}
