using UnityEngine;

public class Collectable11CollectionHandler : CollectableCollectionHandler
{
    //This collectable checks first round completed on Silhouettes Minigame (make sure to put this script on SilhouettesMinigame scene)

    private void OnEnable()
    {
        SilhouettesMinigameManager.OnSilhouettesRoundEnd += SilhouettesMinigameManager_OnSilhouettesRoundEnd;
    }

    private void OnDisable()
    {
        SilhouettesMinigameManager.OnSilhouettesRoundEnd -= SilhouettesMinigameManager_OnSilhouettesRoundEnd;
    }

    private void SilhouettesMinigameManager_OnSilhouettesRoundEnd(object sender, SilhouettesMinigameManager.OnSilhouettesRoundEventArgs e)
    {
        CollectCollectable(false);
    }
}
