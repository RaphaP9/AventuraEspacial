using UnityEngine;
using Lofelt.NiceVibrations;

public class CollectableCollectionUIVibrationHandler : UIVibrationHandler
{
    [Header("Settings")]
    [SerializeField] private HapticPreset collectableCollectedHapticPreset;

    private void OnEnable()
    {
        CollectableCollectionHandler.OnCollectableCollected += CollectableCollectionHandler_OnCollectableCollected;
    }

    private void OnDisable()
    {
        CollectableCollectionHandler.OnCollectableCollected -= CollectableCollectionHandler_OnCollectableCollected;
    }

    private void CollectableCollectionHandler_OnCollectableCollected(object sender, CollectableCollectionHandler.OnCollectableCollectedEventArgs e)
    {
        PlayHaptic_Unforced(collectableCollectedHapticPreset);
    }
}
