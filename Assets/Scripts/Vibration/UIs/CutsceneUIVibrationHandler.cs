using UnityEngine;

public class CutsceneUIVibrationHandler : UIVibrationHandler
{
    [Header("Settings")]
    [SerializeField] private HapticPreset nextCutscenePanelHapticPreset;

    private void OnEnable()
    {
        CutsceneSceneUIHandler.OnNextCutscenePanelCreated += CutsceneSceneUIHandler_OnNextCutscenePanelCreated;
    }

    private void OnDisable()
    {
        CutsceneSceneUIHandler.OnNextCutscenePanelCreated -= CutsceneSceneUIHandler_OnNextCutscenePanelCreated;
    }

    #region Subscriptions
    private void CutsceneSceneUIHandler_OnNextCutscenePanelCreated(object sender, CutsceneSceneUIHandler.OnCutsceneEventArgs e)
    {
        PlayHaptic_Unforced(nextCutscenePanelHapticPreset);
    }
    #endregion

}
