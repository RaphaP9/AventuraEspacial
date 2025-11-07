using UnityEngine;

public class CollectableCollectionUISFXHandler : UISFXHandler
{
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
        PlaySFX_Unpausable(SFXPool.collectableCollected);
    }
}