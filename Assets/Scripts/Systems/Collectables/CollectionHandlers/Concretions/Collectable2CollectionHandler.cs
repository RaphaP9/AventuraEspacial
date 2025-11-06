using UnityEngine;

public class Collectable2CollectionHandler : CollectableCollectionHandler
{
    //This collectable checks the first time user enters Minigame Selection
    private void Start()
    {
        CollectCollectable(false); //Do not use Async
    }
}
