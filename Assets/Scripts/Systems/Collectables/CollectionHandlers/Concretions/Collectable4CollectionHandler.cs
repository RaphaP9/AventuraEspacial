using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Collectable4CollectionHandler : CollectableCollectionHandler
{
    //This collectable checks if user has played all minigames;

    [Header("Lists")]
    [SerializeField] private List<Minigame> minigameList;

    [Header("Settings")]
    [SerializeField, Range(0f, 2f)] private float collectionDelay;

    private bool hasPerformedFirstUpdateLogic = false;

    private void Update()
    {
        FirstUpdateLogic();
    }

    //As the minigame times played count add is performed on Start (MinigameSceneDataModifier), we must make sure we check condition after (First Update)
    private void FirstUpdateLogic()
    {
        if (hasPerformedFirstUpdateLogic) return;

        hasPerformedFirstUpdateLogic = true;

        StartCoroutine(CheckCollectableCollectionCoroutine());
    }

    private IEnumerator CheckCollectableCollectionCoroutine()
    {
        yield return new WaitForSeconds(collectionDelay);

        bool condition = CheckAllMinigamesPlayed();

        if (condition) CollectCollectable(false);
    }

    private bool CheckAllMinigamesPlayed()
    {
        foreach(Minigame minigame in minigameList)
        {
            if(!DataContainer.Instance.HasEnteredMinigame(minigame)) return false;
        }

        return true;
    }
}
