using UnityEngine;
using Lofelt.NiceVibrations;

public class CollectableCollectionUIVibrationHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private HapticPatterns.PresetType collectableCollectedHapticPreset;

    private void OnEnable()
    {
        CollectableCollectionHandler.OnCollectableCollected += CollectableCollectionHandler_OnCollectableCollected;
    }

    private void OnDisable()
    {
        CollectableCollectionHandler.OnCollectableCollected -= CollectableCollectionHandler_OnCollectableCollected;
    }

    private void PlayCollectableCollectedHaptic() => HapticManager.Instance.PlayHaptic(collectableCollectedHapticPreset, false);

    private void CollectableCollectionHandler_OnCollectableCollected(object sender, CollectableCollectionHandler.OnCollectableCollectedEventArgs e)
    {
        PlayCollectableCollectedHaptic();
    }
}
