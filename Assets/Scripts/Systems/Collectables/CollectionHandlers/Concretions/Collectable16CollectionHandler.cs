using UnityEngine;

public class Collectable16CollectionHandler : CollectableCollectionHandler
{
    //Collectable that checks if you match a silhouette (Memory Minigame) having X time or less on the timer
    [Header("Specific Components")]
    [SerializeField] private MinigameTimerManager minigameTimerManager;

    [Header("Settings")]
    [SerializeField, Range(1f, 20f)] private float targetTime;

    private void OnEnable()
    {
        SilhouettesMinigameManager.OnSilhouetteMatchPreliminar += SilhouettesMinigameManager_OnSilhouetteMatchPreliminar;
    }

    private void OnDisable()
    {
        SilhouettesMinigameManager.OnSilhouetteMatchPreliminar -= SilhouettesMinigameManager_OnSilhouetteMatchPreliminar;
    }

    private void CheckCollectableCollected(float currentTime)
    {
        if (currentTime > targetTime) return;
        CollectCollectable(false);
    }

    private void SilhouettesMinigameManager_OnSilhouetteMatchPreliminar(object sender, System.EventArgs e)
    {
        CheckCollectableCollected(minigameTimerManager.CurrentTime);
    }
}
