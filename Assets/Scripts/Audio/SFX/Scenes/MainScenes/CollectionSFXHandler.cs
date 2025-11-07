using UnityEngine;

public class CollectionSFXHandler : SceneSFXHandler
{
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
        if (e.collectableUI.IsCollected) PlaySFX_Unpausable(SFXPool.collectableCollectedSelected);
        else PlaySFX_Unpausable(SFXPool.collectableNotCollectedSelected);
    }
    #endregion
}