using System.Collections;
using UnityEngine;

public class Collectable1CollectionHandler : CollectableCollectionHandler
{
    [Header("Settings")]
    [SerializeField, Range(0f,2f)] private float collectionDelay;

    //This collectable checks the first time user enters Minigame Selection
    private void Start()
    {
        StartCoroutine(CollectCollectableCorroutine());
    }

    private IEnumerator CollectCollectableCorroutine()
    {
        yield return new WaitForSecondsRealtime(collectionDelay);
        CollectCollectable(false); //Do not use Async
    }
}
