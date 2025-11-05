using Lofelt.NiceVibrations;
using UnityEngine;

public class CollectionSceneVibrationHandler : SceneVibrationHandler
{
    [Header("Settings")]
    [SerializeField] private HapticPatterns.PresetType collectableCollectedTapHapticPreset;
    [SerializeField] private HapticPatterns.PresetType collectableNotCollectedTapHapticPreset;

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

    private void PlayCollectableCollectedTapHaptic() => HapticManager.Instance.PlayHaptic(collectableCollectedTapHapticPreset, false);
    private void PlayCollectableNotCollectedTapHaptic() => HapticManager.Instance.PlayHaptic(collectableNotCollectedTapHapticPreset, false);

    #region Subscriptions
    private void CollectableUI_OnCollectableUIClicked(object sender, CollectableUI.OnCollectableUIEventArgs e)
    {
        if (e.collectableUI.IsCollected) PlayCollectableCollectedTapHaptic();
        else PlayCollectableNotCollectedTapHaptic();
    }
    #endregion
}