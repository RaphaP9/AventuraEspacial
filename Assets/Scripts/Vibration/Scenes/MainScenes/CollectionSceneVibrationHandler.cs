using Lofelt.NiceVibrations;
using UnityEngine;

public class CollectionSceneVibrationHandler : SceneVibrationHandler
{
    [Header("Settings")]
    [SerializeField] private HapticPreset collectableCollectedTapHapticPreset;
    [SerializeField] private HapticPreset collectableNotCollectedTapHapticPreset;

    protected override void OnEnable()
    {
        base.OnEnable();
        CollectableUI.OnCollectableUIClicked += CollectableUI_OnCollectableUIClicked;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CollectableUI.OnCollectableUIClicked -= CollectableUI_OnCollectableUIClicked;
    }

    #region Subscriptions
    private void CollectableUI_OnCollectableUIClicked(object sender, CollectableUI.OnCollectableUIEventArgs e)
    {
        if (e.collectableUI.IsCollected) PlayHaptic_Unforced(collectableCollectedTapHapticPreset);
        else PlayHaptic_Unforced(collectableNotCollectedTapHapticPreset);
    }
    #endregion
}