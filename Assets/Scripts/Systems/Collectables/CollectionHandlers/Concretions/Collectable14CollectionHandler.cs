using System;
using UnityEngine;

public class Collectable14CollectionHandler : CollectableCollectionHandler
{
    //Collectable that checks if you won the Silhouettes Minigame

    private void OnEnable()
    {
        MinigameManager.OnGameWinning += MinigameManager_OnGameWinning;
    }

    private void OnDisable()
    {
        MinigameManager.OnGameWinning -= MinigameManager_OnGameWinning;
    }

    private void MinigameManager_OnGameWinning(object sender, EventArgs e)
    {
        CollectCollectable(false);
    }
}
