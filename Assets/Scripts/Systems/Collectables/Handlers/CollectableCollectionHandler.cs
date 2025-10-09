using System;
using UnityEngine;

public abstract class CollectableCollectionHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CollectableSO collectableSO;


    public static event EventHandler<OnCollectableCollectedEventArgs> OnCollectableCollected;

    public class OnCollectableCollectedEventArgs : EventArgs
    {
        public CollectableSO collectableSO;
    }


    //NOTE: Use non Async Save when multiple collectables might be collected on same frame (Ex. Game End)
    protected void CollectCollectable(bool useAsyncSave)
    {
        if (DataContainer.Instance.CollectCollectable(collectableSO))
        {
            if (useAsyncSave) GeneralDataManager.Instance.SaveJSONDataAsyncWrapper();
            else GeneralDataManager.Instance.SaveJSONData();

            OnCollectableCollected?.Invoke(this, new OnCollectableCollectedEventArgs { collectableSO = collectableSO });
        }
    }
}
