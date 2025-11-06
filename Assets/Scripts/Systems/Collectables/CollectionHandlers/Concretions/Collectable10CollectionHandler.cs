using UnityEngine;

public class Collectable10CollectionHandler : CollectableCollectionHandler
{
    //Collectable that checks if you hit a pair (Memory Minigame) having X time or less on the timer
    [Header("Specific Components")]
    [SerializeField] private MinigameTimerManager minigameTimerManager;

    [Header("Settings")]
    [SerializeField, Range(1f, 20f)] private float targetTime;

    private void OnEnable()
    {
        MemoryMinigameManager.OnPairMatchPreliminar += MemoryMinigameManager_OnPairMatchPreliminar;
    }

    private void OnDisable()
    {
        MemoryMinigameManager.OnPairMatchPreliminar -= MemoryMinigameManager_OnPairMatchPreliminar;
    }

    private void CheckCollectableCollected(float currentTime)
    {
        if (currentTime > targetTime) return;
        CollectCollectable(false);
    }

    private void MemoryMinigameManager_OnPairMatchPreliminar(object sender, System.EventArgs e)
    {
        CheckCollectableCollected(minigameTimerManager.CurrentTime);
    }
}
