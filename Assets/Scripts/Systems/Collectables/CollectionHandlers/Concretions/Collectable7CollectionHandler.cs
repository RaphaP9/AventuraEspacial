using UnityEngine;

public class Collectable7CollectionHandler : CollectableCollectionHandler
{
    //Collectable that checks if you concluded the Memory Minigame (won or lost)

    private void OnEnable()
    {
        MinigameManager.OnGameWon += MinigameManager_OnGameWon;
        MinigameManager.OnGameLost += MinigameManager_OnGameLost;
    }

    private void OnDisable()
    {
        MinigameManager.OnGameWon -= MinigameManager_OnGameWon;
        MinigameManager.OnGameLost -= MinigameManager_OnGameLost;
    }

    private void MinigameManager_OnGameWon(object sender, System.EventArgs e)
    {
        CollectCollectable(false);
    }

    private void MinigameManager_OnGameLost(object sender, System.EventArgs e)
    {
        CollectCollectable(false);
    }
}
