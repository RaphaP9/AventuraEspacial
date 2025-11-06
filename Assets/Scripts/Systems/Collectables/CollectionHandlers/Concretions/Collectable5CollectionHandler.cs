using UnityEngine;

public class Collectable5CollectionHandler : CollectableCollectionHandler
{
    //This collectable checks first round completed on Memory Minigame (make sure to put this script on MemoryMinigame scene)

    private void OnEnable()
    {
        MemoryMinigameManager.OnMemoryRoundEnd += MemoryMinigameManager_OnMemoryRoundEnd;
    }

    private void OnDisable()
    {
        MemoryMinigameManager.OnMemoryRoundEnd -= MemoryMinigameManager_OnMemoryRoundEnd;
    }

    private void MemoryMinigameManager_OnMemoryRoundEnd(object sender, MemoryMinigameManager.OnMemoryRoundEventArgs e)
    {
        CollectCollectable(false);
    }
}
